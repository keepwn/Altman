using System.Collections.Generic;
using System.Xml;
using Altman.Desktop.Forms;
using Altman.Logic;
using Altman.Model;
using Altman.Web;
using PluginFramework;

namespace Altman.Desktop.Service
{
    public class Core : IHostCore
    {
        private FormMain _mainForm;
        public Core(FormMain mainForm)
        {
            _mainForm = mainForm;
        }

        /// <summary>
        /// 提交命令
        /// </summary>
        /// <param name="data">shellstruct数据</param>
        /// <param name="funcNameXpath">xpath表示的方法名（/cmder/readfile）</param>
        /// <param name="param"></param>
        public byte[] SubmitCommand(Shell data, string funcNameXpath, string[] param)
        {
            CustomShellType shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
            CustomCommandCode customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
            Dictionary<string, string> commandCode = customCommandCode.GetCode(funcNameXpath, param);

            ShellExtra shellExtra = new ShellExtra(data.ShellExtraString);
            HttpClient httpClient = new HttpClient(shellExtra.HttpHeader);
            return httpClient.SubmitCommandByPost(data.ShellUrl, commandCode);
        }
        public byte[] SubmitCommand(Shell data,
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

        public XmlNode GetShellHttpHeader(Shell data)
        {
            return ShellExtraHandle.GetHttpHeaderXml(data.ShellExtraString);
        }

        public XmlNode GetShellSqlConnection(Shell data)
        {
            return ShellExtraHandle.GetSqlConnectionXml(data.ShellExtraString);
        }
    }
}
