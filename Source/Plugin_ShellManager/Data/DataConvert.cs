using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Altman.Plugin;
using Altman.Webshell.Model;

namespace Plugin_ShellManager.Data
{
	public class DataConvert
	{
		public static Shell ConvertDataRowToShellStruct(DataRow row)
		{
			Shell shell = new Shell();

			shell.Id = row["id"].ToString();
			shell.TargetId = row["target_id"].ToString();
			shell.TargetLevel = row["target_level"].ToString();
			shell.Status = row["status"].ToString();

			shell.ShellUrl = row["shell_url"].ToString();
			shell.ShellPwd = row["shell_pwd"].ToString();
			shell.ShellType = row["shell_type"].ToString();
			shell.ShellExtraString = row["shell_extra_setting"].ToString();
			shell.ServerCoding = row["server_coding"].ToString();
			shell.WebCoding = row["web_coding"].ToString();

			shell.Area = row["area"].ToString();
			shell.Remark = row["remark"].ToString();
			shell.AddTime = row["add_time"].ToString();

			return shell;
		}

		public static PluginParameter ConvertShellStructToPluginParameter(Shell shell)
		{
			var param = new PluginParameter();
			param.AddParameter("id", shell.Id);
			param.AddParameter("target_id", shell.TargetId);
			param.AddParameter("target_level", shell.TargetLevel);
			param.AddParameter("status", shell.Status);

			param.AddParameter("shell_url", shell.ShellUrl);
			param.AddParameter("shell_pwd", shell.ShellPwd);
			param.AddParameter("shell_type", shell.ShellType);
			param.AddParameter("shell_extra_setting", shell.ShellExtraString);
			param.AddParameter("server_coding", shell.ServerCoding);
			param.AddParameter("web_coding", shell.WebCoding);

			param.AddParameter("area", shell.Area);
			param.AddParameter("remark", shell.Remark);
			param.AddParameter("add_time", shell.AddTime);

			return param;
		}
	}
}
