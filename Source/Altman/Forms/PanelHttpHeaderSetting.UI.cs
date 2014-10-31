using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class PanelHttpHeaderSetting
	{
		void Init()
		{
			//_contextMenuRightMenu
			_contextMenuRightMenu = new ContextMenu();
			var addItem = new ButtonMenuItem {Text = "Add"};
			addItem.Click += addItem_Click;
			var delItem = new ButtonMenuItem {Text = "Delete"};
			delItem.Click += delItem_Click;
			_contextMenuRightMenu.Items.Add(addItem);
			_contextMenuRightMenu.Items.Add(delItem);

			//_gridViewHeader
			_gridViewHeader = new GridView
			{
				AllowMultipleSelection = false,
				BackgroundColor = Colors.White,
				Size = new Size(200, 200),
				ShowCellBorders = true
			};
			//_gridViewHeader.CellEditing += _gridViewHeader_CellEditing;
			//_gridViewHeader.CellEdited += _gridViewHeader_CellEdited;

			_gridViewHeader.Columns.Add(new GridColumn
			{
				HeaderText = "Key",
				DataCell = new TextBoxCell("Key"),
				Editable = true,
				Sortable = true,
				AutoSize = true
			});
			_gridViewHeader.Columns.Add(new GridColumn
			{
				HeaderText = "Value",
				DataCell = new TextBoxCell("Value"),
				Editable = true,
				Sortable = true,
				AutoSize = false,
				Width = 200
			});
			_gridViewHeader.ContextMenu = _contextMenuRightMenu;
			_gridViewHeader.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					if (_gridViewHeader.ContextMenu != null)
						_gridViewHeader.ContextMenu.Show(this);
				}
			};

			var layout = new DynamicLayout { Padding = new Padding(20, 20), Spacing = new Size(10, 10) };
			layout.AddRow(_gridViewHeader);
			layout.Add(null);

			this.Content = layout;
		}

		private GridView _gridViewHeader;
		private ContextMenu _contextMenuRightMenu;
	}
}
