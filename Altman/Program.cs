using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Altman.LogicCore;

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
            var setup = new AppDomainSetup();
            setup.ShadowCopyFiles = "true";
            setup.CachePath = Path.Combine(Application.StartupPath, "__cache__");
            setup.ShadowCopyDirectories = Path.Combine(Application.StartupPath, "Plugins");
            var appDomain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup);

            appDomain.DoCallBack(new CrossAppDomainDelegate(DoInShadowCopyFilesDomain));
        }
        private static void DoInShadowCopyFilesDomain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            //启动自定义shelltype
            //Application.Run(new FormCustomShellTypeWizard());

            //test
            //XmlDocument xmlDoc = XmlHelper.ReadXml("data.xml", false,"body");
            //XmlHelper.SaveXml("data1.xml", xmlDoc,true, "body");
        }
    }
}
