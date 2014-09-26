using System;
using System.IO;
using System.Reflection;
using Altman.Desktop;
using Eto;

namespace Altman.Gtk
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
			var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				var setup = new AppDomainSetup();
				setup.PrivateBinPath = "Bin";
				setup.ShadowCopyFiles = "true";
				setup.CachePath = Path.Combine(Path.GetTempPath(), "__cache__");
				setup.ShadowCopyDirectories = Path.Combine(path, "Plugins") + ";" + Path.Combine(path, "Bin");

				var appDomain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup);
				appDomain.ExecuteAssembly(Assembly.GetExecutingAssembly().CodeBase);
			}
			else
			{
				Start();
			}
        }

	    static void Start()
	    {
			var generator = Platform.Get(Platforms.Gtk2);
			var app = new AltmanApplication(generator);
			app.Run();
	    }
    }
}
