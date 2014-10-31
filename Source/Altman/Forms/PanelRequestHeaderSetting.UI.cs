using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class PanelRequestHeaderSetting
	{
		void Init()
		{
			//_gridViewHeader
			_gridViewHeader = new GridView()
			{
				AllowMultipleSelection = false,
				BackgroundColor = Colors.White,
				Size = new Size(200, 200)
			};
			_gridViewHeader.ShowCellBorders = true;
			_gridViewHeader.Columns.Add(new GridColumn
			{
				HeaderText = "Key",
				DataCell = new TextBoxCell("Key"),
				Editable = false,
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

			var items = new DataStoreCollection();
			//items.Add(new HeaderItem("Accept", "*"));
			//items.Add(new HeaderItem("User-Agent", "IE8.0"));
			//items.Add(new HeaderItem(null, null));
			_gridViewHeader.DataStore = items;

			_gridViewHeader.CellEditing += _gridViewHeader_CellEditing;
			_gridViewHeader.CellEdited += _gridViewHeader_CellEdited;

			//_contextMenuRightMenu
			_contextMenuRightMenu = new ContextMenu();
			var add = _contextMenuRightMenu.Items.GetSubmenu("Add");

			var layout = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(10, 10) };
			layout.AddRow(_gridViewHeader);
			layout.Add(null);

			this.Content = layout;
		}

		private GridView _gridViewHeader;
		private ContextMenu _contextMenuRightMenu;
	}
}
