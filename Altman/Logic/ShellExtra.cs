using System.Net;
using System.Xml;

namespace Altman.Logic
{
    internal class ShellExtra
    {
        private string _shellExtraString;
        public ShellExtra(string shellExtraString)
        {
            this._shellExtraString = shellExtraString;
        }

        public XmlNode HttpHeaderXml
        {
            get { return ShellExtraHandle.GetHttpHeaderXml(_shellExtraString);}
        }

        public XmlNode SqlConnectionXml
        {
            get { return ShellExtraHandle.GetSqlConnectionXml(_shellExtraString); }
        }

        public WebHeaderCollection HttpHeader
        {
            get
            {
                WebHeaderCollection header = new WebHeaderCollection();
                foreach (var i in ShellExtraHandle.GetHttpHeaderList(_shellExtraString))
                {
                    header.Add(i.Key, i.Value);
                }
                return header;
            }
        }
    }
}