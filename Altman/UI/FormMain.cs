using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Altman.DbCore;
using Altman.LogicCore;
using Altman.ModelCore;
using Altman.WebCore;
using PluginFramework;

namespace Altman
{
    public partial class FormMain : Form
    {
        private PluginsImport _pluginsImport;
        private DirectoryCatalog _directoryCatalog;
        private CompositionContainer _container;

        private IHostService _hostService;
        public FormMain()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;

            //----导入插件----
            _pluginsImport = new PluginsImport();
            _hostService = new HostService(this);
            Compose();
            //----导入插件结束----


            //----数据初始化----
            InitUi.InitCustomShellType();
            InitUi.InitGlobalSetting();
            //----数据初始化结束----


            //----UI处理----
            //tabControl事件绑定
            tabControl1.DoubleClick += tabControl1_DoubleClick;
            //设置版本号
            toolStripStatusLabel_productVersion.Text = string.Format("Version:{0}@KeePwn",Application.ProductVersion);
            //tabControl初始化
            TabCore.Init(this, this.tabControl1);
           

            //treenode
            TreeNode treeNodeRoot1;
            treeNodeRoot1 = InitUi.GetCustomShellTypeTree();
            treeNodeRoot1.Name = "ShellType";
            treeNodeRoot1.Text = "ShellType";
            this.treeView_func.Nodes.AddRange(new TreeNode[] { treeNodeRoot1 });           

            //plugins
            LoadPluginsInUi(_pluginsImport.Plugins);
            //----UI处理结束----

            
            //显示免责声明
            InitUi.InitWelcome();

            //auto load plugins
            AutoLoadPlugins(_pluginsImport.Plugins);
        }

        private void pluginRun_Click(object sender, EventArgs e)
        {
            IPlugin plugin = (IPlugin)(sender as ToolStripMenuItem).Tag;
            if (plugin == null)
                return;
            if (plugin is IControlPlugin)
            {
                UserControl view = (plugin as IControlPlugin).GetUi(new ShellStruct());
                //创建新的tab标签
                //设置标题为FileManager|TargetId
                string title = plugin.PluginAttribute.Name;
                TabCore.CreateNewTabPage(title, view);
            }
            else if (plugin is IFormPlugin)
            {
                Form form = (plugin as IFormPlugin).GetUi(new ShellStruct());
                form.Show();
            }
        }
        private void pluginAbout_Click(object sender, EventArgs e)
        {
            IPlugin plugin = (IPlugin)(sender as ToolStripMenuItem).Tag;
            if (plugin == null)
                return;
            string msg = string.Format("Author:{0}\nVersion:{1}\nDescription:{2}\n",
                plugin.PluginAttribute.Author,
                plugin.PluginAttribute.Version,
                plugin.PluginAttribute.Description);
            MessageBox.Show(msg, "About "+plugin.PluginAttribute.Name, MessageBoxButtons.OK);
        }

        #region MEF处理
        /// <summary>
        /// 组合部件
        /// </summary>
        private void Compose()
        {
            //判断Plugins文件夹是否存在
            if (!Directory.Exists("Plugins"))
            {
                Directory.CreateDirectory("Plugins");
            }

            var catalog = new AggregateCatalog();
			_directoryCatalog = new DirectoryCatalog("Plugins");
			catalog.Catalogs.Add(_directoryCatalog);
            _container = new CompositionContainer(catalog);
            try
            {
                _container.ComposeExportedValue("IHostService", _hostService);
                _container.ComposeParts(_pluginsImport);
            }
            catch (CompositionException compositionException)
            {
                MessageBox.Show(compositionException.ToString());
                _container.Dispose();
            }
        }
        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        private bool UnLoadPlugin(IPlugin plugin)
        {
            bool isSuccess = false;
            try
            {
                var batch = new CompositionBatch();
                var part = AttributedModelServices.CreatePart(plugin);
                //var part = batch.AddExportedValue<IPlugin>(plugin);
                //var part2 = _container.GetExportedValues<IPlugin>().First();
                //Lazy<IPlugin> part3 = _container.GetExport<IPlugin>();
                //IPlugin tmp = part3.Value;
                //batch.RemovePart(part);
                batch.AddPart(part);
                _container.Compose(batch);
                //_container.ReleaseExport(part3);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        #endregion

        #region Event
        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text != "Index" && tabControl1.SelectedTab.Text != "shellManager")
            {
                int index = tabControl1.SelectedIndex;
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                if (index - 1 >= 0)
                    tabControl1.SelectedIndex = index - 1;
            }
        }

        private void treeView_func_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //
        }

                private void Tsmi_Setting_Click(object sender, EventArgs e)
        {
            FormGlobalSetting setting = new FormGlobalSetting();
            setting.ShowDialog();
        }

        private void Tsmi_Wizard_Click(object sender, EventArgs e)
        {
            //启动自定义shelltype
            FormCustomShellTypeWizard wiz = new FormCustomShellTypeWizard();
            wiz.Show();
        }

        private void Tsmi_Listening_Click(object sender, EventArgs e)
        {
            FormListening lisenting = new FormListening();
            lisenting.Show();
        }

        private void Tsmi_about_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void Tsmi_developerMode_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !Tsmi_developerMode.Checked;
        }

        private void Tsmi_ReloadShellType_Click(object sender, EventArgs e)
        {
            InitUi.InitCustomShellType();
        }

        private void Tsmi_ReloadSetting_Click(object sender, EventArgs e)
        {
            InitUi.InitGlobalSetting();
        }

        private void Tsmi_docs_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "//help.chm");
        }
        #endregion

        public string MsgInStatusBar 
        {
            get { return toolStripStatusLabel_showMsg.Text; }
            set { toolStripStatusLabel_showMsg.Text = value; }
        }

        public PluginsImport PluginsImport
        {
            get { return _pluginsImport; }
        }

        public void RefreshPlugins()
        {
            _directoryCatalog.Refresh();
            try
            {
                _container.ComposeParts(_pluginsImport);
            }
            catch (CompositionException compositionException)
            {
                MessageBox.Show(compositionException.ToString());
                _container.Dispose();
            }
            finally
            {
                LoadPluginsInUi(_pluginsImport.Plugins);
            }
        }

        private void LoadPluginsInUi(IEnumerable<IPlugin> plugins)
        {
            //clear Tsmi_Plugins
            Tsmi_Plugins.DropDownItems.Clear();

            foreach (var plugin in plugins)
            {
                string title = plugin.PluginAttribute.Name;

                //plugins in Tsmi_Plugins
                ToolStripMenuItem item = new ToolStripMenuItem(title);
                if (!plugin.PluginSetting.IsNeedShellData)
                {
                    ToolStripMenuItem pluginRun = new ToolStripMenuItem("run");
                    pluginRun.Click += pluginRun_Click;
                    pluginRun.Tag = plugin;
                    item.DropDownItems.Add(pluginRun);
                }
                ToolStripMenuItem pluginAbout = new ToolStripMenuItem("about");
                pluginAbout.Click += pluginAbout_Click;
                pluginAbout.Tag = plugin;
                item.DropDownItems.Add(pluginAbout);
                Tsmi_Plugins.DropDownItems.Add(item);
            }
        }

        private void AutoLoadPlugins(IEnumerable<IPlugin> plugins)
        {
            //IsAutoLoad
            foreach (var plugin in plugins)
            {
                //IsAutoLoad
                if (plugin.PluginSetting.IsAutoLoad)
                {
                    string title = plugin.PluginAttribute.Name;
                    if (plugin is IControlPlugin)
                    {
                        UserControl view = (plugin as IControlPlugin).GetUi(new ShellStruct());
                        //创建新的tab标签
                        TabCore.CreateNewTabPage(title, view);
                    }
                    else if (plugin is IFormPlugin)
                    {
                        Form form = (plugin as IFormPlugin).GetUi(new ShellStruct());
                        form.Show();
                    }
                }
            }
        }
    }
}
