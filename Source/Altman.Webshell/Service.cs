using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Altman.Util.Web;
using Altman.Webshell.Core;
using Altman.Webshell.Model;

namespace Altman.Webshell
{
	public class Service
	{
		/// <summary>
		/// 提交命令
		/// </summary>
		/// <param name="data">shellstruct数据</param>
		/// <param name="funcNameXpath">xpath表示的方法名（/cmder/readfile）</param>
		/// <param name="param"></param>
		public static byte[] SubmitCommand(Shell data, string funcNameXpath, string[] param)
		{
			var shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
			var customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
			var commandCode = customCommandCode.GetCode(funcNameXpath, param);

			var httpClient = new HttpClient(GetHttpHeader(data));
			return httpClient.SubmitCommandByPost(data.ShellUrl, commandCode);
		}
		public static byte[] SubmitCommand(Shell data,
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

		public static string GetCustomShellTypeServerCode(string shellTypeName)
		{
			if (CustomShellTypeProvider.ShellTypeStyleContainer.ContainsKey(shellTypeName))
			{
				CustomShellType shelltype = CustomShellTypeProvider.ShellTypeStyleContainer[shellTypeName];
				return shelltype.BasicSetting.ServiceExample;
			}
			return null;
		}

		public static string[] GetCustomShellTypeNameList()
		{
			return CustomShellTypeProvider.ShellTypeStyleContainer.Keys.ToArray();
		}

		public static string[] GetDbNodeInfoList(string shellTypeName)
		{
			var funcCodeNameList = new List<string>();
			if (CustomShellTypeProvider.ShellTypeStyleContainer.ContainsKey(shellTypeName))
			{
				CustomShellType shelltype = CustomShellTypeProvider.ShellTypeStyleContainer[shellTypeName];
				FuncTreeNode dbNode = shelltype.FuncTreeRoot.FindNodes("/DbManager");
				if (dbNode != null)
				{
					funcCodeNameList.AddRange(dbNode.Nodes.Select(child => child.Value.Info));
				}
			}
			return funcCodeNameList.ToArray();
		}

		public static string[] GetShellSqlConnection(Shell data)
		{
			var shellExtra = new ShellExtra(data.ShellExtraString);
			return shellExtra.SqlConnection;
		}

		public static WebHeaderCollection GetHttpHeader(Shell data)
		{
			var shellExtra = new ShellExtra(data.ShellExtraString);
			return shellExtra.HttpHeader;
		}

		//public void RegisterService(IPlugin plugin)
		//{
		//	PluginServiceProvider.RegisterService<Func<Shell, string, string[], byte[]>>
		//		(plugin, "SubmitCommand", SubmitCommand);
		//	PluginServiceProvider.RegisterService<Func<Shell, string, string[], bool, bool, byte[]>>
		//		(plugin, "SubmitCommandWithEvent", SubmitCommand);
		//	PluginServiceProvider.RegisterService<Func<string, string>>
		//		(plugin, "GetCustomShellTypeServerCode", GetCustomShellTypeServerCode);
		//	PluginServiceProvider.RegisterService<Func<string[]>>
		//		(plugin, "GetCustomShellTypeNameList", GetCustomShellTypeNameList);
		//	PluginServiceProvider.RegisterService<Func<string, string[]>>
		//		(plugin, "GetDbNodeInfoList", GetDbNodeInfoList);
		//	PluginServiceProvider.RegisterService<Func<Shell, string[]>>
		//		(plugin, "GetShellSqlConnection", GetShellSqlConnection);
		//}
	}
}
