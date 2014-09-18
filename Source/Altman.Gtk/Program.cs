using System;
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
            var generator = Platform.Get(Platforms.Gtk2);
            var app = new AltmanApplication(generator);
            app.Run();
        }
    }
}
