using System;
using System.Collections;

namespace Altman.Resources
{
	public static class AltStrRes
	{
		private static Hashtable _table;
		public static void SetTranslatedStrings(Hashtable table)
		{
			_table = table;
			if (table == null) throw new ArgumentNullException("table");

			_title = GetString(table, "StrTitle", _title);
			_aboutAltman = GetString(table, "StrAboutAltman", _aboutAltman);
			_docs = GetString(table, "StrDocs", _docs);
            _checkUpdate = GetString(table, "StrCheckUpdate", _checkUpdate);
			_disclaimer = GetString(table, "StrDisclaimer", _disclaimer);
			_dontDisplayAgain = GetString(table, "StrDontDisplayAgain", _dontDisplayAgain);
			_no = GetString(table, "StrNo", _no);
			_yes = GetString(table, "StrYes", _yes);
			_cancel = GetString(table, "StrCancel", _cancel);
			_welcome = GetString(table, "StrWelcome", _welcome);
			_quit = GetString(table, "StrQuit", _quit);
			_close = GetString(table, "StrClose", _close);
			_saveAndExit = GetString(table, "StrSaveAndExit", _saveAndExit);
			_file = GetString(table, "StrFile", _file);
			_options = GetString(table, "StrOptions", _options);
			_services = GetString(table, "StrServices", _services);
			_setting = GetString(table, "StrSetting", _setting);
			_plugins = GetString(table, "StrPlugins", _plugins);
			_help = GetString(table, "StrHelp", _help);

			// options
			_basic = GetString(table, "StrBasic", _basic);
			_userAgent = GetString(table, "StrUserAgent", _userAgent);
			_httpHeader = GetString(table, "StrHttpHeader", _httpHeader);
			_policy = GetString(table, "StrPolicy", _policy);
			_proxy = GetString(table, "StrProxy", _proxy);
			_language = GetString(table, "StrLanguage", _language);
			_isShowDisclaimer = GetString(table, "StrIsShowDisclaimer", _isShowDisclaimer);
			_isOpenIPythonSupport = GetString(table, "StrIsOpenIPythonSupport", _isOpenIPythonSupport);
			_isUserAgentRandom = GetString(table, "StrIsUserAgentRandom", _isUserAgentRandom);
			_userAgentList = GetString(table, "StrUserAgentList", _userAgentList);
			_isParamRandom = GetString(table, "StrIsParamRandom", _isParamRandom);
			_isShowDisclaimer = GetString(table, "StrIsShowDisclaimer", _isShowDisclaimer);
			_notUseProxy = GetString(table, "StrNotUseProxy", _notUseProxy);
			_useSystemProxySetting = GetString(table, "StrUseSystemProxySetting", _useSystemProxySetting);
			_useCustomProxySetting = GetString(table, "StrUseCustomProxySetting", _useCustomProxySetting);
		}

		public static string GetString(string strName, string strDefault)
		{
			if (_table != null)
			{
				return GetString(_table, strName, strDefault);
			}
			return strDefault;
		}

		public static string GetString(Hashtable table, string strName, string strDefault)
		{
			if (table.Contains(strName.ToLower()))
			{
				return (string)table[strName.ToLower()];
			}
			return strDefault;
		}

		public static string AboutAltman
		{
			get { return _aboutAltman; }
		}

		private static string _disclaimer = @"Welcome to use Altman (the website management tool)!
Please read the following statement carefully, when you used the Altman tool, indicating your acceptance of the following:
I
Altman tool is an experimental product. Software authors analysis, research and improvement of the existing management tools, has written this tool.
II
Altman tool support plug-in extensions, as a plug-in developers should strictly follow this statement as well as national laws and regulations, software author does not assume any legal liability.
III
This program (with plug-in) some functions may carry offensive, for security research and teaching purposes! Please users to comply with relevant state laws and regulations! 
Users of this program (included with plugin) for other purposes, by the users bear full legal and joint and several liability, software authors do not assume any legal liability.

This statement applies to local laws, users and software authors agreed to obey local laws. We reserve the right to change the above disclaimers and other terms of rights.";
		public static string Disclaimer
		{
			get { return _disclaimer; }
		}

		public static string DontDisplayAgain
		{
			get { return _dontDisplayAgain; }
		}

		public static string No
		{
			get { return _no; }
		}

		public static string Yes
		{
			get { return _yes; }
		}

		public static string Welcome
		{
			get { return _welcome; }
		}

		public static string Quit
		{
			get { return _quit; }
		}

		public static string Options
		{
			get { return _options; }
		}

		public static string Setting
		{
			get { return _setting; }
		}

		public static string Close
		{
			get { return _close; }
		}

		public static string Basic
		{
			get { return _basic; }
		}

		public static string UserAgent
		{
			get { return _userAgent; }
		}

		public static string HttpHeader
		{
			get { return _httpHeader; }
		}

		public static string Policy
		{
			get { return _policy; }
		}

		public static string Proxy
		{
			get { return _proxy; }
		}

		public static string SaveAndExit
		{
			get { return _saveAndExit; }
		}

		public static string Cancel
		{
			get { return _cancel; }
		}

		public static string Title
		{
			get { return _title; }
		}

		public static string File
		{
			get { return _file; }
		}

		public static string Services
		{
			get { return _services; }
		}

		public static string Plugins
		{
			get { return _plugins; }
		}

		public static string Help
		{
			get { return _help; }
		}

		public static string IsParamRandom
		{
			get { return _isParamRandom; }
		}

		public static string NotUseProxy
		{
			get { return _notUseProxy; }
		}

		public static string UseSystemProxySetting
		{
			get { return _useSystemProxySetting; }
		}

		public static string UseCustomProxySetting
		{
			get { return _useCustomProxySetting; }
		}

		public static string IsUserAgentRandom
		{
			get { return _isUserAgentRandom; }
		}

		public static string UserAgentList
		{
			get { return _userAgentList; }
		}

		public static string Docs
		{
			get { return _docs; }
		}

        public static string CheckUpdate
        {
            get { return _checkUpdate; }
        }

		public static string Language
		{
			get { return _language; }
		}

		public static string IsShowDisclaimer
		{
			get { return _isShowDisclaimer; }
		}

		public static string IsOpenIPythonSupport
		{
			get { return _isOpenIPythonSupport; }
		}

	    private static string _title = @"Altman";

		private static string _aboutAltman = @"About Altman";
		private static string _docs = @"Altman Docs";
        private static string _checkUpdate = @"Check Update";

		private static string _welcome = @"Welcome";
		private static string _dontDisplayAgain = @"don't display again";

		private static string _no = @"No";
		private static string _yes = @"Yes";
		private static string _cancel = @"Cancel";
		private static string _quit = @"Quit";
		private static string _close = @"Close";
		private static string _saveAndExit = @"SaveAndExit";

		private static string _file = @"File";
		private static string _options = @"Options";
		private static string _setting = @"Setting";
		private static string _services = @"Services";
		private static string _plugins = @"Plugins";
		private static string _help = @"Help";

		private static string _basic = @"Basic";
		private static string _userAgent = @"User-Agent";
		private static string _httpHeader = @"HttpHeader";
		private static string _policy = @"Policy";
		private static string _proxy = @"Proxy";
		private static string _language = @"Language";
		private static string _isOpenIPythonSupport = @"Open IronPython Support";
		private static string _isUserAgentRandom = @"UserAgent Random";
		private static string _userAgentList = @"UserAgent List";
		private static string _isParamRandom = @"Is Param Random";
		private static string _isShowDisclaimer = @"Show Disclaimer";
		private static string _notUseProxy = @"Not Use Proxy";
		private static string _useSystemProxySetting = @"Use System Proxy Setting";
		private static string _useCustomProxySetting = @"Use Custom Proxy Setting";
	}
}
