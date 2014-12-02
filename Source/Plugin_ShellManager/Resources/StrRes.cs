using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_ShellManager.Resources
{
	public static class StrRes
	{
		public static string GetString(string strName, string strDefault)
		{
			if (ShellManager.Host != null)
			{
				return ShellManager.Host.Ui.GetTranslatedText(strName, strDefault);
			}
			return strDefault;
		}
	}
}
