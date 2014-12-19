using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Altman.Util.Data;
using Altman.Webshell.Model;
using Eto.Forms;
using Plugin_ShellManager.Data;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager.Actions
{
	public class Import
	{
		#region ImportCaidaoShell

		private static DataTable GetCaiDaoSite()
		{
			var cmd = "SELECT SITE.ID, " +
			          "SITE.nCodePage, " +
			          "SITE.SiteUrl, " +
			          "SITE.SitePass, " +
			          "SITE.Config, " +
			          "SITE.nScript, " +
			          "SITE.AccessTime, " +
			          "SITE.Note, " +
					  "Switch(ISNULL(TYPE.TypeName),'Default',True,TYPE.TypeName) AS TypeName " +
					  "FROM SITE LEFT OUTER JOIN TYPE " +
					  "ON SITE.nType = TYPE.ID";
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

			// convert time
			DateTime time;
			var success = DateTime.TryParse(row["AccessTime"].ToString(), out time);
			if (!success) time = DateTime.Now;
			var timeStr = time.Date.ToShortDateString();
			if (timeStr.Contains("/"))
			{
				timeStr = timeStr.Replace("/", "-");
			}
			shell.AddTime = timeStr;

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
				Filters = {new FileDialogFilter("Caidao Databses", ".mdb")}
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

		#endregion

		#region ImportAltmanShell

		private static DataTable GetAltmanDataTable(string connectionString)
		{
			try
			{
				var sql = string.Format("select * from {0};", "shell");
				return SqliteHelper.ExecuteDataTable(connectionString, sql, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return new DataTable();
		}

		private static Shell ConvertAltmanDataRowToShell(DataRow row)
		{
			var shell = DataConvert.ConvertDataRowToShellStruct(row);
			return shell;
		}

		public static void ImportAltmanShell()
		{
			var openFileDialog = new OpenFileDialog
			{
				Title = "Select Altman Database To Import",
				Filters = {new FileDialogFilter("Altman Databases", ".db3")}
			};
			if (openFileDialog.ShowDialog(Application.Instance.MainForm) == DialogResult.Ok)
			{
				var srcfile = openFileDialog.FileName;
				var connString = string.Format("Data Source={0}", srcfile);
				try
				{
					var datas = GetAltmanDataTable(connString);
					var shellList = new List<Shell>();
					foreach (DataRow row in datas.Rows)
					{
						shellList.Add(ConvertAltmanDataRowToShell(row));
					}
					foreach (var shell in shellList)
					{
						ShellManager.Insert(shell);
					}
					if (shellList.Count > 0)
					{
						MessageBox.Show(string.Format("Imported {0} Shell(s)", shellList.Count));
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		#endregion

		#region ImportAltmanShellFromXml

		private static string GetAltmanXml(string path)
		{
			var xmldoc = new XmlDocument();
			xmldoc.Load(path);
			return xmldoc.OuterXml;
		}

		private static DataTable ConvertXmlToDataTable(string xmlData)
		{
			StringReader stream = null;
			XmlTextReader reader = null;
			try
			{
				var ds = new DataSet();
				stream = new StringReader(xmlData);
				reader = new XmlTextReader(stream);
				ds.ReadXml(reader);
				return ds.Tables[0];
			}
			catch (Exception ex)
			{
				return new DataTable();
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		public static void ImportAltmanShellFromXml()
		{
			var openFileDialog = new OpenFileDialog
			{
				Title = "Select Altman Xml To Import",
				Filters = {new FileDialogFilter("Altman Xml", ".xml")}
			};
			if (openFileDialog.ShowDialog(Application.Instance.MainForm) == DialogResult.Ok)
			{
				var srcfile = openFileDialog.FileName;
				try
				{
					var xmlString = GetAltmanXml(srcfile);
					var datas = ConvertXmlToDataTable(xmlString);
					var shellList = new List<Shell>();
					foreach (DataRow row in datas.Rows)
					{
						shellList.Add(ConvertAltmanDataRowToShell(row));
					}
					foreach (var shell in shellList)
					{
						ShellManager.Insert(shell);
					}
					if (shellList.Count > 0)
					{
						MessageBox.Show(string.Format("Imported {0} Shell(s)", shellList.Count));
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		#endregion
	}
}
