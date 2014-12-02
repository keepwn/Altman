using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Altman.Webshell.Model;
using Eto.Forms;
using Plugin_ShellManager.Data;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager.Actions
{
	public class Import
	{
		private static DataTable GetCaiDaoSite()
		{
			var cmd = "select SITE.ID, " +
			          "SITE.nCodePage, " +
			          "SITE.SiteUrl, " +
			          "SITE.SitePass, " +
			          "SITE.Config, " +
			          "SITE.nScript, " +
			          "SITE.AccessTime, " +
			          "SITE.Note, " +
			          "TYPE.TypeName " +
					  "from SITE, TYPE " +
					  "where SITE.nType = TYPE.ID";
			using (var command = new OleDbCommand(cmd))
			{
				return AccessHelper.GetDataTable(command);
			}
		}

		private static string GetScriptTypeById(int id)
		{
			switch (id)
			{
				case 0:
					return "phpEvel";
				case 1:
					return "aspxEval";
				case 2:
					return "aspEval";
				default:
					return "customize";
			}
		}

		private static Shell ConvertCaiDaoDataRowToShell(DataRow row)
		{
			var shell = new Shell();

			shell.Id = row["ID"].ToString();
			shell.TargetId = row["Note"].ToString();
			shell.TargetLevel = row["TypeName"].ToString();
			shell.Status = "";

			shell.ShellUrl = row["SiteUrl"].ToString();
			shell.ShellPwd = row["SitePass"].ToString();
			shell.ShellType = GetScriptTypeById(Convert.ToInt32(row["nScript"].ToString()));
			shell.ShellExtraString = row["Config"].ToString();
			shell.ServerCoding = "UTF-8";
			shell.WebCoding = "UTF-8";

			shell.Area = "";
			shell.Remark = row["Note"].ToString();
			shell.AddTime = row["AccessTime"].ToString();

			return shell;
		}

		public static void ImportCaidaoShell()
		{
			if (!Eto.EtoEnvironment.Platform.IsWindows)
			{
				MessageBox.Show("Sorry, only supports windows.");
				return;
			}

			var openFileDialog = new OpenFileDialog
			{
				Title = "Select Caidao Databse To Import",
				Filters = new List<IFileDialogFilter> { new FileDialogFilter("Caidao Databses", ".mdb") }
			};
			if (openFileDialog.ShowDialog(Application.Instance.MainForm) == DialogResult.Ok)
			{
				var srcfile = openFileDialog.FileName;
				//var pwd = "密" + (char) 13 + (char) 9 + (char) 10 + "码" + (char) 2;
				var pwd = Encoding.GetEncoding(936).GetString(new byte[] { 0xc3, 0xdc, 0x0d, 0x09, 0x0a, 0xc2, 0xeb, 0x02 });
				var connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;" +
				                               "Data Source={0};" +
				                               "Mode=Share Deny Read|Share Deny Write;Persist Security Info=False;" +
				                               "Jet OLEDB:Database Password=\"{1}\"", srcfile, pwd);
				AccessHelper.ConnString = connString;
				try
				{
					var sites = GetCaiDaoSite();
					var shellList = new List<Shell>();
					foreach (DataRow row in sites.Rows)
					{
						shellList.Add(ConvertCaiDaoDataRowToShell(row));
					}
					foreach (var shell in shellList)
					{
						ShellManager.Insert(shell);
					}
					if (shellList.Count > 0)
					{
						MessageBox.Show(string.Format("Imported {0} Shell(s)", shellList.Count));
						MessageBox.Show(
							StrRes.GetString("StrImportCaidaoShellInfo",
							"Although the data of caidao be imported successfully, but it's scriptType and Config are not compatible with altman, you may still need to modify some data manually."));
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		public static void ImportAltmanShell()
		{
			var openFileDialog = new OpenFileDialog
			{
				Title = "Select Altman Database To Import",
				Filters = new List<IFileDialogFilter> { new FileDialogFilter("Altman Databases", ".db") }
			};
			if (openFileDialog.ShowDialog(Application.Instance.MainForm) == DialogResult.Ok)
			{
				var srcfile = openFileDialog.FileName;
				var connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;" +
											   "Data Source={0};" +
											   "Mode=Share Deny Read|Share Deny Write;Persist Security Info=False;" +
											   "Jet OLEDB:Database Password=\"{1}\"", srcfile, pwd);
				AccessHelper.ConnString = connString;
				try
				{
					var sites = GetCaiDaoSite();
					var shellList = new List<Shell>();
					foreach (DataRow row in sites.Rows)
					{
						shellList.Add(ConvertCaiDaoDataRowToShell(row));
					}
					foreach (var shell in shellList)
					{
						ShellManager.Insert(shell);
					}
					if (shellList.Count > 0)
					{
						MessageBox.Show(string.Format("Imported {0} Shell(s)", shellList.Count));
						MessageBox.Show(
							StrRes.GetString("StrImportCaidaoShellInfo",
							"Although the data of caidao be imported successfully, but it's scriptType and Config are not compatible with altman, you may still need to modify some data manually."));
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}
