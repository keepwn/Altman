using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml;
using Altman.Forms;

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
            setup.PrivateBinPath = "Bin;Languages";
            setup.ShadowCopyFiles = "true";
            setup.CachePath = Path.Combine(Application.StartupPath, "__cache__");
            setup.ShadowCopyDirectories = Path.Combine(Application.StartupPath, "Plugins") 
                                            + ";" + Path.Combine(Application.StartupPath, "Bin") 
                                            + ";" + Path.Combine(Application.StartupPath, "Languages");
            setup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //从Web下载时，程序会设置为远程程序集。须手动设置loadFromRemoteSources属性为真
            var trustedLoadFromRemoteSource = new PermissionSet(PermissionState.Unrestricted);
            var appDomain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup, trustedLoadFromRemoteSource);

            appDomain.DoCallBack(new CrossAppDomainDelegate(DoInShadowCopyFilesDomain));
        }
        private static void DoInShadowCopyFilesDomain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN"); 

            Application.Run(new FormMain());

            //启动自定义shelltype
            //Application.Run(new FormCustomShellTypeWizard());

            //test
            //XmlDocument xmlDoc = XmlHelper.ReadXml("data.xml", false,"body");
            //XmlHelper.SaveXml("data1.xml", xmlDoc,true, "body");
        }
    }
}
