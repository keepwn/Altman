using System;
using System.Text;

using Altman.Controls;

namespace Plugin_ShellCmder
{
    public class InternalCommand
    {
        private TextBoxShell shellTextBoxCmder;
        private ShellCmderService cmdshell;
        public InternalCommand(TextBoxShell shellTextBoxCmder, ShellCmderService cmdshell)
        {
            this.shellTextBoxCmder = shellTextBoxCmder;
            this.cmdshell = cmdshell;
        }

        private void Cls()
        {
            shellTextBoxCmder.ClearContext();
        }
        private void Help()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SECTool ShellCmder Demo");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("*******************************************");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("Commands Available");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("sec       - view or edit settings");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("history   - prints history of entered commands");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("cls       - clears the screen");
            stringBuilder.Append(System.Environment.NewLine);

            shellTextBoxCmder.PrintCommandResult(stringBuilder.ToString());
        }
        private void History()
        {
            string[] commands = shellTextBoxCmder.GetCommandHistory();
            StringBuilder stringBuilder = new StringBuilder(commands.Length);
            foreach (string s in commands)
            {
                stringBuilder.Append(s);
                stringBuilder.Append(System.Environment.NewLine);
            }
            shellTextBoxCmder.PrintCommandResult(stringBuilder.ToString());
        }
        private void Sec()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Please Input More.Such As:");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("Sec View");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("Sec Set [oldSettingName] [newSetting]");
            stringBuilder.Append(System.Environment.NewLine);
            shellTextBoxCmder.PrintCommandResult(stringBuilder.ToString());
        }
        private void SecView()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("基本配置信息");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("*******************************************");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("shellUrl      ->  " + cmdshell.ShellUrl);
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("shellPwd      ->  " + cmdshell.ShellPwd);
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("shellType     ->  " + cmdshell.ShellType);
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("shellCoding   ->  " + cmdshell.ShellCoding);
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("shellTimeOut  ->  " + cmdshell.ShellTimeOut);
            stringBuilder.Append(System.Environment.NewLine);
            shellTextBoxCmder.PrintCommandResult(stringBuilder.ToString());
        }
        private void SecSet(string oldSettingName, string newSetting)
        {
            bool flag = false;
            if (oldSettingName == "shellurl")
            {
                cmdshell.ShellUrl = newSetting;
                flag = true;
            }
            else if (oldSettingName == "shellpwd")
            {
                cmdshell.ShellPwd = newSetting;
                flag = true;
            }
            else if (oldSettingName == "shelltype")
            {
                cmdshell.ShellType=newSetting;
                flag = true;
            }
            else if (oldSettingName == "shellcoding")
            {
                cmdshell.ShellCoding = newSetting;
                flag = true;
            }
            else if (oldSettingName == "shelltimetype")
            {
                cmdshell.ShellTimeOut = int.Parse(newSetting);
                flag = true;
            }
            if (flag)
            {
                shellTextBoxCmder.PrintCommandResult("设置成功");
            }
            else
            {
                shellTextBoxCmder.PrintCommandResult("设置失败");
            }
        }

        /// <summary>
        /// 内部命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public bool ProcessInternalCommand(string cmd)
        {
            cmd = cmd.ToLower();
            if (cmd=="cls")
            {
                Cls();
            }
            else if (cmd == "help")
            {
                Help();
            }
            else if (cmd == "history")
            {
                History();
            }
            else if (cmd.StartsWith("sec"))
            {
                string[] parts = cmd.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length ==2 &&parts[1] == "view")
                {
                    SecView();
                }
                else if (parts.Length == 4)
                {
                    SecSet(parts[2], parts[3]);
                }
                else
                {
                    Sec();
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
