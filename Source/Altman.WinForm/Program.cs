using System;
using System.IO;
using System.Reflection;
using Eto;

namespace Altman
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
			var generator = Platform.Get(Platforms.WinForms);
			Style.Add<Eto.WinForms.Forms.Controls.GridViewHandler>(null,
				h =>
				{
					h.Control.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
					h.Control.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
					h.Control.BackgroundColor = System.Drawing.SystemColors.Window;
				});

			Style.Add<Eto.WinForms.MenuBarHandler>(null, h =>
			{
				h.Control.BackColor = System.Drawing.SystemColors.Control;
			});
			generator.Add<Eto.Forms.TabControlPlus.IHandler>(() => new TabControlPlusHandler());

			var app = new AltmanApplication(generator);
			app.Run();
	    }
    }
}
