using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Eto;
using Eto.Forms;

namespace Altman.Resources
{
	public class AltLangRes
	{
		//读取默认语言 
		public static string ReadDefaultLanguage()
		{
			XmlReader reader = new XmlTextReader("LanguageDefine.xml");
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);
			XmlNode root = doc.DocumentElement;
			//选取DefaultLangugae节点 
			XmlNode node = root.SelectSingleNode("DefaultLanguage");

			string result = "EN";
			if (node != null)
				//取出节点中的内容 
				result = node.InnerText;

			reader.Close();
			return result;
		}
		//修改默认语言 
		public static void WriteDefaultLanguage(string lang)
		{
			DataSet ds = new DataSet();
			ds.ReadXml("LanguageDefine.xml");
			DataTable dt = ds.Tables["Language"];

			dt.Rows[0]["DefaultLanguage"] = lang;
			ds.AcceptChanges();
			ds.WriteXml("LanguageDefine.xml");
		}

		public static Hashtable Table = new Hashtable();
		private static XmlDocument ReadXml(string lang)
		{
			var langDir = AppEnvironment.AppLanguagePath;
			var doc = new XDocument(new XElement("Resource"));

			var files = Directory.GetFiles(langDir, "*_" + lang + ".xml");
			foreach (var tmp in files.Select(XDocument.Load).Where(tmp => tmp.Root != null))
			{
				doc.Root.Add(tmp.Root.Elements());
			}

			var xmlDoc = new XmlDocument();
			using (var reader = doc.CreateReader())
			{
				xmlDoc.Load(reader);
			}
			return xmlDoc;
		}

		private static void AddValueToHashtable(Hashtable result, XmlNode node)
		{
			var node1 = node.SelectSingleNode("@id");
			var node2 = node.SelectSingleNode("text()");
			if (node1 != null && node2 != null)
			{
				var tmp = node1.InnerText.ToLower();
				if (!result.ContainsKey(tmp))
				{
					result.Add(tmp, node2.InnerText);
				}
			}
		}

		public static void ReadLanguageResource(string lang)
		{
			var result = new Hashtable();
			try
			{
				var doc = ReadXml(lang);
				var root = doc.DocumentElement;
				if (root == null) return;

				var strNodelist = root.SelectNodes("Strings/String");
				if (strNodelist == null) return;
				foreach (XmlNode str in strNodelist)
				{
					AddValueToHashtable(result, str);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Table = result;
		}

		public static void UpdateLanguage(object obj)
		{
			try
			{
				if (obj is Window)
				{
					var form = obj as Window;
					var id = form.ID ?? "";
					//Menu
					if (form.Menu != null)
					{
						UpdateMenu(form.Menu.Items, Table);
					}
					//Title
					if (Table.Contains(id.ToLower()))
						form.Title = (string) Table[id.ToLower()];
				}
				if (obj is Panel)
				{
					var panel = obj as Panel;
					var id = panel.ID ?? "";
					if (panel.ContextMenu != null)
					{
						UpdateMenu(panel.ContextMenu.Items, Table);
					}
					if (panel.Controls != null)
					{
						UpdateControls(panel.Controls, Table);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public static void UpdateLanguage(Form mainForm)
		{
			try
			{
				var id = mainForm.ID ?? "";
				//Menu
				if (mainForm.Menu != null)
				{
					UpdateMenu(mainForm.Menu.Items, Table);
				}
				//Title
				if (Table.Contains(id.ToLower()))
					mainForm.Title = (string)Table[id.ToLower()];
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public static string GetString(string strName, string strDefault)
		{
			if (Table.Contains(strName))
			{
				return (string)Table[strName];
			}
			return strDefault;
		}

		private static void UpdateText(Widget obj, Hashtable table)
		{
			if (obj.ID == null) return;
			var tmp = obj.ID.ToLower();
			if (table.Contains(tmp))
			{
				if (obj is MenuItem)
				{
					(obj as MenuItem).Text = (string)table[tmp];
				}
				else if (obj is TabPage)
				{
					(obj as TabPage).Text = (string)table[tmp];
				}
				if (obj is TextControl)
				{
					(obj as TextControl).Text = (string)table[tmp];
				}
			}
		}

		private static void UpdateMenu(IEnumerable<MenuItem> menuItems, Hashtable table)
		{
			if (menuItems == null) return;
			foreach (var item in menuItems)
			{
				UpdateText(item, table);

				if (item.GetType() == typeof (ButtonMenuItem))
				{
					UpdateMenu((item as ButtonMenuItem).Items, table);
				}
			}
		}

		private static void UpdateControls(IEnumerable<Control> controls, Hashtable table)
		{
			if (controls == null) return;
			foreach (var item in controls)
			{
				if (item is Container)
				{
					if (item is Panel)
					{
						//TabPage.Text
						if (item is TabPage)
						{
							UpdateText(item, table);
						}

						//Panel.ContextMenu
						if ((item as Panel).ContextMenu != null)
						{
							UpdateMenu((item as Panel).ContextMenu.Items, table);
						}
					}
					//Container.Controls
					if ((item as Container).Controls != null)
					{
						UpdateControls((item as Container).Controls, table);
					}
				}
				else if (item is GridView)
				{
					if ((item as GridView).ContextMenu != null)
					{
						UpdateMenu((item as GridView).ContextMenu.Items, table);
					}
				}
				else if (item is TextControl)//Lable,Button,CheckBox
				{
					UpdateText(item, table);
				}
			}
		}
	}
}
