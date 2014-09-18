using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Eto;
using Eto.Forms;

namespace Altman.Desktop
{
	public class Language
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


		private static Hashtable _table = new Hashtable();


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

		private static string AddValueToHashtable(Hashtable result, XmlNode node, string formId)
		{
			var id = string.Empty;
			var node1 = node.SelectSingleNode("@id");
			var node2 = node.SelectSingleNode("@text");
			if (node1 != null && node2 != null)
			{
				var tmp = (formId + node1.InnerText).ToLower();
				if (!result.ContainsKey(tmp))
				{
					result.Add(tmp, node2.InnerText);
					id = node1.InnerText;
				}
			}
			return id;
		}

		public static void ReadLanguageResource(string lang)
		{
			var result = new Hashtable();
			try
			{
				var doc = ReadXml(lang);
				var root = doc.DocumentElement;
				if (root == null) return;

				#region Form || Plugin.Form
				var formNodelist = root.SelectNodes("Form|Plugin.Form");
				if (formNodelist == null) return;
				foreach (XmlNode formNode in formNodelist)
				{
					//Form
					var id = AddValueToHashtable(result, formNode, "");

					//MenuBar
					var menuNodes = formNode.SelectNodes("MenuBar/MenuItem");
					if (menuNodes == null) return;
					foreach (XmlNode menuNode in menuNodes)
					{
						AddValueToHashtable(result, menuNode, id);
					}

					//Controls
					var controlNodelist = formNode.SelectNodes("Controls/Control");
					if (controlNodelist == null) return;
					foreach (XmlNode controlNode in controlNodelist)
					{
						AddValueToHashtable(result, controlNode, id);
					}
				}
				#endregion

				#region Plugin.Panel
				var pluginPanelNodelist = root.SelectNodes("Plugin.Panel");
				if (pluginPanelNodelist == null) return;
				foreach (XmlNode pluginNode in pluginPanelNodelist)
				{
					//Panel
					var id = AddValueToHashtable(result, pluginNode, "");

					//ContextMenu
					var menuNodes = pluginNode.SelectNodes("ContextMenu/MenuItem");
					if (menuNodes == null) return;
					foreach (XmlNode menuNode in menuNodes)
					{
						AddValueToHashtable(result, menuNode, id);
					}

					//Controls
					var controlNodelist = pluginNode.SelectNodes("Controls/Control");
					if (controlNodelist == null) return;
					foreach (XmlNode controlNode in controlNodelist)
					{
						AddValueToHashtable(result, controlNode, id);
					}
				}
				#endregion

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			_table = result;
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
						UpdateMenu(id, form.Menu.Items, _table);
					}
					//Title
					if (_table.Contains(id.ToLower()))
						form.Title = (string) _table[id.ToLower()];
				}
				if (obj is Panel)
				{
					var panel = obj as Panel;
					var id = panel.ID ?? "";
					if (panel.ContextMenu != null)
					{
						UpdateMenu(id, panel.ContextMenu.Items, _table);
					}
					if (panel.Controls != null)
					{
						UpdateControls(id, panel.Controls, _table);
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
					UpdateMenu(id, mainForm.Menu.Items, _table);
				}
				//Title
				if (_table.Contains(id.ToLower()))
					mainForm.Title = (string)_table[id.ToLower()];
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private static void UpdateText(string id, Widget obj, Hashtable table)
		{
			if (obj.ID == null) return;
			var tmp = (id + obj.ID).ToLower();
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

		private static void UpdateMenu(string id, IEnumerable<MenuItem> menuItems, Hashtable table)
		{
			if (menuItems == null) return;
			foreach (var item in menuItems)
			{
				UpdateText(id, item, table);

				if (item.GetType() == typeof (ButtonMenuItem))
				{
					UpdateMenu(id, (item as ButtonMenuItem).Items, table);
				}
			}
		}

		private static void UpdateControls(string id, IEnumerable<Control> controls, Hashtable table)
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
							UpdateText(id, item, table);
						}

						//Panel.ContextMenu
						if ((item as Panel).ContextMenu != null)
						{
							UpdateMenu(id, (item as Panel).ContextMenu.Items, table);
						}
					}
					//Container.Controls
					if ((item as Container).Controls != null)
					{
						UpdateControls(id, (item as Container).Controls, table);
					}
				}
				else if (item is GridView)
				{
					if ((item as GridView).ContextMenu != null)
					{
						UpdateMenu(id, (item as GridView).ContextMenu.Items, table);
					}
				}
				else if (item is TextControl)//Lable,Button,CheckBox
				{
					UpdateText(id, item, table);
				}
			}
		}
	}
}
