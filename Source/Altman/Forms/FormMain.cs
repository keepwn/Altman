using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Altman.CustomControls;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Resources;
using Altman.Service;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public class FormMain : Form
	{
		//private PluginsImport _pluginsImport;
		private CompositionContainer _container;

		private IHost _host;

		private ButtonMenuItem _pluginsMenuItem;
		private TabControlPlus _tabControl;
		private Label _showMsgLabel;
		public FormMain()
		{
			//InitializeComponent();
			//CheckForIllegalCrossThreadCalls = false;
			this.Title = AltStrRes.Title;
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
			//_pluginsImport = new PluginsImport();
			_host = new Host(this);
			PluginProvider.Host = _host;
			PluginProvider.Compose(AppEnvironment.AppPluginPath, AppEnvironment.AppServicePath, true);

			//----导入插件结束----


			//----数据初始化----
			InitUi.InitCustomShellType(AppEnvironment.AppCustomShellTypePath);
			InitUi.InitGlobalSetting(AppEnvironment.AppPath);
			//----数据初始化结束----


			//----UI处理----       

			//treenode
			//TreeView treeViewFunc = new TreeView();
			//TreeItem treeItem = InitUi.GetCustomShellTypeTree();
			//treeItem.Text = "ShellType";
			//treeViewFunc.DataStore = treeItem;

			//plugins
			LoadPluginsInUi();
			//----UI处理结束----


			//显示免责声明
			InitUi.InitWelcome();

			//auto call services
			AutoLoadServices(PluginProvider.Services);
			//auto load plugins
			AutoLoadPlugins(PluginProvider.Plugins);
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

		public string MsgInStatusBar
		{
			get { return _showMsgLabel.Text; }
			set { _showMsgLabel.Text = value; }
		}

		public ContextMenu RightMenu
		{
			get { return this.ContextMenu; }
		}

		public IEnumerable<IPlugin> Plugins
		{
			get { return PluginProvider.Plugins; }
		}

		/*
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
		 */

		private void LoadPluginsInUi()
		{
			foreach (var plugin in PluginProvider.GetRootPlugins())
			{
				var item = LoadPluginsInUi(plugin, true);
				_pluginsMenuItem.Items.Add(item);
			}
		}

		private ButtonMenuItem LoadPluginsInUi(IPlugin plugin, bool isRoot)
		{
			var item = new ButtonMenuItem();

			var title = plugin.PluginInfo.Name;
			item.Text = title;
			item.Order = plugin.PluginSetting.IndexInList;

			// 如果是插件，添加Run按钮
			if (isRoot)
			{
				var pluginRun = new Command()
				{
					MenuText = "Run",
					Tag = plugin,
				};
				pluginRun.Executed += pluginRun_Click;
				item.Items.Add(pluginRun);
			}

			// 添加About按钮
			var pluginAbout = new Command()
			{
				MenuText = "About",
				Tag = plugin,
			};
			pluginAbout.Executed += pluginAbout_Click;
			pluginAbout.Tag = plugin;
			item.Items.Add(pluginAbout);

			// 添加子插件按钮
			var childs = PluginProvider.GetChildPlugins(plugin);
			if (childs.Any())
			{
				item.Items.AddSeparator();
				var pluginChild = new ButtonMenuItem()
				{
					Text = "Child Plugins",
				};
				foreach (var c in childs)
				{
					pluginChild.Items.Add(LoadPluginsInUi(c, false));
				}
				item.Items.Add(pluginChild);
			}

			var serviceNames = PluginServiceProvider.GetServiceNames(title).ToList();
			if (serviceNames.Any())
			{
				var pluginService = new ButtonMenuItem()
				{
					Text = "Services",
				};
				foreach (var name in serviceNames)
				{
					var serviceType = PluginServiceProvider.GetServiceTypeName(name);
					var text = string.IsNullOrEmpty(serviceType) ? name : "[Type: " + serviceType + " ] " + name;
					pluginService.Items.Add(new ButtonMenuItem {Text = text});
				}
				item.Items.Add(pluginService);
			}

			return item;
		}

		private void AutoLoadServices(IEnumerable<IService> services)
		{
			//IsAutoLoad
			foreach (var service in services)
			{
				// IsService, Auto Call Load()
				service.Load();
			}
		}

		private void AutoLoadPlugins(IEnumerable<IPlugin> plugins)
		{
			//IsAutoLoad
			foreach (var plugin in plugins)
			{
				// IsService, Auto Call Load()
				plugin.Load();
				//IsAutoLoad
				if (plugin.PluginSetting.IsAutoLoad)
				{
					string title = plugin.PluginInfo.Name;
					if (plugin is IControlPlugin)
					{
						Panel view = null;
						var p = (plugin as IControlPlugin);
						view = p.LoadGui(null) as Panel;
						//创建新的tab标签
						CreateNewTabPage(title, view);
					}
					else if (plugin is IFormPlugin)
					{
						object form = (plugin as IFormPlugin).LoadGui(null);
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
				object view = (plugin as IControlPlugin).LoadGui(null);
				//创建新的tab标签
				//设置标题为FileManager|TargetId
				string title = plugin.PluginInfo.Name;
				CreateNewTabPage(title, view);
			}
			else if (plugin is IFormPlugin)
			{
				object form = (plugin as IFormPlugin).LoadGui(null);
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
			var file = menu.Items.GetSubmenu(AltStrRes.Menu, 100);
			_pluginsMenuItem = menu.Items.GetSubmenu(AltStrRes.Plugin, 300);
			var help = menu.Items.GetSubmenu(AltStrRes.Help, 1000);

			var about = new Actions.About();
			var docs = new Actions.Docs();
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
			help.Items.Add(docs);

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
			layout.Add(DefaultTabs(), yscale: true);
			layout.Add(StatusBar());

			return layout;
		}

		Control StatusBar()
		{
			var font = new Font(SystemFont.StatusBar);
			var layout = new TableLayout(3, 1) { Size = new Size(Size.Width, 18), Spacing = new Size(5, 5), Padding = new Padding(5, 0) };
			layout.Add(_showMsgLabel = new Label { Text = "Ready", Font = font }, 0, 0);
			layout.Add(null, 1, 0);

			var version = string.Format("Version:{0}@KeePwn", Assembly.GetExecutingAssembly().GetName().Version);
			layout.Add(new Label { Text = version, Font = font, HorizontalAlign = HorizontalAlign.Right }, 2, 0);
			return layout;
		}

		Control DefaultTabs()
		{
			var control = _tabControl = new TabControlPlus();
			control.Pages.Add(new TabPage { Text = "Index" });
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
			_tabControl.Pages.Add(newTabpage);
			_tabControl.SelectedPage = newTabpage;
		}

		#region Event

		private void Tsmi_Setting_Click(object sender, EventArgs e)
		{
			//FormGlobalSetting setting = new FormGlobalSetting();
			//setting.ShowDialog();
		}

		private void Tsmi_developerMode_Click(object sender, EventArgs e)
		{
			//splitContainer1.Panel1Collapsed = !Tsmi_developerMode.Checked;
		}

		private void Tsmi_ReloadShellType_Click(object sender, EventArgs e)
		{
			//InitUi.InitCustomShellType(AppEnvironment.AppPath);
		}

		private void Tsmi_ReloadSetting_Click(object sender, EventArgs e)
		{
			InitUi.InitGlobalSetting(AppEnvironment.AppPath);
		}
		#endregion
	}
}
