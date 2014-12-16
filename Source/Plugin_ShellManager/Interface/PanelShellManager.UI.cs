using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager.Interface
{
	partial class PanelShellManager
	{
		void InitUi()
		{
			//_itemRefreshStatus
			_itemRefreshStatus = new ButtonMenuItem { Text = StrRes.GetString("StrRefreshStatus", "Refresh Status") };
			_itemRefreshStatus.Click += _itemRefreshStatus_Click;
			//_itemCopyServerCode
			_itemCopyServerCode = new ButtonMenuItem { Text = StrRes.GetString("StrCopyServerCode", "Copy ServerCode") };
			_itemCopyServerCode.Click += _itemCopyServerCode_Click;
			//_itemAdd
			_itemAdd = new ButtonMenuItem { Text =  StrRes.GetString("StrAdd","Add") };
			_itemAdd.Click += _itemAdd_Click;
			//_itemEdit
			_itemEdit = new ButtonMenuItem { Text =  StrRes.GetString("StrEdit","Edit") };
			_itemEdit.Click += _itemEdit_Click;
			//_itemDelete
			_itemDelete = new ButtonMenuItem { Text =  StrRes.GetString("StrDelete","Delete") };
			_itemDelete.Click += _itemDelete_Click;

			//rightMenu_Webshell
			_rightMenuWebshell = new ContextMenu();
			_rightMenuWebshell.Items.Add(_itemRefreshStatus);
			_rightMenuWebshell.Items.Add(_itemCopyServerCode);
			_rightMenuWebshell.Items.Add(new SeparatorMenuItem());
			_rightMenuWebshell.Items.Add(_itemAdd);
			_rightMenuWebshell.Items.Add(_itemEdit);
			_rightMenuWebshell.Items.Add(_itemDelete);
			_rightMenuWebshell.Items.Add(new SeparatorMenuItem());

			//_gridViewShell
			_gridViewShell = CreateListView();
			_gridViewShell.ContextMenu = _rightMenuWebshell;
			_gridViewShell.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					_gridViewShell.ContextMenu.Show(_gridViewShell);
				}
			};
			//_gridViewShell.CellDoubleClick += _gridViewShell_CellDoubleClick;
			//_gridViewShell.ColumnHeaderClick += _gridViewShell_ColumnHeaderClick;

			var layout = new DynamicLayout { Padding = new Padding(0), Spacing = new Size(10, 10) };
			layout.Add(_gridViewShell);

			this.Content = layout;
		}

		GridView CreateListView()
		{
			//_gridViewHeader
			var gridView = new GridView()
			{
				AllowMultipleSelection = true,
				BackgroundColor = Colors.White,
				ShowCellBorders = false,
			};
			gridView.ColumnHeaderClick += _gridViewShell_ColumnHeaderClick;
			gridView.CellDoubleClick += _gridViewShell_CellDoubleClick;
			//Id
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Id",
				DataCell = new TextBoxCell("Id"),
				Editable = false,
				Sortable = true,
				AutoSize = true,
			});
			//Name
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Name",
				DataCell = new TextBoxCell("TargetId"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			//Level
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Level",
				DataCell = new TextBoxCell("TargetLevel"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 50
			});
			//Status
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Status",
				DataCell = new TextBoxCell("Status"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 50
			});
			//ShellUrl
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "ShellUrl",
				DataCell = new TextBoxCell("ShellUrl"),
				Editable = false,
				Sortable = false,
				AutoSize = false,
				Width = 250
			});
			//Type
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Type",
				DataCell = new TextBoxCell("ShellType"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			//Remark
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Remark",
				DataCell = new TextBoxCell("Remark"),
				Editable = false,
				Sortable = false,
				AutoSize = false,
				Width = 100
			});
			//AddTime
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "AddTime",
				DataCell = new TextBoxCell("AddTime"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			return gridView;
		}

		private ContextMenu _rightMenuWebshell;
		private ButtonMenuItem _itemRefreshStatus;
		private ButtonMenuItem _itemCopyServerCode;
		private ButtonMenuItem _itemAdd;
		private ButtonMenuItem _itemEdit;
		private ButtonMenuItem _itemDelete;
		private GridView _gridViewShell;
	}
}
