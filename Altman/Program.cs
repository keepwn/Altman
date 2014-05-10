using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using Altman.LogicCore;
using Altman.LogicCore.New;

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
