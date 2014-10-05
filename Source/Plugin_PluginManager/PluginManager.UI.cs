using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_PluginManager
{
	partial class PluginManager
	{
		void Init()
		{
			//page1
			var page1 = new DynamicLayout();
			_gridViewInstalled = new GridView();
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new CheckBoxCell("Checked"),
				HeaderText = "Checked",
				Editable = true
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Name"), 
				HeaderText = "Name",
				AutoSize = false,
				Width = 150
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Author"), 
				HeaderText = "Author",
				AutoSize = false,
				Width = 100
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("InstalledVersion"),
				HeaderText = "InstalledVersion",
				AutoSize = false,
				Width = 150
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("FileName"), 
				HeaderText = "FileName",
				AutoSize = false,
				Width = 150
			});
			_gridViewInstalled.SelectionChanged += _gridViewInstalled_SelectionChanged;
			_textAreatInstalledDes = new TextArea();
			_buttonRemove = new Button {Text = "Remove"};
			_buttonRemove.Click += _buttonRemove_Click;
			page1.Add(_gridViewInstalled, true, true);
			page1.Add(_textAreatInstalledDes, true, false);
			page1.AddSeparateRow(null, _buttonRemove);

			//page2
			var page2 = new DynamicLayout();
			_gridViewAvailable = new GridView();
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new CheckBoxCell("Checked"),
				HeaderText = "Checked",
				Editable = true
			});
			_gridViewAvailable.Columns.Add(new GridColumn() 
			{
				DataCell = new TextBoxCell("Name"), 
				HeaderText = "Name",
				AutoSize = false,
				Width = 150
			});
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Author"), 
				HeaderText = "Author",
				AutoSize = false,
				Width = 100
			});
			_gridViewAvailable.Columns.Add(new GridColumn() 
			{
				DataCell = new TextBoxCell("AvailableVersion"), 
				HeaderText = "AvailableVersion",
				AutoSize = false,
				Width = 150
			});
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("RequiredVersion"), 
				HeaderText = "RequiredVersion",
				AutoSize = false,
				Width = 150
			});
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("CanInstall"), 
				HeaderText = "CanInstall",
				AutoSize = false,
				Width = 100
			});
			_gridViewAvailable.SelectionChanged += _gridViewAvailable_SelectionChanged;
			_textAreatAvailableDes = new TextArea();
			_buttonRefresh = new Button {Text = "Refresh"};
			_buttonRefresh.Click += _buttonRefresh_Click;
			_buttonInstall = new Button {Text = "Install"};
			_buttonInstall.Click += _buttonInstall_Click;
			page2.Add(_gridViewAvailable, true, true);
			page2.Add(_textAreatAvailableDes, true, false);
			page2.AddSeparateRow(null, _buttonRefresh, _buttonInstall);

			//page3
			var page3 = new DynamicLayout();
			_gridViewUpdatable = new GridView();
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new CheckBoxCell("Checked"),
				HeaderText = "Checked",
				Editable = true
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Name"), 
				HeaderText = "Name",
				AutoSize = false,
				Width = 150
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Author"), 
				HeaderText = "Author",
				AutoSize = false,
				Width = 100
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("InstalledVersion"), 
				HeaderText = "InstalledVersion",
				AutoSize = false,
				Width = 150
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("AvailableVersion"), 
				HeaderText = "AvailableVersion",
				AutoSize = false,
				Width = 150
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("CanUpdate"), 
				HeaderText = "CanUpdate",
				AutoSize = false,
				Width = 100
			});
			_gridViewUpdatable.SelectionChanged += _gridViewUpdatable_SelectionChanged;
			_textAreatUpdatesDes = new TextArea();
			_buttonUpdate = new Button {Text = "Update"};
			_buttonUpdate.Click += _buttonUpdate_Click;
			page3.Add(_gridViewUpdatable, true, true);
			page3.Add(_textAreatUpdatesDes, true, false);
			page3.AddSeparateRow(null, _buttonUpdate);

			//_tabPageInstalled
			_tabPageInstalled = new TabPage {Text = "Installed"};
			_tabPageInstalled.Content = page1;

			//_tabPageAvailable
			_tabPageAvailable = new TabPage {Text = "Available"};
			_tabPageAvailable.Content = page2;

			//_tabPageUpdatable
			_tabPageUpdatable = new TabPage {Text = "Updatable"};
			_tabPageUpdatable.Content = page3;

			//_tabControl
			_tabControl = new TabControl();
			_tabControl.Pages.Add(_tabPageInstalled);
			_tabControl.Pages.Add(_tabPageAvailable);
			_tabControl.Pages.Add(_tabPageUpdatable);
			_tabControl.SelectedIndexChanged += _tabControl_SelectedIndexChanged;

			//_buttonSetting
			_buttonSetting = new Button {Text = "Setting"};
			_buttonSetting.Click += _buttonSetting_Click;

			//_buttonClose
			_buttonClose = new Button {Text = "Close"};
			_buttonClose.Click += _buttonClose_Click;

			//_labelMsg
			_labelMsg = new Label
			{
				TextColor = Colors.Red,
				Text = "Your operation will take effect when the program start next time."
			};
			_labelMsg.Visible = false;

			var layout = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			layout.Add(_tabControl, true, true);
			layout.AddSeparateRow(_buttonSetting, _labelMsg, null, _buttonClose);

			Content = layout;
			Size = new Size(610, 430);
			Title = "PluginManager";
			Icon = Application.Instance.MainForm.Icon;
		}
		
		private TabControl _tabControl;
		private TabPage _tabPageInstalled;
		private TabPage _tabPageAvailable;
		private TabPage _tabPageUpdatable;

		private GridView _gridViewInstalled;
		private TextArea _textAreatInstalledDes;
		private Button _buttonRemove;

		private GridView _gridViewAvailable;
		private TextArea _textAreatAvailableDes;
		private Button _buttonRefresh;
		private Button _buttonInstall;

		private GridView _gridViewUpdatable;
		private TextArea _textAreatUpdatesDes;
		private Button _buttonUpdate;

		private Button _buttonSetting;
		private Button _buttonClose;
		private Label _labelMsg;
	}
}
