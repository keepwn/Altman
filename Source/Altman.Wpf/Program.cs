using System;
using Eto;

namespace Altman.Wpf
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var generator = Platform.Get(Platforms.Wpf);
            var app = new AltmanApplication(generator);
            app.Run();
        }
    }
}
