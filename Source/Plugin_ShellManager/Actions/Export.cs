using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Altman.Util.Data;
using Altman.Webshell.Model;
using Eto.Forms;

namespace Plugin_ShellManager.Actions
{
	public class Export
	{
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

		private static string ConvertAltmanDataTableToXml(DataTable dt)
		{
			MemoryStream stream = null;
			XmlTextWriter writer = null;
			try
			{
				stream = new MemoryStream();
				writer = new XmlTextWriter(stream, Encoding.Default);
				writer.Formatting = Formatting.Indented; 
				dt.WriteXml(writer);
				var count = (int)stream.Length;
				var arr = new byte[count];
				stream.Seek(0, SeekOrigin.Begin);
				stream.Read(arr, 0, count);
				return new UTF8Encoding().GetString(arr);
			}
			catch
			{
				return String.Empty;
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		private static void SaveStringToXml(string path, string str)
		{
			var sw = new StreamWriter(path, false);
			sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			sw.WriteLine(str);
			sw.Close();
		}

		public static void ExportShell()
		{
			var saveFileDialog = new SaveFileDialog
			{
				Title = "Export to ...",
				Filters = {new FileDialogFilter("Altman Xml", ".xml")}
			};

			if (saveFileDialog.ShowDialog(Application.Instance.MainForm) == DialogResult.Ok)
			{
				var srcfile = saveFileDialog.FileName;
				var connString = string.Format("Data Source={0}", Path.Combine(ShellManager.Host.App.AppCurrentDir, "data.db3"));
				try
				{
					var dt = GetAltmanDataTable(connString);
					if (dt.Rows.Count > 0)
					{
						var xmlString = ConvertAltmanDataTableToXml(dt);
						SaveStringToXml(srcfile, xmlString);

						MessageBox.Show(string.Format("Export {0} Shell(s)", dt.Rows.Count));
					}
					else
					{
						MessageBox.Show("No shell be exported");
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
