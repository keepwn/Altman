using System;
using System.Collections.Generic;
using Altman.Util.Share;

namespace Altman.Webshell.Core
{
	internal class ShellExtraHandle
    {
		static ShellExtraHandle()
		{
			IniSettings.CommentChars = new[] {"#"};
			IniSettings.DefaultValueFormatting = "? = $		;";
			IniSettings.SeparateHeader = false;
			IniSettings.AllowInlineComments = false;
		}

        private static Ini GetShellExtraIni(string shellExtraString)
        {
            if (string.IsNullOrEmpty(shellExtraString)) return null;
	        Ini ini = null;
            try
            {
	            ini = Ini.FromString(shellExtraString);
            }
            catch
            {
				throw new Exception("Sorry,the extra string is not legal ini. Please edit it firstly.");
            }
            return ini;
        }

        public static string[] GetSqlConnection(string shellExtraString)
        {
	        var sqlconn = new string[2];

			var ini = GetShellExtraIni(shellExtraString);
			if (ini == null) return sqlconn;
			if (ini["SqlConnection"] == null) return sqlconn;

	        sqlconn[0] = ini["SqlConnection"]["type"];//type
			sqlconn[1] = ini["SqlConnection"]["conn"];//conn
	        return sqlconn;
        }

        public static Dictionary<string, string> GetHttpHeaderList(string shellExtraString)
        {
			var headers = new Dictionary<string, string>();

			var ini = GetShellExtraIni(shellExtraString);
            if (ini == null) return headers;
			if (ini["HttpHeader"] == null) return headers;

	        var items = ini["HttpHeader"].GetKeysAndValues();
			foreach (var item in items)
			{
				var name = item.Key;
	            var value = item.Value;
                if (!string.IsNullOrWhiteSpace(name) && !headers.ContainsKey(name))
                {
                    headers.Add(name, value);
                }             
            }
            return headers;
        }
    }
}
