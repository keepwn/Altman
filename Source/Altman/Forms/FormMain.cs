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
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class FormMain : Form
	{
		private CompositionContainer _container;

		private IHost _host;

		public FormMain()
		{
			//InitializeComponent();
			//CheckForIllegalCrossThreadCalls = false;

			//目录初始化
			if (!Directory.Exists(AppEnvironment.AppBinPath))
				Directory.CreateDirectory(AppEnvironment.AppBinPath);
			if (!Directory.Exists(AppEnvironment.AppPluginPath))
				Directory.CreateDirectory(AppEnvironment.AppPluginPath);
			if (!Directory.Exists(AppEnvironment.AppPluginConfigPath))
				Directory.CreateDirectory(AppEnvironment.AppPluginConfigPath);
			if (!Directory.Exists(AppEnvironment.AppLanguagePath))
				Directory.CreateDirectory(AppEnvironment.AppLanguagePath);

			//----数据初始化----
			InitUi.InitCustomShellType(AppEnvironment.AppCustomShellTypePath);
			InitUi.InitGlobalSetting(AppEnvironment.AppPath);
			var basicSetting = (GlobalSetting.Setting as Setting).BasicSetting;
			//----数据初始化结束----

			//语言初始化
			var lang = basicSetting.Language;
			AltLangRes.ReadLanguageResource(lang);
			AltStrRes.SetTranslatedStrings(AltLangRes.Table);

			//----导入插件----
			//_pluginsImport = new PluginsImport();
			_host = new Host(this);
			PluginProvider.Host = _host;
			PluginProvider.Compose(AppEnvironment.AppPluginPath, AppEnvironment.AppServicePath, basicSetting.IsOpenIPythonSupport);

			//UI处理
			Init();
			LoadPluginsInUi();
			//----导入插件结束----


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

		public void CreateNewTabPage(string name, object userControl)
		{
			//create new tabpage
			var newTabpage = new TabPage
			{
				ID = name,
				Text = name,
				Content = userControl as Panel
			};

			if (Platform.IsWinForms || Platform.IsGtk)
			{
				var tab = _tabControl as TabControlPlus;
				if (!tab.Pages.Any())
				{
					tab.Pages.Add(new TabPage { Text = "Index", Content = new PanelIndex() });
				}
				tab.Pages.Add(newTabpage);
				tab.SelectedPage = newTabpage;
			}
			else
			{
				var tab = _tabControl as TabControl;
				if (!tab.Pages.Any())
				{
					tab.Pages.Add(new TabPage { Text = "Index", Content = new PanelIndex() });
				}
				tab.Pages.Add(newTabpage);
				tab.SelectedPage = newTabpage;
			}

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
