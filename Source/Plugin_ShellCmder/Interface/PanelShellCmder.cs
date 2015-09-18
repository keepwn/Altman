using System;
using System.Threading;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Util.Common.AltData;
using Altman.Webshell.Model;
using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellCmder.Controls;

namespace Plugin_ShellCmder.Interface
{
    public partial class PanelShellCmder : Panel
    {
        private IHost _host;
        private ShellCmder _shellCmder;
        private Shell _shellData;
        private InternalCommand _internalCommand;

        private bool _isWin;
        private string _currentDir;

        public PanelShellCmder(IHost host, PluginParameter data)
        {
            Init();
            _host = host;
			_shellData = (Shell)data[0];

            _consoleBoxCmder.CommandEntered+=ConsoleBoxCmderCommandEntered;
            _consoleBoxCmder.Prompt = "AltmanCmder";
            ConnectOneShell();
        }

        /// <summary>
		/// Enter events
        /// </summary>
        private void ConsoleBoxCmderCommandEntered(object sender, ConsoleBox.CommandEnteredEventArgs e)
        {
            string command = e.Command;
            var thread = new Thread(() => ExecuteCmd(command));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
        
		/// <summary>
		/// Connect shell
        /// </summary>
        private void ConnectOneShell()
        {
		    try
		    {
		        //初始化ShellCmder
		        _shellCmder = new ShellCmder(_host, _shellData);
		        //初始化内部命令
		        _internalCommand = new InternalCommand(_consoleBoxCmder, _shellCmder);
		        //获取系统信息
		        var info = _shellCmder.GetSysInfo();
		        var str = string.Format("{0}: {1}\n{2}: {3}",
		            _host.Ui.GetTranslatedText("StrPlatform", "Platform"),
		            info.Platform,
		            _host.Ui.GetTranslatedText("StrCurrentUser", "CurrentUser"),
		            info.CurrentUser);
		        //设置系统平台
		        _isWin = info.DirSeparators == @"\";
		        //设置当前目录
		        _currentDir = info.ShellDir;
		        //cmder的系统平台
		        _consoleBoxCmder.IsWin = _isWin;
		        //设置提示信息
		        _consoleBoxCmder.Prompt = _currentDir;
		        _consoleBoxCmder.PrintCommandResult(str);
                _host.Ui.ShowMsgInStatusBar("Connect success");
		    }
		    catch (Exception ex)
		    {
		        _consoleBoxCmder.PrintCommandResult(ex.Message);
                _host.Ui.ShowMsgInStatusBar("Connect failed");
		    }
        }

        /// <summary>
		/// Execute cmd
        /// </summary>
        private void ExecuteCmd(string command)
        {
            if (!_internalCommand.ProcessInternalCommand(command))
            {
                try
                {
                    _host.Ui.ShowMsgInStatusBar("Executing...", true);

                    if (_shellCmder != null)
                    {
                        CmdResult cmdResult = _shellCmder.ExecuteCmd("", command, _currentDir, _isWin);
                        //设置当前目录
                        _currentDir = cmdResult.CurrentDir;
                        //设置提示信息
                        _consoleBoxCmder.Prompt = _currentDir;
                        _consoleBoxCmder.PrintCommandResult(cmdResult.Result);
                        _host.Ui.ShowMsgInStatusBar("Execute success", false);
                    }
                }
                catch (Exception ex)
                {
                    _consoleBoxCmder.PrintCommandResult("[Error]:"+ex.Message);
                    _host.Ui.ShowMsgInStatusBar("Execute failed", false);
                }
            }
        }
    }
}
