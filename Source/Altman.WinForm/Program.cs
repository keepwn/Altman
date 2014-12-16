using System;
using System.IO;
using System.Reflection;
using Eto;
using Eto.Forms;
using Eto.WinForms.Forms;
using Eto.WinForms.Forms.Controls;
using Eto.WinForms.Forms.Menu;

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

		    Style.Add<Eto.WinForms.Forms.Controls.GridViewHandler>(null, h =>
		    {
				h.Control.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
				h.Control.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
				h.Control.BackgroundColor = System.Drawing.SystemColors.Window;
		    });
		    Style.Add<FormHandler>(null, h =>
		    {
				h.Control.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		    });
			Style.Add<MenuBarHandler>(null, h =>
			{
				h.Control.BackColor = System.Drawing.SystemColors.Control;
			});
			Style.Add<DropDownHandler>(null, h =>
			{
				h.Control.Font = new System.Drawing.Font(h.Control.Font.FontFamily, 10.5F, System.Drawing.FontStyle.Regular);
			});
			Style.Add<ComboBoxHandler>(null, h =>
			{
				h.Control.Font = new System.Drawing.Font(h.Control.Font.FontFamily, 10.5F, System.Drawing.FontStyle.Regular);
			});
			Style.Add<TextBoxHandler>(null, h =>
			{
				h.Control.Font = new System.Drawing.Font(h.Control.Font.FontFamily, 9.5F, System.Drawing.FontStyle.Regular);
				//h.Control.ImeMode = System.Windows.Forms.ImeMode.Disable;
			});
			//Style.Add<Eto.WinForms.ButtonHandler>(null, h =>
			//{
			//	h.Control.Font = new System.Drawing.Font(h.Control.Font.FontFamily, 9.5F, System.Drawing.FontStyle.Regular);
			//});
			generator.Add<TabControlPlus.IHandler>(() => new TabControlPlusHandler());

			var app = new AltmanApplication(generator);
			app.Run();
	    }
    }
}
