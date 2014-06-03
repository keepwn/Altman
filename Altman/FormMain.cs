using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Altman.LogicCore;
using Altman.ModelCore;
using Altman.WebCore;
using PluginFramework;

namespace Altman
{
    public partial class FormMain : Form
    {
        private PluginsImport _pluginsImport;
        private CompositionContainer _container;
        public FormMain()
        {
            InitializeComponent();
            //线程安全开关，关闭这个就能跨线程访问控件
            //CheckForIllegalCrossThreadCalls = false;

            _pluginsImport = new PluginsImport();
            //导入插件
            Compose();

            //tab事件绑定
            tabControl1.DoubleClick += tabControl1_DoubleClick;
            //设置版本号标签靠右
            toolStripStatusLabel_productVersion.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabel_productVersion.Text = string.Format("Version:{0}@KeePwn",Application.ProductVersion);

            TabCore.Init(this, this.tabControl1);
            //TabCore.Init(this, this.customTabControl1);
            TabCore.CreateTabPage_ShellManager(_pluginsImport.Plugins);

            InitTreeView();
            InitUI.InitCustomShellType();
            InitUI.InitGlobalSetting();

            TreeNode treeNodeRoot1 = new TreeNode("Plugins");
            this.treeView_func.Nodes.AddRange(new TreeNode[] { treeNodeRoot1 });

            foreach (var plugin in _pluginsImport.Plugins)
            {
                //添加tabpage到TabCore中
                string title = plugin.PluginAttribute.Title;
                //TabCore.CreateNewTabPage(name, ucplugin.Content);

                //添加到Plugins节点中
                TreeNode treeNode1 = new TreeNode(title);
                treeNodeRoot1.Nodes.Add(treeNode1);

                //添加到Tsmi_Plugins中
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = title;
                item.Text = title;
                item.Checked = true;                
                item.Tag = plugin;
                item.CheckOnClick = true;
                item.CheckedChanged += item_CheckedChanged;
                Tsmi_Plugins.DropDownItems.Add(item);

                //绑定插件的事件
                plugin.HostService.ShowMsgInStatusBar = Host_ShowMsgInStatusBar;
                plugin.HostService.ShowMsgInAppDialog = Host_ShowMsgInAppDialog;
                plugin.HostService.CreateNewTabPage = Host_CreateNewTabPage;
                plugin.HostService.SubmitCommand = SubmitCommand;
                //ucplugin.Func.OnSubmit += Func_OnSubmit;
            }


            //test
            TreeNode treeNodeRoot2;
            treeNodeRoot2 = InitUI.GetCustomShellTypeTree();
            treeNodeRoot2.Name = "ShellType";
            treeNodeRoot2.Text = "ShellType";
            this.treeView_func.Nodes.AddRange(new TreeNode[] { treeNodeRoot2 });
            
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

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("Plugins"));
            _container = new CompositionContainer(catalog);
            try
            {
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
                batch.RemovePart(part);
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


        #region 插件与宿主绑定的事件或方法
        /// <summary>
        /// 提交命令
        /// </summary>
        /// <param name="data">shellstruct数据</param>
        /// <param name="funcNameXpath">xpath表示的方法名（/cmder/readfile）</param>
        /// <param name="param"></param>
        /// <returns></returns>
        private byte[] SubmitCommand(ShellStruct data, string funcNameXpath, string[] param)
        {
            CustomShellType shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
            CustomCommandCode customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
            Dictionary<string, string> commandCode = customCommandCode.GetCode(funcNameXpath, param);
            HttpClient httpClient = new HttpClient();
            return httpClient.SubmitCommandByPost(data.ShellUrl, commandCode);
        }
        private byte[] SubmitCommand(ShellStruct data,
                                string funcNameXpath, string[] param,
                                bool isBindUploadProgressChangedEvent,
                                bool isBindDownloadProgressChangedEvent)
        {
            CustomShellType shellType = CustomShellTypeProvider.GetShellType(data.ShellType);
            CustomCommandCode customCommandCode = new CustomCommandCode(shellType, data.ShellPwd);
            Dictionary<string, string> commandCode = customCommandCode.GetCode(funcNameXpath, param);
            HttpClient httpClient = new HttpClient();
            //if (isBindUploadProgressChangedEvent)
            //    httpClient.UploadFileProgressChangedToDo += httpClient_UploadFileProgressChangedToDo;
            //if (isBindDownloadProgressChangedEvent)
            //    httpClient.DownloadFileProgressChangedToDo += httpClient_DownloadFileProgressChangedToDo;
            return httpClient.SubmitCommandByPost(data.ShellUrl, commandCode);
        }

        private void Host_ShowMsgInAppDialog(string msg)
        {
            MessageBox.Show(msg);
        }
        private void Host_ShowMsgInStatusBar(string msg)
        {
            toolStripStatusLabel_showMsg.Text = msg;
        }
        private void Host_CreateNewTabPage(string tabPageName, UserControl control)
        {
            TabCore.CreateNewTabPage(tabPageName, control);
        }
        private void item_CheckedChanged(object sender, EventArgs e)
        {
            //暂时屏蔽
            //ToolStripMenuItem item = sender as ToolStripMenuItem;
            //if (item != null && item.Checked == false)
            //{
            //    IPlugin plugin = item.Tag as IPlugin;
            //    if (UnLoadPlugin(plugin))
            //    {
            //        //MessageBox.Show("卸载成功");
            //        Tsmi_Plugins.DropDownItems.Remove(item);
            //    }
            //    else
            //    {
            //        MessageBox.Show("卸载失败");
            //        item.Checked = true;
            //    }
            //}
        }
        #endregion


        #region Init
        /// <summary>
        /// 初始化TreeView
        /// </summary>
        private void InitTreeView()
        {
            TreeNode treeNode1 = new TreeNode(TabCore.Names.shellManager);
            TreeNode treeNode2 = new TreeNode(TabCore.Names.shellCmder);
            TreeNode treeNode3 = new TreeNode(TabCore.Names.fileManager);

            TreeNode treeNodeRoot1 = new TreeNode("Webshell", new TreeNode[] { treeNode1, treeNode2, treeNode3 });

            this.treeView_func.Nodes.AddRange(new TreeNode[] {
                treeNodeRoot1}
                );
            this.treeView_func.Nodes[0].Expand();
        }

        #endregion

        #region Event
        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedTab.Name);
            if (tabControl1.SelectedTab.Text != "Index" && tabControl1.SelectedTab.Text != "shellManager")
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void treeView_func_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tn = treeView_func.SelectedNode;
            //判断是否是根节点
            if (tn.Parent != null)
            {
                string name = treeView_func.SelectedNode.Text;
                //判断tabPage集合中是否含有该关键字的tabPage
                TabCore.SelectTabPage(name);
            }
        }
        #endregion

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
            string msg = "本程序只用于测试，仅供安全学习、研究所用，\n" +
                         "请勿用于非法用途，否则造成的一切后果自负。\n" +
                         "                                              by keepwn";

            //MessageBox.Show(msg, "About Altman", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void Tsmi_developerMode_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !Tsmi_developerMode.Checked;
        }

        private void Tsmi_ReloadShellType_Click(object sender, EventArgs e)
        {
            InitUI.InitCustomShellType();
        }

        private void Tsmi_ReloadSetting_Click(object sender, EventArgs e)
        {
            InitUI.InitGlobalSetting();
        }

        private void Tsmi_docs_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "//help.chm");
        }

    }
}
