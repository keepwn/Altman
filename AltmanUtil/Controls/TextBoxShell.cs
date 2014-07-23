using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Altman.Controls
{
    public partial class TextBoxShell : TextBox
    {
        public class CommandEnteredEventArgs : EventArgs
        {
            string command;
            public CommandEnteredEventArgs(string command)
            {
                this.command = command;
            }

            public string Command
            {
                get { return command; }
            }
        }
        public class CommandHistory
        {
            private int _currentPosn;
            private string _lastCommand;
            private ArrayList commandHistory = new ArrayList();

            /// <summary>
            /// 添加命令
            /// </summary>
            public void Add(string command)
            {
                if (command != _lastCommand)
                {
                    commandHistory.Add(command);
                    _lastCommand = command;
                    _currentPosn = commandHistory.Count;
                }
            }

            /// <summary>
            /// 上一条命令是否存在
            /// </summary>
            public bool DoesPreviousCommandExist()
            {
                return _currentPosn > 0;
            }
            /// <summary>
            /// 下一条命令是否存在
            /// </summary>
            public bool DoesNextCommandExist()
            {
                return _currentPosn < commandHistory.Count - 1;
            }
            /// <summary>
            /// 获取上一条命令
            /// </summary>
            public string GetPreviousCommand()
            {
                _lastCommand = (string)commandHistory[--_currentPosn];
                return _lastCommand;
            }
            /// <summary>
            /// 获取下一条命令
            /// </summary>
            public string GetNextCommand()
            {
                _lastCommand = (string)commandHistory[++_currentPosn];
                return LastCommand;
            }
            /// <summary>
            /// 获取历史命令
            /// </summary>
            public string[] GetCommandHistory()
            {
                return (string[])commandHistory.ToArray(typeof(string));
            }
            /// <summary>
            /// 最后一条命令
            /// </summary>
            public string LastCommand
            {
                get { return _lastCommand; }
            }
        }

        private bool _isWin = true;
        private string _prompt;
        private CommandHistory commandHistory = new CommandHistory();
        
        public delegate void EventCommandEntered(object sender, CommandEnteredEventArgs e);
        public event EventCommandEntered CommandEntered;
        public TextBoxShell()
        {
            InitializeComponent();
        }

        #region 按键事件处理
        /// <summary>
        /// 重写，防止正文内容被删除
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0302: //WM_PASTE
                case 0x0300: //WM_CUT
                case 0x000C: //WM_SETTEXT
                    if (!IsCaretAtWritablePosition())
                        MoveCaretToEndOfText();
                    break;
                case 0x0303: //WM_CLEAR
                    return;
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 处理enter之前的一些事情
        /// </summary>
        private void ShellControl_KeyDown(object sender, KeyEventArgs e)
        {
            //如果插入符号在其他位置,且按下不是enter按键或者ctrl时，将它设置回原位
            if (!IsCaretAtWritablePosition() && !(e.Control || IsTerminatorKey(e.KeyCode)))
            {
                MoveCaretToEndOfText();
            }
            //不允许插入符号移动到提示文字之前
            if (e.KeyCode == Keys.Left && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                //历史命令记录
                if (commandHistory.DoesNextCommandExist())
                {
                    ReplaceTextAtPrompt(commandHistory.GetNextCommand());
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                //历史命令记录
                if (commandHistory.DoesPreviousCommandExist())
                {
                    ReplaceTextAtPrompt(commandHistory.GetPreviousCommand());
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                ////Performs command completion
                //string currentTextAtPrompt = GetTextAtPrompt();
                //string lastCommand = commandHistory.LastCommand;

                //if (lastCommand != null && (currentTextAtPrompt.Length == 0 || lastCommand.StartsWith(currentTextAtPrompt)))
                //{
                //    if (lastCommand.Length > currentTextAtPrompt.Length)
                //    {
                //        this.AddText(lastCommand[currentTextAtPrompt.Length].ToString());
                //    }
                //}
            }
        }
        /// <summary>
        /// 处理空格和enter按键
        /// </summary>
        private void ShellTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //不允许开头空格
            if (e.KeyChar == (char)8 && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
                return;
            }
            //如果是执行按键
            if (IsTerminatorKey(e.KeyChar))
            {
                //如果当前行不是以Prompt开头,则忽略
                if (!IsStartWithPromptAtCurrentLine())
                {
                    e.Handled = true;
                    PrintPrompt();
                    return;
                }
                e.Handled = true;
                string currentCommand = GetTextAtPrompt();
                ExcuteCmd(currentCommand);
            }
        }
        #endregion

        /// <summary>
        /// 执行触发命令
        /// </summary>
        private void ExcuteCmd(string command)
        {
            string currentCommand = command;
            if (currentCommand.Length != 0)
            {
                //PrintLine();     
                commandHistory.Add(currentCommand);
                OnCommandEntered(command);
            }
            else
            {
                PrintLine(1);
                PrintPrompt();
            }
        }
        /// <summary>
        /// 重写OnCommandEntered方法
        /// </summary>
        protected virtual void OnCommandEntered(string command)
        {
            if (CommandEntered != null)
                CommandEntered(command, new CommandEnteredEventArgs(command));
        }

        #region private method
        /// <summary>
        /// 替换提示信息
        /// </summary>
        private void ReplaceTextAtPrompt(string text)
        {
            string currentLine = GetCurrentLine();
            int charactersAfterPrompt = currentLine.Length - _prompt.Length;

            if (charactersAfterPrompt == 0)
                this.AddText(text);
            else
            {
                this.Select(this.TextLength - charactersAfterPrompt, charactersAfterPrompt);
                this.SelectedText = text;
            }
        }
        /// <summary>
        /// 移动插入符号到文末
        /// </summary>
        private void MoveCaretToEndOfText()
        {
            this.SelectionStart = this.TextLength;
            this.ScrollToCaret();
        }
        /// <summary>
        /// 获取当前行
        /// </summary>
        private string GetCurrentLine()
        {
            if (this.Lines.Length > 0)
                return (string)this.Lines.GetValue(this.Lines.GetLength(0) - 1);
            else
                return "";
        }
        /// <summary>
        /// 得到当前插入符号列位置
        /// </summary>
        private int GetCurrentCaretColumnPosition()
        {
            string currentLine = GetCurrentLine();
            int currentCaretPosition = this.SelectionStart;
            return (currentCaretPosition - this.TextLength + currentLine.Length);
        }
        /// <summary>
        /// 得到提示信息处的text
        /// </summary>
        private string GetTextAtPrompt()
        {
            return GetCurrentLine().Substring(_prompt.Length);
        }
        /// <summary>
        /// 判断当前行是否以Prompt开头
        /// </summary>
        private bool IsStartWithPromptAtCurrentLine()
        {
            return GetCurrentLine().Trim().StartsWith(_prompt);
        }
        /// <summary>
        /// 判断插入符号是否在当前行
        /// </summary>
        private bool IsCaretAtCurrentLine()
        {
            return this.TextLength - this.SelectionStart <= GetCurrentLine().Length;
        }
        /// <summary>
        /// 判断插入符号是否在可写位置
        /// </summary>
        private bool IsCaretAtWritablePosition()
        {
            return IsCaretAtCurrentLine() && GetCurrentCaretColumnPosition() >= _prompt.Length;
        }
        /// <summary>
        /// 判断插入符号是否刚要在提示信息之前
        /// </summary>
        private bool IsCaretJustBeforePrompt()
        {
            return IsCaretAtCurrentLine() && GetCurrentCaretColumnPosition() == _prompt.Length;
        }
        /// <summary>
        /// 判断是否是enter按键
        /// </summary>
        private bool IsTerminatorKey(Keys key)
        {
            return key == Keys.Enter;
        }
        /// <summary>
        /// 判断是否是enter按键
        /// </summary>
        private bool IsTerminatorKey(char keyChar)
        {
            return ((int)keyChar) == 13;
        }
        /// <summary>
        /// 添加text
        /// </summary>
        private void AddText(string text)
        {
            this.Text += text;
            MoveCaretToEndOfText();
        }

        private void AddResultText(string result)
        {
            if (_isWin)
            {
                this.Text += result;
            }
            else
            {
                this.Text += result.Replace("\n","\r\n");
            }
            MoveCaretToEndOfText();
        }

        /// <summary>
        /// 打印提示信息
        /// </summary>
        private void PrintPrompt()
        {
            string currentText = this.Text;
            //if (currentText.Length != 0 && currentText[currentText.Length - 1] != '\n')
            if (currentText.Length != 0 && !currentText.EndsWith(Environment.NewLine))
                PrintLine(1);
            {
                this.AddText(_prompt);
            }
        }
        /// <summary>
        /// 换行
        /// </summary>
        private void PrintLine(int t)
        {
            for (int i = 1; i <= t; i++)
            {
                this.AddText(System.Environment.NewLine);
            }
        }
        #endregion

        #region public method
        private delegate void SetPromptTextClient(string val);//代理  
        /// <summary>
        /// 用异步的方式设置新提示信息并且打印
        /// </summary>
        public void SetPromptText(string val)
        {
            //等待异步
            if (this.InvokeRequired)
            {
                SetPromptTextClient c = new SetPromptTextClient(SetPromptText);
                this.Invoke(c, new object[1] { val });//通过代理调用刷新方法
            }
            else
            {
                _prompt = _isWin ? (val + "> ") : ("[" + val + "]$ ");
                PrintPrompt();
            }
        }
        /// <summary>
        /// 获取命令历史记录
        /// </summary>
        public string[] GetCommandHistory()
        {
            return commandHistory.GetCommandHistory();
        }

        private delegate void PrintCommandResultClient(string result);//代理       
        /// <summary>
        /// 用异步的方式打印命令执行的结果（解决多线程调用UI的问题）
        /// </summary>
        public void PrintCommandResult(string result)
        {
            //等待异步
            if (this.InvokeRequired)
            {
                PrintCommandResultClient c = new PrintCommandResultClient(PrintCommandResult);
                this.Invoke(c, new object[1] { result });//通过代理调用刷新方法
            }
            else
            {
                PrintLine(1);
                AddResultText(result ?? "");
                PrintLine(2);
                PrintPrompt();
            }
        }
        
        private delegate void ClearContextClient();//代理
        /// <summary>
        /// 用异步的方式清空文本内容（解决多线程调用UI的问题）
        /// </summary>
        public void ClearContext()
        {
            //等待异步
            if (this.InvokeRequired)
            {
                ClearContextClient c = new ClearContextClient(ClearContext);
                this.Invoke(c);//通过代理调用刷新方法
            }
            else
            {
                string[] sLines = this.Lines;  
                string[] sNewLines = new string[2];  
  
                Array.Copy(sLines,0,sNewLines,0,sNewLines.Length);
                this.Lines = sNewLines;
                PrintLine(2);
                PrintPrompt();
            }
        }
        #endregion
        
        #region 属性
        /// <summary>
        /// 前景色
        /// </summary>
        public Color ShellTextForeColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color ShellTextBackColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        /// <summary>
        /// 字体
        /// </summary>
        public Font ShellTextFont
        {
            get { return this.Font; }
            set { this.Font = value; }
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Prompt
        {
            get { return this._prompt; }
            set
            {                
                this._prompt = _isWin?(value+"> "):("["+value+"]$ ");
            }
        }

        public bool IsWin
        {
            get { return this._isWin; }
            set { this._isWin = value; }
        }

        #endregion

    }
}
