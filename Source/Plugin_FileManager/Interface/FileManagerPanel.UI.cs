using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Desktop.CustomControls;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_FileManager.Interface
{
	partial class FileManagerPanel
	{
		void Init()
		{
			//_textboxUrl
			_textboxUrl = new TextBox();

			//_buttonDir
			_buttonDir = new Button {Text = "Dir"};
			_buttonDir.Click += _buttonDir_Click;

			//_treeViewDirs
			_treeViewDirs = new TreeViewPlus();
			_treeViewDirs.Activated += _treeViewDirs_Activated;

			_gridViewFile = new GridView
			{
				AllowMultipleSelection = false,
				BackgroundColor = Colors.White,
				ShowCellBorders = false,
				RowHeight = 21
			};
			_gridViewFile.CellEditing += _gridViewFile_CellEditing;
			_gridViewFile.CellEdited += _gridViewFile_CellEdited;
			_gridViewFile.CellDoubleClick += _gridViewFile_CellDoubleClick;

			_gridViewFile.Columns.Add(new GridColumn
			{
				HeaderText = "",
				//DataCell = new ImageTextCell("Image","Name"),
				DataCell = new ImageViewCell("Image"),
				Resizable = false,
				Width = 21
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				HeaderText = "Name",
				DataCell = new TextBoxCell("Name"),
				Sortable = true,
				AutoSize = false,
				Editable = true,
				Width = 200
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				HeaderText = "Time",
				DataCell = new TextBoxCell("FileMTime"),
				Sortable = true,
				AutoSize = false,
				Editable = true,
				Width = 150
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				HeaderText = "Size",
				DataCell = new TextBoxCell("FileSize"),
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				HeaderText = "Attribute",
				DataCell = new TextBoxCell("FileAttributes"),
				Sortable = true,
				AutoSize = false,
				Width = 70
			});

			_gridViewFile.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					if (_gridViewFile.ContextMenu!=null)
						_gridViewFile.ContextMenu.Show(_gridViewFile);
				}
			};

			var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.Add(_textboxUrl, true);
			layout.Add(_buttonDir);
			layout.EndHorizontal();
			layout.EndVertical();

			layout.AddRow(new Splitter
			{
				Panel1 = _treeViewDirs,
				Panel2 = _gridViewFile,
				Orientation = SplitterOrientation.Horizontal,
				Position = 200,
			});

			Content = layout;
		}

		TextBox _textboxUrl;
		Button _buttonDir;
		TreeViewPlus _treeViewDirs;
		GridView _gridViewFile;
	}
}
