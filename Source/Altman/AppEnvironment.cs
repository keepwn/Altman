using System;
using System.IO;
using System.Reflection;

namespace Altman
{
	public static class AppEnvironment
	{
		public static Version AppVersion
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version; }
		}

		public static string AppPath
		{
			//不推荐使用Environment.CurrentDirectory等其他方法，在跨平台时可能会有问题
			get
			{
				var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
				return path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? path : path + Path.DirectorySeparatorChar;			
			}
		}

		public static string AppPluginDir
		{
			get { return "Plugins"; }
		}
		public static string AppPluginPath
		{
			get { return  Path.Combine(AppPath, "Plugins"); }
		}

		public static string AppServiceDir
		{
			get { return "Services"; }
		}
		public static string AppServicePath
		{
			get { return Path.Combine(AppPath, "Services"); }
		}

		public static string AppPluginConfigDir
		{
			get { return "Config"; }
		}
		public static string AppPluginConfigPath
		{
			get { return Path.Combine(AppPath, AppPluginConfigDir, "Config"); }
		}

		public static string AppBinDir
		{
			get { return "Bin"; }
		}
		public static string AppBinPath
		{
			get { return Path.Combine(AppPath, "Bin"); }
		}

		public static string AppLanguageDir
		{
			get { return "Languages"; }
		}
		public static string AppLanguagePath
		{
			get { return Path.Combine(AppPath, "Languages"); }
		}

		public static string AppCustomShellTypeDir
		{
			get { return "CustomType"; }
		}
		public static string AppCustomShellTypePath
		{
			get { return Path.Combine(AppPath, "CustomType"); }
		}
	}
}
