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

			this.Title = AltStrRes.Title;
			this.Style = "main";
			this.Icon = Icons.AltmanIcon;
			this.ClientSize = new Size(800, 500);
		}
		MenuBar GenerateMenuBar()
		{
			var docs = new Actions.Docs();
            var checkUpdate = new Actions.CheckUpdate();
			var about = new Actions.About();
			var quit = new Actions.Quit();
			var setting = new Actions.Setting();

			//var file = new ButtonMenuItem {Text = AltStrRes.File, ID = "File"};
			var service = _servicesMenuItem = new ButtonMenuItem { Text = AltStrRes.Services, ID = "Services" };
			var plugin = _pluginsMenuItem = new ButtonMenuItem { Text = AltStrRes.Plugins, ID = "Plugins" };
			//var help = new ButtonMenuItem { Text = AltStrRes.Help, ID = "Help" };

			var menuBar = new MenuBar
			{
				Trim = false,
				Items =
				{
					service,
					plugin
				},
				ApplicationItems =
				{
					setting
				},
				HelpItems =
				{
					docs,
                    checkUpdate
				},
				QuitItem = quit,
				AboutItem = about,
				IncludeSystemItems = MenuBarSystemItems.None
			};
			var file = menuBar.ApplicationMenu;
			file.ID = "File";
			if (!Platform.IsMac)
			{
				file.Text = AltStrRes.File;
			}

			var help = menuBar.HelpMenu;
			help.ID = "Help";
			help.Text = AltStrRes.Help;

			return menuBar;
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
				tabControl.Pages.Add(new TabPage { Text = "Index", Content = new PanelIndex() });
				_tabControl = tabControl;
			}
			else
			{
				var tabControl = new TabControl();
				tabControl.Pages.Add(new TabPage { Text = "Index", Content = new PanelIndex() });
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
			var layout = new TableLayout(4, 1) { Size = new Size(Size.Width, 18), Spacing = new Size(5, 5), Padding = new Padding(5, 0) };
		    layout.Add(_lodingSpinner = new Spinner { Visible = false }, 0, 0);
            layout.Add(_showMsgLabel = new Label { Text = "Ready", Font = font }, 1, 0);
			layout.Add(null, 2, 0);

			var version = string.Format("Version: {0}@KeePwn", Assembly.GetExecutingAssembly().GetName().Version);
			layout.Add(new Label { Text = version, Font = font, HorizontalAlign = HorizontalAlign.Right }, 3, 0);
			return layout;
		}

		private ButtonMenuItem _pluginsMenuItem;
		private ButtonMenuItem _servicesMenuItem;
		private Control _tabControl;
		private Label _showMsgLabel;
	    private Spinner _lodingSpinner;
	}
}