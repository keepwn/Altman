using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.LogicCore;
using Altman.ModelCore;
using Altman.WebCore;
using PluginFramework;

namespace Altman
{
    public class CoreService : ICoreInHostService
    {
        private FormMain _mainForm;
        public CoreService(FormMain mainForm)
        {
            _mainForm = mainForm;
        }

        /// <summary>
        /// 提交命令
        /// </summary>
        /// <param name="data">shellstruct数据</param>
        /// <param name="funcNameXpath">xpath表示的方法名（/cmder/readfile）</param>
        /// <param name="param"></param>
        public byte[] SubmitCommand(ShellStruct data, string funcNameXpath, string[] param)
        {
            CustomShellType shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
            CustomCommandCode customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
            Dictionary<string, string> commandCode = customCommandCode.GetCode(funcNameXpath, param);
            HttpClient httpClient = new HttpClient();
            return httpClient.SubmitCommandByPost(data.ShellUrl, commandCode);
        }
        public byte[] SubmitCommand(ShellStruct data,
                                string funcNameXpath, string[] param,
                                bool isBindUploadProgressChangedEvent,
                                bool isBindDownloadProgressChangedEvent)
        {
            CustomShellType shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
            CustomCommandCode customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
            Dictionary<string, string> commandCode = customCommandCode.GetCode(funcNameXpath, param);
            HttpClient httpClient = new HttpClient();
            //if (isBindUploadProgressChangedEvent)
            //    httpClient.UploadFileProgressChangedToDo += httpClient_UploadFileProgressChangedToDo;
            //if (isBindDownloadProgressChangedEvent)
            //    httpClient.DownloadFileProgressChangedToDo += httpClient_DownloadFileProgressChangedToDo;
            return httpClient.SubmitCommandByPost(data.ShellUrl, commandCode);
        }

        public string GetCustomShellTypeServerCode(string shellTypeName)
        {
            return InitUi.GetCustomShellTypeServerCode(shellTypeName);
        }

        public string[] GetCustomShellTypeNameList()
        {
            return InitUi.GetCustomShellTypeNameList();
        }

        public string[] GetDbNodeFuncCodeNameList(string shellTypeName)
        {
            return InitUi.GetDbNodeFuncCodeNameList(shellTypeName);
        }
        public IEnumerable<IPlugin> GetPlugins()
        {
            if (_mainForm.PluginsImport == null)
            {
                return null;
            }
            return _mainForm.PluginsImport.Plugins;
        }
    }
}
