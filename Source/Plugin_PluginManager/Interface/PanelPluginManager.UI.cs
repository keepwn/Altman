using Eto.Drawing;
using Eto.Forms;
using Plugin_PluginManager.Resources;

namespace Plugin_PluginManager.Interface
{
	partial class PanelPluginManager
	{
		void Init()
		{
			//page1
			var page1 = new DynamicLayout();
			_gridViewInstalled = new GridView();
			_gridViewInstalled.ColumnHeaderClick += _gridViewInstalled_ColumnHeaderClick;
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new CheckBoxCell("Checked"),
				HeaderText = StrRes.GetString("StrChecked","Checked"),
				Editable = true
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				ID = "Name",
				DataCell = new TextBoxCell("Name"),
				HeaderText = StrRes.GetString("StrName", "Name"),
				Sortable = true,
				AutoSize = false,
				Width = 150
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Author"), 
				HeaderText = StrRes.GetString("StrAuthor","Author"),
				AutoSize = false,
				Width = 100
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("InstalledVersion"),
				HeaderText = StrRes.GetString("StrInstalledVersion","InstalledVersion"),
				AutoSize = false,
				Width = 150
			});
			_gridViewInstalled.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("FileName"), 
				HeaderText = StrRes.GetString("StrFileName","FileName"),
				AutoSize = false,
				Width = 150
			});
			_gridViewInstalled.SelectionChanged += _gridViewInstalled_SelectionChanged;
			_textAreatInstalledDes = new TextArea();
			_buttonRemove = new Button {Text = StrRes.GetString("StrRemove","Remove")};
			_buttonRemove.Click += _buttonRemove_Click;
			page1.Add(_gridViewInstalled, true, true);
			page1.Add(_textAreatInstalledDes, true, false);
			page1.AddSeparateRow(null, _buttonRemove);

			//page2
			var page2 = new DynamicLayout();
			_gridViewAvailable = new GridView();
			_gridViewAvailable.ColumnHeaderClick += _gridViewAvailable_ColumnHeaderClick;
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new CheckBoxCell("Checked"),
				HeaderText = StrRes.GetString("StrChecked","Checked"),
				Editable = true
			});
			_gridViewAvailable.Columns.Add(new GridColumn() 
			{
				ID = "Name",
				DataCell = new TextBoxCell("Name"), 
				HeaderText = StrRes.GetString("StrName","Name"),
				Sortable = true,
				AutoSize = false,
				Width = 150
			});
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Author"), 
				HeaderText = StrRes.GetString("StrAuthor","Author"),
				AutoSize = false,
				Width = 100
			});
			_gridViewAvailable.Columns.Add(new GridColumn() 
			{
				DataCell = new TextBoxCell("AvailableVersion"), 
				HeaderText = StrRes.GetString("StrAvailableVersion","AvailableVersion"),
				AutoSize = false,
				Width = 150
			});
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("RequiredVersion"), 
				HeaderText = StrRes.GetString("StrRequiredVersion","RequiredVersion"),
				AutoSize = false,
				Width = 150
			});
			_gridViewAvailable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("CanInstall"), 
				HeaderText = StrRes.GetString("StrCanInstall","CanInstall"),
				AutoSize = false,
				Width = 100
			});
			_gridViewAvailable.SelectionChanged += _gridViewAvailable_SelectionChanged;
			_textAreatAvailableDes = new TextArea();
			_buttonRefresh = new Button {Text = StrRes.GetString("StrRefresh","Refresh")};
			_buttonRefresh.Click += _buttonRefresh_Click;
			_buttonInstall = new Button {Text = StrRes.GetString("StrInstall","Install")};
			_buttonInstall.Click += _buttonInstall_Click;
			page2.Add(_gridViewAvailable, true, true);
			page2.Add(_textAreatAvailableDes, true, false);
			page2.AddSeparateRow(null, _buttonRefresh, _buttonInstall);

			//page3
			var page3 = new DynamicLayout();
			_gridViewUpdatable = new GridView();
			_gridViewUpdatable.ColumnHeaderClick += _gridViewUpdatable_ColumnHeaderClick;
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new CheckBoxCell("Checked"),
				HeaderText = StrRes.GetString("StrChecked","Checked"),
				Editable = true
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				ID = "Name",
				DataCell = new TextBoxCell("Name"), 
				HeaderText = StrRes.GetString("StrName","Name"),
				Sortable = true,
				AutoSize = false,
				Width = 150
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("Author"), 
				HeaderText = StrRes.GetString("StrAuthor","Author"),
				AutoSize = false,
				Width = 100
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("InstalledVersion"), 
				HeaderText = StrRes.GetString("StrInstalledVersion","InstalledVersion"),
				AutoSize = false,
				Width = 150
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("AvailableVersion"), 
				HeaderText = StrRes.GetString("StrAvailableVersion","AvailableVersion"),
				AutoSize = false,
				Width = 150
			});
			_gridViewUpdatable.Columns.Add(new GridColumn()
			{
				DataCell = new TextBoxCell("CanUpdate"), 
				HeaderText = StrRes.GetString("StrCanUpdate","CanUpdate"),
				AutoSize = false,
				Width = 100
			});
			_gridViewUpdatable.SelectionChanged += _gridViewUpdatable_SelectionChanged;
			_textAreatUpdatesDes = new TextArea();
			_buttonUpdate = new Button {Text = StrRes.GetString("StrUpdate","Update")};
			_buttonUpdate.Click += _buttonUpdate_Click;
			page3.Add(_gridViewUpdatable, true, true);
			page3.Add(_textAreatUpdatesDes, true, false);
			page3.AddSeparateRow(null, _buttonUpdate);

			//_tabPageInstalled
			_tabPageInstalled = new TabPage {Text = StrRes.GetString("StrInstalled","Installed")};
			_tabPageInstalled.Content = page1;

			//_tabPageAvailable
			_tabPageAvailable = new TabPage {Text = StrRes.GetString("StrAvailable","Available")};
			_tabPageAvailable.Content = page2;

			//_tabPageUpdatable
			_tabPageUpdatable = new TabPage {Text = StrRes.GetString("StrUpdatable","Updatable")};
			_tabPageUpdatable.Content = page3;

			//_tabControl
			_tabControl = new TabControl();
			_tabControl.Pages.Add(_tabPageInstalled);
			_tabControl.Pages.Add(_tabPageAvailable);
			_tabControl.Pages.Add(_tabPageUpdatable);
			_tabControl.SelectedIndexChanged += _tabControl_SelectedIndexChanged;

			//_buttonSetting
			_buttonSetting = new Button {Text = StrRes.GetString("StrSetting","Setting")};
			_buttonSetting.Click += _buttonSetting_Click;

            //_buttonInstallFrom
            _buttonInstallFrom = new Button { Text = StrRes.GetString("StrInstallPluginFrom", "InstallPluginFrom") };
            _buttonInstallFrom.Click += _buttonInstallFrom_Click;

			//_buttonClose
			_buttonClose = new Button {Text = StrRes.GetString("StrClose","Close")};
			_buttonClose.Click += _buttonClose_Click;

			//_labelMsg
			_labelMsg = new Label
			{
				TextColor = Colors.Red,
				Text =
					StrRes.GetString("StrYourOperationWillTakeEffectWhenTheProgramStartNextTime",
						"Your operation will take effect when the program start next time.")
			};
			_labelMsg.Visible = false;

			var layout = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			layout.Add(_tabControl, true, true);
		    layout.AddSeparateRow(_buttonSetting, _buttonInstallFrom, _labelMsg, null, _buttonClose);

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
	    private Button _buttonInstallFrom;
		private Button _buttonClose;
		private Label _labelMsg;
	}
}
