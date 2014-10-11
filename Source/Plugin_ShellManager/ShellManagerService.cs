using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Altman.Model;
using Altman.Web;
using PluginFramework;
using Plugin_ShellManager.Core;
using Plugin_ShellManager.Data;
using Plugin_ShellManager.Model;

namespace Plugin_ShellManager
{
	public class ShellManagerService
	{
		/// <summary>
		/// 提交命令
		/// </summary>
		/// <param name="data">shellstruct数据</param>
		/// <param name="funcNameXpath">xpath表示的方法名（/cmder/readfile）</param>
		/// <param name="param"></param>
		public byte[] SubmitCommand(Shell data, string funcNameXpath, string[] param)
		{
			var shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
			var customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
			var commandCode = customCommandCode.GetCode(funcNameXpath, param);

			var httpClient = new HttpClient(GetHttpHeader(data));
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
			return InitWorker.GetCustomShellTypeServerCode(shellTypeName);
		}

		public string[] GetCustomShellTypeNameList()
		{
			return InitWorker.GetCustomShellTypeNameList();
		}

		public string[] GetDbNodeInfoList(string shellTypeName)
		{
			return InitWorker.GetDbNodeFuncCodeNameList(shellTypeName);
		}

		public string[] GetShellSqlConnection(Shell data)
		{
			var shellExtra = new ShellExtra(data.ShellExtraString);
			return shellExtra.SqlConnection;
		}

		public WebHeaderCollection GetHttpHeader(Shell data)
		{
			var shellExtra = new ShellExtra(data.ShellExtraString);
			return shellExtra.HttpHeader;
		}

		public void RegisterService()
		{
			PluginServiceProvider.RegisterService<Func<Shell, string, string[], byte[]>>
				("SubmitCommand", SubmitCommand);
			PluginServiceProvider.RegisterService<Func<Shell, string, string[], bool, bool, byte[]>>
				("SubmitCommandWithEvent", SubmitCommand);
			PluginServiceProvider.RegisterService<Func<string, string>>
				("GetCustomShellTypeServerCode", GetCustomShellTypeServerCode);
			PluginServiceProvider.RegisterService<Func<string[]>>
				("GetCustomShellTypeNameList", GetCustomShellTypeNameList);
			PluginServiceProvider.RegisterService<Func<string, string[]>>
				("GetDbNodeInfoList", GetDbNodeInfoList);
			PluginServiceProvider.RegisterService<Func<Shell, string[]>>
				("GetShellSqlConnection", GetShellSqlConnection);
		}
	}
}
