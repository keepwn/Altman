using System.Collections.Generic;
using System.Windows.Forms;
using Altman.ModelCore;
using Altman.UICore.Control_ShellManager;
using PluginFramework;

namespace Altman
{
    public class TabCore
    {
        private static TabControl _tabControl = null;
        private static FormMain _formMain = null;
        /// <summary>
        /// 存放name，tabpage词典
        /// </summary>
        private static Dictionary<string, TabPage> _dicTagPage = null;
        private static Dictionary<string, UserControl> _dicTagPageControl = null;
        /// <summary>
        /// 存放tab标签名集合
        /// </summary>
        public struct Names
        {
            public static string shellManager = "shellManager";
            public static string shellCmder = "shellCmder";
            public static string fileManager = "fileManager";
        }

        public static void Init(FormMain formMain, TabControl tabControl)
        {
            _formMain = formMain;
            _tabControl = tabControl;
            InitDicTagPage();
        }

        /// <summary>
        /// 初始化DicTagPage
        /// </summary>
        private static void InitDicTagPage()
        {
            _dicTagPage = new Dictionary<string, TabPage>();
            _dicTagPageControl = new Dictionary<string, UserControl>();
        }

        /// <summary>
        /// 选择标签页（若标签页不存在则新建，存在则选择已经打开的标签页）
        /// </summary>
        /// <param name="name"></param>
        /// <returns>返回tabpages中的userControl</returns>
        public static UserControl SelectTabPage(string name)
        {
            //判断tabPage集合中是否含有该关键字的tabPage
            if (!_tabControl.TabPages.ContainsKey(name))
            {
                if (_dicTagPage.ContainsKey(name))
                {
                    _tabControl.TabPages.Add(_dicTagPage[name]);
                }
                else
                {
                    MessageBox.Show("不存在" + name + "标签");
                    return null;
                }
            }
            _tabControl.SelectTab(name);
            return _dicTagPageControl[name];
        }

        public static void CreateNewTabPage(string name, UserControl userControl)
        {
            //create new tabpage
            TabPage newTabpage = new TabPage(name);
            newTabpage.Name = name;
            newTabpage.Controls.Add(userControl);
            _tabControl.TabPages.Add(newTabpage);
            _tabControl.SelectTab(newTabpage);
        }


        /**
         * 这里是生成tabpage的方法
        */
        public static void CreateTabPage_ShellManager(IEnumerable<IPlugin> plugins)
        {
            //control_shellmanager
            ControlShellManager controlShellmanager = new ControlShellManager(plugins);
            TabPage tabpageShellmanager = new TabPage(Names.shellManager);
            tabpageShellmanager.Name = Names.shellManager;
            tabpageShellmanager.Controls.Add(controlShellmanager);

            _dicTagPageControl.Add(Names.shellManager, controlShellmanager);
            _dicTagPage.Add(Names.shellManager, tabpageShellmanager);

            _tabControl.TabPages.Add(tabpageShellmanager);
            _tabControl.SelectedTab = tabpageShellmanager;
        }
    }
}
