using System;
using System.Collections.Generic;
using System.Xml;

namespace Altman.Logic
{
    internal class ShellExtraHandle
    {
        private static XmlNode GetShellExtraXml(string shellExtraString)
        {
            if (string.IsNullOrEmpty(shellExtraString)) return null;
            XmlNode node = null;
            try
            {
                node = new XmlDocument().CreateElement("ExtraSetting");
                node.InnerXml = shellExtraString;
            }
            catch
            {
                throw new Exception("Sorry,the ini is not legal xml.Please edit the ini firstly.");
            }
            return node;
        }

        public static XmlNode GetHttpHeaderXml(string shellExtraString)
        {
            XmlNode root = GetShellExtraXml(shellExtraString);
            if (root == null) return null;
            return root.SelectSingleNode("/httpHeader") ?? null;
        }

        public static XmlNode GetSqlConnectionXml(string shellExtraString)
        {
            XmlNode root = GetShellExtraXml(shellExtraString);
            if (root == null) return null;
            return root.SelectSingleNode("/sqlConnection") ?? null;
        }

        public static Dictionary<string, string> GetHttpHeaderList(string shellExtraString)
        {
            XmlNode node = GetHttpHeaderXml(shellExtraString);
            Dictionary<string, string> headers = new Dictionary<string, string>();

            if (node == null) return headers;
            XmlNodeList keys = node.SelectNodes("key");
            foreach (XmlNode key in keys)
            {
                if (key.Attributes["name"] == null) continue;
                string name = key.Attributes["name"].Value;
                string value = key.InnerText;
                if (!string.IsNullOrWhiteSpace(name) && !headers.ContainsKey(name))
                {
                    headers.Add(name, value);
                }             
            }
            return headers;
        }
    }
}
