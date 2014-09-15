using System;
using swf=System.Windows.Forms;
using sd=System.Drawing;
using Altman.Desktop;
using Eto;

namespace Altman.WinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var generator = Platform.Get(Platforms.WinForms);
			Style.Add<Eto.WinForms.Forms.Controls.GridViewHandler>(null,
				h =>
				{
					h.Control.ColumnHeadersBorderStyle = swf.DataGridViewHeaderBorderStyle.None;
					h.Control.BorderStyle = swf.BorderStyle.Fixed3D;
				});

	        Style.Add<Eto.WinForms.MenuBarHandler>(null, h =>
	        {
				h.Control.BackColor = sd.SystemColors.Control;
	        });
            var app = new AltmanApplication(generator);
            app.Run();
        }
    }
}
