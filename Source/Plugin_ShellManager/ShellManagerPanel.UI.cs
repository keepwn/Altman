using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_ShellManager
{
	partial class ShellManagerPanel
	{
		void Init()
		{
			//_itemRefreshStatus
			_itemRefreshStatus = new ButtonMenuItem { Text = "Refresh Status" };
			_itemRefreshStatus.Click += _itemRefreshStatus_Click;
			//_itemCopyServerCode
			_itemCopyServerCode = new ButtonMenuItem { Text = "Copy ServerCode" };
			_itemCopyServerCode.Click += _itemCopyServerCode_Click;
			//_itemAdd
			_itemAdd = new ButtonMenuItem { Text = "Add" };
			_itemAdd.Click += _itemAdd_Click;
			//_itemEdit
			_itemEdit = new ButtonMenuItem { Text = "Edit" };
			_itemEdit.Click += _itemEdit_Click;
			//_itemDelete
			_itemDelete = new ButtonMenuItem { Text = "Delete" };
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
			_gridViewShell.CellDoubleClick += _gridViewShell_CellDoubleClick;

			var layout = new DynamicLayout { Padding = new Padding(0), Spacing = new Size(10, 10) };
			layout.Add(_gridViewShell);

			this.Content = layout;
		}

		GridView CreateListView()
		{
			//_gridViewHeader
			var gridView = new GridView()
			{
				AllowMultipleSelection = false,
				BackgroundColor = Colors.White,
				ShowCellBorders = false,
			};
			//Id
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Id",
				DataCell = new TextBoxCell("Id"),
				Editable = false,
				Sortable = true,
				AutoSize = true
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
				Sortable = true,
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
				Sortable = true,
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
