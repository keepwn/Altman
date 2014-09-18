using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Altman.Desktop.CustomControls;
using Altman.Desktop.Service;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Altman.Desktop.Forms
{
	public class FormMain : FormPlus
	{
		private PluginsImport _pluginsImport;
		private DirectoryCatalog _directoryCatalog;
		private CompositionContainer _container;

		private IHost _host;


		private ButtonMenuItem _pluginsMenuItem;
		private TabControl _tabControl;
		private Label _showMsgLabel;
		public FormMain()
		{
			//InitializeComponent();
			//CheckForIllegalCrossThreadCalls = false;
			this.ID = "FormMain";
			this.Title = "Altman";
			this.Style = "main";
			this.Icon = Icons.AltmanIcon;
			this.ClientSize = new Size(800, 500);

			this.Menu = GenerateMenuBar();
			//this.ToolBar = GenerateToolBar();
			this.Content = GenerateContent();

			//目录初始化
			if (!Directory.Exists(AppEnvironment.AppBinPath))
				Directory.CreateDirectory(AppEnvironment.AppBinPath);
			if (!Directory.Exists(AppEnvironment.AppPluginPath))
				Directory.CreateDirectory(AppEnvironment.AppPluginPath);
			if (!Directory.Exists(AppEnvironment.AppPluginConfigPath))
				Directory.CreateDirectory(AppEnvironment.AppPluginConfigPath);
			if (!Directory.Exists(AppEnvironment.AppLanguagePath))
				Directory.CreateDirectory(AppEnvironment.AppLanguagePath);


			//----导入插件----
			_pluginsImport = new PluginsImport();
			_host = new Host(this);
			Compose();
			//----导入插件结束----


			//----数据初始化----
			InitUi.InitCustomShellType(AppEnvironment.AppCustomShellTypePath);
			InitUi.InitGlobalSetting(AppEnvironment.AppPath);
			//----数据初始化结束----


			//----UI处理----       

			//treenode
			TreeView treeViewFunc = new TreeView();
			TreeItem treeItem = InitUi.GetCustomShellTypeTree();
			treeItem.Text = "ShellType";
			treeViewFunc.DataStore = treeItem;

			//plugins
			LoadPluginsInUi(_pluginsImport.Plugins.OrderBy(p => p.PluginSetting.IndexInList).ThenBy(p => p.PluginInfo.Name));
			//----UI处理结束----


			//显示免责声明
			InitUi.InitWelcome();

			//auto load plugins
			AutoLoadPlugins(_pluginsImport.Plugins);
		}


		private void pluginAbout_Click(object sender, EventArgs e)
		{
			IPlugin plugin = (IPlugin)(sender as Command).Tag;
			if (plugin == null)
				return;
			string msg = string.Format("Author:{0}\nVersion:{1}\nDescription:{2}\n",
				plugin.PluginInfo.Author,
				plugin.PluginInfo.Version,
				plugin.PluginInfo.Description);
			MessageBox.Show(msg, "About " + plugin.PluginInfo.Name, MessageBoxButtons.OK);
		}

		#region MEF处理
		/// <summary>
		/// 组合部件
		/// </summary>
		private void Compose()
		{
			var catalog = new AggregateCatalog();
			_directoryCatalog = new DirectoryCatalog("Plugins");
			catalog.Catalogs.Add(_directoryCatalog);
			_container = new CompositionContainer(catalog);
			try
			{
				_container.ComposeExportedValue("IHost", _host);
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



		public string MsgInStatusBar
		{
			get { return _showMsgLabel.Text; }
			set { _showMsgLabel.Text = value; }
		}

		public ContextMenu RightMenu
		{
			get { return this.ContextMenu; }
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
			//clear PluginsMenuItem
			//_pluginsMenuItem.Items.Clear();

			foreach (var plugin in plugins)
			{
				string title = plugin.PluginInfo.Name;

				//plugins in PluginsMenuItem
				var item = _pluginsMenuItem.Items.GetSubmenu(title);
				if (!plugin.PluginSetting.IsNeedShellData)
				{
					var pluginRun = new Command()
					{
						ID = "run",
						MenuText = "run",
						Tag = plugin,
					};
					pluginRun.Executed += pluginRun_Click;
					item.Items.Add(pluginRun);
				}
				var pluginAbout = new Command()
				{
					ID = "about",
					MenuText = "about",
					Tag = plugin,
				};
				pluginAbout.Executed += pluginAbout_Click;
				pluginAbout.Tag = plugin;
				item.Items.Add(pluginAbout);
				//_pluginsMenuItem.Items.Add(item);
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
					string title = plugin.PluginInfo.Name;
					if (plugin is IControlPlugin)
					{
						Panel view = null;
						var p = (plugin as IControlPlugin);
						p.PluginLoad += (sender, e) => { Language.UpdateLanguage(view); };
						view = p.GetUi(new Shell()) as Panel;
						//创建新的tab标签
						CreateNewTabPage(title, view);
					}
					else if (plugin is IFormPlugin)
					{
						object form = (plugin as IFormPlugin).GetUi(new Shell());
						var form1 = form as Form;
						if (form1 != null) form1.Show();
					}
				}
			}
		}

		private void pluginRun_Click(object sender, EventArgs e)
		{
			IPlugin plugin = (IPlugin)(sender as Command).Tag;
			if (plugin == null)
				return;
			if (plugin is IControlPlugin)
			{
				object view = (plugin as IControlPlugin).GetUi(new Shell());
				//创建新的tab标签
				//设置标题为FileManager|TargetId
				string title = plugin.PluginInfo.Name;
				CreateNewTabPage(title, view);
			}
			else if (plugin is IFormPlugin)
			{
				object form = (plugin as IFormPlugin).GetUi(new Shell());
				var form1 = form as Form;
				if (form1 != null) form1.Show();
			}
		}
		MenuBar GenerateMenuBar()
		{
			//创建标准系统菜单
			var menu = MenuBar.CreateStandardMenu();
			menu.Trim = false;

			//添加菜单内容
			var file = menu.Items.GetSubmenu("Menu", 100);
			file.ID = "menuitemMenu";
			_pluginsMenuItem = menu.Items.GetSubmenu("Plugins", 300);
			_pluginsMenuItem.ID = "menuitemPlugins";

			var help = menu.Items.GetSubmenu("Help", 1000);
			help.ID = "menuitemHelp";

			var about = new Actions.About();
			var quit = new Actions.Quit();
			var setting = new Actions.Setting();

			if (Platform.IsMac)
			{
				//适合 OS X style
				var main = menu.Items.GetSubmenu(Application.Instance.Name, 0);
				main.Items.Add(about, 0);
				main.Items.AddSeparator();
				main.Items.Add(setting, 100);
				main.Items.AddSeparator();
				main.Items.Add(quit, 1000);
			}

			//适合 windows/gtk style
			file.Items.Add(setting);
			file.Items.Add(quit);
			help.Items.Add(about);

			//menu.Items.Trim();
			return menu;
		}

		ToolBar GenerateToolBar()
		{
			var toolBar = new ToolBar();
			toolBar.Dock = ToolBarDock.Top;
			//toolBar.Items.Add(quit);
			//toolBar.Items.Add(new ButtonToolItem(about));

			return toolBar;
		}

		Control GenerateContent()
		{
			var layout = new DynamicLayout();
			layout.Add(_tabControl = DefaultTabs(), yscale: true);
			layout.Add(StatusBar());

			return layout;
		}

		Control StatusBar()
		{
			var font = new Font(SystemFont.StatusBar);
			var layout = new TableLayout(3, 1) { Size = new Size(Size.Width, 18), Spacing = new Size(5, 5), Padding = new Padding(5, 0) };
			layout.Add(_showMsgLabel = new Label { Text = "Hello World", Font = font }, 0, 0);
			layout.Add(null, 1, 0);

			var version = string.Format("Version:{0}@KeePwn", Assembly.GetExecutingAssembly().GetName().Version);
			layout.Add(new Label { Text = version, Font = font, HorizontalAlign = HorizontalAlign.Right }, 2, 0);
			return layout;
		}

		TabControl DefaultTabs()
		{
			var control = new TabControl();
			control.Pages.Add(new TabPage { Text = "Index" });
			control.MouseDoubleClick += control_MouseDoubleClick;
			return control;
		}
		public void CreateNewTabPage(string name, object userControl)
		{
			//create new tabpage
			var newTabpage = new TabPage
			{
				ID = name,
				Text = name,
				Content = userControl as Panel
			};
			//newTabpage.MouseDoubleClick += newTabpage_MouseDoubleClick;
			_tabControl.Pages.Add(newTabpage);
			_tabControl.SelectedPage = newTabpage;
		}



		#region Event
		void newTabpage_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (_tabControl.SelectedPage.Text != "Index" && _tabControl.SelectedPage.Text != "shellManager")
			{
				int index = _tabControl.SelectedIndex;
				_tabControl.Pages.Remove(_tabControl.SelectedPage);
				if (index - 1 >= 0)
					_tabControl.SelectedIndex = index - 1;
			}
		}

		void control_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			/*
			var tabControl = sender as TabControl;
			PointF pt = e.Location;

			for (int i = 0; i < tabControl.Pages.Count; i++)
			{
				Rectangle recTab = tabControl.Pages[0].Bounds;
				if (recTab.Contains())
				{
					tabcontrol.tabpage.remove(rectab);
					return;
				}
			}
			 */
		}

		private void Tsmi_Setting_Click(object sender, EventArgs e)
		{
			//FormGlobalSetting setting = new FormGlobalSetting();
			//setting.ShowDialog();
		}

		private void Tsmi_Wizard_Click(object sender, EventArgs e)
		{
			//启动自定义shelltype
			//FormCustomShellTypeWizard wiz = new FormCustomShellTypeWizard();
			//wiz.Show();
		}

		private void Tsmi_Listening_Click(object sender, EventArgs e)
		{
			//FormListening lisenting = new FormListening();
			//lisenting.Show();
		}

		private void Tsmi_developerMode_Click(object sender, EventArgs e)
		{
			//splitContainer1.Panel1Collapsed = !Tsmi_developerMode.Checked;
		}

		private void Tsmi_ReloadShellType_Click(object sender, EventArgs e)
		{
			InitUi.InitCustomShellType(AppEnvironment.AppPath);
		}

		private void Tsmi_ReloadSetting_Click(object sender, EventArgs e)
		{
			InitUi.InitGlobalSetting(AppEnvironment.AppPath);
		}

		private void Tsmi_docs_Click(object sender, EventArgs e)
		{
			/*
			string chm = Application.StartupPath + "//Docs//HELP.chm";
			if (File.Exists(chm))
			{
				System.Diagnostics.Process.Start(chm);
			}
			else
			{
				MessageBox.Show("Not find /Docs/help.chm");
			}
			 * */
		}
		#endregion
	}
}
