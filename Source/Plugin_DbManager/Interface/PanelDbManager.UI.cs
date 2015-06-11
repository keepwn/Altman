using Eto.Drawing;
using Eto.Forms;
using Plugin_DbManager.Resources;

namespace Plugin_DbManager.Interface
{
	partial class PanelDbManager
	{
		void Init()
		{
			//_buttonConnect
			_buttonConnect = new Button { Text = StrRes.GetString("StrConnect","Connect"), Image = Icons.ConnectIcon };
			_buttonConnect.Click += ButtonConnect_Click;

			//_buttonDisconnect
			_buttonDisconnect = new Button { Text = StrRes.GetString("StrDisconnect","Disconnect"), Image = Icons.DisconnectIcon };
			_buttonDisconnect.Click += _buttonDisconnect_Click;

			//_dropDownDbs
		    _dropDownDbs = new DropDown {Width = 200};

			//_buttonRunScript
			_buttonRunScript = new Button {Text = StrRes.GetString("StrExecute","Execute"), Image = Icons.RunScripIcon};
			_buttonRunScript.Click += _buttonRunScript_Click;

			//_itemSaveAs
			_itemSaveAs = new ButtonMenuItem { Text = StrRes.GetString("StrSaveAs","SaveAs...") };
			_itemSaveAs.Click += _itemSaveAs_Click;

			//_itemViewTable
			_itemViewTable = new ButtonMenuItem { Text = StrRes.GetString("StrViewTable","ViewTable") };
			_itemViewTable.Click += _itemViewTable_Click;
			_itemViewTable.Enabled = false;

			//_itemCopyName
			_itemCopyName = new ButtonMenuItem { Text = StrRes.GetString("StrCopyName","CopyName") };
			_itemCopyName.Click += _itemCopyName_Click;

			//_menuDbView
			_menuDbView = new ContextMenu();
			_menuDbView.Items.Add(_itemCopyName);
			_menuDbView.Items.Add(_itemViewTable);

			//_menuResultView
			_menuResultView = new ContextMenu();
			_menuResultView.Items.Add(_itemSaveAs);

			//_treeViewDbs
			_treeViewDbs = new TreeView();
			_treeViewDbs.ContextMenu = _menuDbView;
			_treeViewDbs.SelectionChanged += _treeViewDbs_SelectionChanged;
			_treeViewDbs.Activated += _treeViewDbs_Activated;
			_treeViewDbs.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					_treeViewDbs.ContextMenu.Show(_treeViewDbs);
				}
			};

			//_textAreaSql
			_textAreaSql = new TextArea {Font = new Font(FontFamilies.Sans, 12, FontStyle.Bold | FontStyle.Italic)};
			
			//_gridViewResult
			_gridViewResult = new GridView();
			_gridViewResult.ContextMenu = _menuResultView;
			_gridViewResult.Style = "GridViewResult";
			_gridViewResult.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					_gridViewResult.ContextMenu.Show(_treeViewDbs);
				}
			};

			//topLayout
			var topLayout = new DynamicLayout();
			topLayout.BeginHorizontal();
			topLayout.AddCentered(_buttonConnect, horizontalCenter: false);
			topLayout.AddCentered(_buttonDisconnect, horizontalCenter: false);
			topLayout.AddCentered(_dropDownDbs, horizontalCenter: false);
			topLayout.AddCentered(_buttonRunScript, horizontalCenter: false);
			topLayout.Add(null);
			topLayout.EndVertical();

			//rightPanel
			var rightPanel = new Splitter
			{
				Panel1 = _textAreaSql,
				Panel2 = _gridViewResult,
				Orientation = SplitterOrientation.Vertical,
				Position = 100
			};

			//mainLayout
			var mainLayout = new Splitter
			{
				Panel1 = _treeViewDbs,
				Panel2 = rightPanel,
				Orientation = SplitterOrientation.Horizontal,
				Position = 200
			};

			//layout
			var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };
			layout.AddRow(topLayout);
			layout.AddRow(mainLayout);
			Content = layout;
		}

		private Button _buttonConnect;
		private Button _buttonDisconnect;
		private DropDown _dropDownDbs;
		private Button _buttonRunScript;

		private TreeView _treeViewDbs;
		private TextArea _textAreaSql;
		private GridView _gridViewResult;

		private MenuItem _itemSaveAs;
		private MenuItem _itemViewTable;
		private MenuItem _itemCopyName;

		private ContextMenu _menuDbView;
		private ContextMenu _menuResultView;
	}
}
