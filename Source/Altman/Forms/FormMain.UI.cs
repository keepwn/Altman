using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Altman.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class FormMain
	{
		public void Init()
		{
			this.Menu = GenerateMenuBar();
			//this.ToolBar = GenerateToolBar();
			this.Content = GenerateContent();

			this.Title = AltStrRes.Title + " [alpha 2014.11.7]";
			this.Style = "main";
			this.Icon = Icons.AltmanIcon;
			this.ClientSize = new Size(800, 500);
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

			if (Platform.IsWinForms || Platform.IsGtk)
			{
				var tabControl = new TabControlPlus();
				_tabControl = tabControl;
			}
			else
			{
				var tabControl = new TabControl();
				_tabControl = tabControl;
			}


			var layout = new DynamicLayout();
			layout.Add(_tabControl, yscale: true);
			layout.Add(StatusBar());

			return layout;
		}

		Control StatusBar()
		{
			var font = new Font(SystemFont.StatusBar);
			var layout = new TableLayout(3, 1) { Size = new Size(Size.Width, 18), Spacing = new Size(5, 5), Padding = new Padding(5, 0) };
			layout.Add(_showMsgLabel = new Label { Text = "Ready", Font = font }, 0, 0);
			layout.Add(null, 1, 0);

			var version = string.Format("Version: {0}@KeePwn", Assembly.GetExecutingAssembly().GetName().Version);
			layout.Add(new Label { Text = version, Font = font, HorizontalAlign = HorizontalAlign.Right }, 2, 0);
			return layout;
		}

		private ButtonMenuItem _pluginsMenuItem;
		private Control _tabControl;
		private Label _showMsgLabel;
	}
}