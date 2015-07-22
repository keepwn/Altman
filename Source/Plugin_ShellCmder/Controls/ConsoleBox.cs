using System;
using System.Collections;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_ShellCmder.Controls
{
    public class ConsoleBox : TextArea
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
        public ConsoleBox()
        {
            this.KeyDown += CmdBox_KeyDown;
            this.Wrap = true;
        }

        #region 按键事件处理
        void CmdBox_KeyDown(object sender, KeyEventArgs e)
        {
            //如果插入符号在其他位置,且按下不是enter按键或者ctrl时，将它设置回原位
            if (!IsCaretAtWritablePosition() && !(e.Control || IsTerminatorKey(e.Key)))
            {
                MoveCaretToEndOfText();
            }
			//不允许退格符移动到提示文字之前
			if (e.Key == Keys.Backspace && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
            }
            //不允许插入符号移动到提示文字之前
            if (e.Key == Keys.Left && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
            }
	        if (e.Key == Keys.Space && IsCaretJustBeforePrompt())
	        {
				e.Handled = true;
	        }
            else if (e.Key == Keys.Down)
            {
                //历史命令记录
                if (commandHistory.DoesNextCommandExist())
                {
                    ReplaceTextAtPrompt(commandHistory.GetNextCommand());
                }
                e.Handled = true;
            }
            else if (e.Key == Keys.Up)
            {
                //历史命令记录
                if (commandHistory.DoesPreviousCommandExist())
                {
                    ReplaceTextAtPrompt(commandHistory.GetPreviousCommand());
                }
                e.Handled = true;
            }
            else if (e.Key == Keys.Right)
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

            //处理空格和enter按键
            //如果是执行按键
            if (IsTerminatorKey(e.Key))
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
                this.Selection = new Range<int>(this.Text.Length - charactersAfterPrompt, this.Text.Length);
                this.SelectedText = text;
            }
        }
        /// <summary>
        /// 移动插入符号到文末
        /// </summary>
        private void MoveCaretToEndOfText()
        {
            this.CaretIndex = this.Text.Length;
        }

        private string[] Lines
        {
            get
            {
                string text = this.Text;
                var arrayList = new ArrayList();
                int j;
                for (int i = 0; i < text.Length; i = j)
                {
                    for (j = i; j < text.Length; j++)
                    {
                        char c = text[j];
                        if (c == '\r' || c == '\n')
                        {
                            break;
                        }
                    }
                    string value = text.Substring(i, j - i);
                    arrayList.Add(value);
                    if (j < text.Length && text[j] == '\r')
                    {
                        j++;
                    }
                    if (j < text.Length && text[j] == '\n')
                    {
                        j++;
                    }
                }
                if (text.Length > 0 && (text[text.Length - 1] == '\r' || text[text.Length - 1] == '\n'))
                {
                    arrayList.Add("");
                }
                return (string[])arrayList.ToArray(typeof(string));
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder(value[0]);
                    for (int i = 1; i < value.Length; i++)
                    {
                        stringBuilder.Append("\r\n");
                        stringBuilder.Append(value[i]);
                    }
                    this.Text = stringBuilder.ToString();
                    return;
                }
				
                this.Text = "";
            }
        }

        /// <summary>
        /// 获取当前行
        /// </summary>
        private string GetCurrentLine()
        {
            string[] lines = this.Lines;

            if (lines.Length > 0)
                return lines[lines.GetLength(0) - 1];
            else
                return "";
        }
        /// <summary>
        /// 得到当前插入符号的位置
        /// </summary>
        private int GetCurrentCaretColumnPosition()
        {
            //return this.CaretIndex;
            string currentLine = GetCurrentLine();
            int currentCaretPosition = this.Selection.Start;
            return (currentCaretPosition - this.Text.Length + currentLine.Length);
        }
        /// <summary>
        /// 得到提示信息处的text
        /// </summary>
        private string GetTextAtPrompt()
        {
			//去除开始的空白字符
            return GetCurrentLine().Substring(_prompt.Length).TrimStart();
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
            return this.Text.Length - this.Selection.Start <= GetCurrentLine().Length;
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
	        this.Append(text,true);
            MoveCaretToEndOfText();
        }

        private void AddResultText(string result)
        {
	        AddText(_isWin ? result : result.Replace("\n", "\r\n"));
        }

	    /// <summary>
        /// 打印提示信息
        /// </summary>
        private void PrintPrompt()
        {
            string currentText = this.Text;
            if (currentText.Length != 0 && !currentText.EndsWith(Environment.NewLine))
                PrintLine(1);
            this.AddText(_prompt);
        }
        /// <summary>
        /// 换行
        /// </summary>
        private void PrintLine(int t)
        {
            for (int i = 1; i <= t; i++)
            {
                this.AddText(Environment.NewLine);
            }
        }
        #endregion

        #region public method
        /// <summary>
        /// 设置新提示信息并且打印
        /// </summary>
        public void SetPromptText(string val)
        {
            _prompt = _isWin ? (val + "> ") : ("[" + val + "]$ ");
            PrintPrompt();
        }
        /// <summary>
        /// 获取命令历史记录
        /// </summary>
        public string[] GetCommandHistory()
        {
            return commandHistory.GetCommandHistory();
        }
     
        /// <summary>
        /// 用异步的方式打印命令执行的结果（解决多线程调用UI的问题）
        /// </summary>
        public void PrintCommandResult(string result)
        {
            Application.Instance.Invoke(() =>
            {
                PrintLine(1);
                AddResultText(result ?? "");
                PrintLine(2);
                PrintPrompt();
            });
        }

        /// <summary>
        /// 用异步的方式清空文本内容（解决多线程调用UI的问题）
        /// </summary>
        public void ClearContext()
        {
            Application.Instance.Invoke(() =>
            {
                string[] sLines = this.Lines;
                string[] sNewLines = new string[2];

                Array.Copy(sLines, 0, sNewLines, 0, sNewLines.Length);
                this.Lines = sNewLines;

                PrintLine(2);
                PrintPrompt();
            });
        }
        #endregion

        #region 属性
        public Color ShellTextForeColor
        {
            get { return this.TextColor; }
            set { this.TextColor = value; }
        }

        public Color ShellTextBackColor
        {
            get { return this.BackgroundColor; }
            set { this.BackgroundColor = value; }
        }

        public Font ShellTextFont
        {
            get { return this.Font; }
            set { this.Font = value; }
        }

        public string Prompt
        {
            get { return this._prompt; }
            set
            {
                this._prompt = _isWin ? (value + "> ") : ("[" + value + "]$ ");
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
