using System;
using System.Collections.Generic;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            
            //启动自定义shelltype
            //Application.Run(new FormCustomShellTypeWizard());

            //test
            //XmlDocument xmlDoc = XmlHelper.ReadXml("data.xml", false,"body");
            //XmlHelper.SaveXml("data1.xml", xmlDoc,true, "body");
          
            //test
            FuncTreeNode root = new FuncTreeNode("root");
            root.AddNode("A");
            FuncTreeNode d =root.AddNodes("/a/b/c/d");
            string dpath = d.FullPath;
            FuncTreeNode find_a = root.FindNode("a");
            FuncTreeNode find_c = root.FindNodes("a/b/c");
            FuncTreeNode find_d = root.FindNodes("a");
        }
    }
}
