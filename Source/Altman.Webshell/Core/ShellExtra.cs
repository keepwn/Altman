using System.Net;

namespace Altman.Webshell.Core
{
    public class ShellExtra
    {
        private string _shellExtraString;
        public ShellExtra(string shellExtraString)
        {
            this._shellExtraString = shellExtraString;
        }

        public string[] SqlConnection
        {
			get { return ShellExtraHandle.GetSqlConnection(_shellExtraString); }
        }

        public WebHeaderCollection HttpHeader
        {
            get
            {
                var header = new WebHeaderCollection();
                foreach (var i in ShellExtraHandle.GetHttpHeaderList(_shellExtraString))
                {
                    header.Add(i.Key, i.Value);
                }
                return header;
            }
        }
    }
}