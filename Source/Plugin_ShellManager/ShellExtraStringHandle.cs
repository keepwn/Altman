using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Altman.Util.Share;

namespace Plugin_ShellManager
{
	public class ShellExtraStringHandle
	{
		static ShellExtraStringHandle()
		{
			IniSettings.CommentChars = new[] {"#"};
			IniSettings.DefaultValueFormatting = "? = $		;";
			IniSettings.SeparateHeader = false;
			IniSettings.AllowInlineComments = false;
		}

		public static string CreateDefaultIniString(IEnumerable<string> conns)
		{
			var ini = new Ini();
			ini.Header = "Shell高级设置\n以#开头则为注释行";
			ini["HttpHeader"].Comment = "设置全局Http头\n键值可以自己添加，e.g. Accept:*/*";
			ini["HttpHeader"]["Accept"] = "*/*";
			ini["HttpHeader"]["User-Agent"] = "IE9.0";
			ini["HttpHeader"]["Cookie"] = "Test:Test123";

			ini["Post"].Comment = "设置提交的Post包(正在开发中)";
			ini["Post"]["post"] = "A=a&B=b&C=c";

			ini["SqlConnection"].Comment = "设置数据库连接\n" +
											"type为数据库类型\n" +
											"conn为连接字符串";
			//ini["SqlConnection"]["type"] = "mysql";
			//ini["SqlConnection"]["conn"] = "127.0.0.1;root;password;utf-8;";

			foreach (var conn in conns)
			{
				foreach (var i in conn.Split(new [] {"\n", "\r\n"},StringSplitOptions.RemoveEmptyEntries))
				{
					ini["SqlConnection"].InsertComment(i);
				}
			}

			return ini.OutputString();
		}

		[Obsolete]
		public static string ConvertIniStringToXmlString(string iniString)
		{
			var xmlDoc = new XmlDocument();
			var root = xmlDoc.CreateElement("root");
			var ini = Ini.FromString(iniString);

			foreach (var section in ini.Sections)
			{
				if (section.Comment != null)
				{
					root.AppendChild(xmlDoc.CreateComment(section.Comment));
				}

				var node = xmlDoc.CreateElement(section.SectionStart.SectionName);				
				foreach (var i in section.Elements)
				{
					XmlNode ele = null;
					if (i is IniBlankLine)
					{					
					}
					else if (i is IniCommentary)
					{
						ele = xmlDoc.CreateComment((i as IniCommentary).Comment);
					}
					else if (i is IniValue)
					{
						ele = xmlDoc.CreateElement((i as IniValue).Key);
						ele.InnerText = (i as IniValue).Value;
					}
					if(ele!=null)
						node.AppendChild(ele);
				}
				root.AppendChild(node);
			}
			return root.InnerXml;
		}

		public static string[] GetShellSqlConnection(string extraString)
		{
			var ini = Ini.FromString(extraString);
			return new[] { ini["SqlConnection"]["type"], ini["SqlConnection"]["conn"] };
		}
	}
}
