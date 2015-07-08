using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using Plugin_FileManager.Controls;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Interface
{
	partial class PanelFileManager
	{
		void Init()
		{
			//_textboxUrl
			_textboxUrl = new TextBox();
            _textboxUrl.KeyDown += _textboxUrl_KeyDown;

			//_buttonDir
			_buttonDir = new Button {Text = StrRes.GetString("StrDir","Dir")};
			_buttonDir.Click += _buttonDir_Click;

			//_treeViewDirs
			_treeViewDirs = new TreeViewPlus();
			_treeViewDirs.Activated += _treeViewDirs_Activated;

            //_itemCopyNodePath
            _itemCopyNodePath = new ButtonMenuItem { Text = StrRes.GetString("StrCopyNodePath", "Copy Path") };
            _itemCopyNodePath.Click += _itemCopyNodePath_Click;

            //rightMenu_TreeViewDirs
            var rightMenuTreeViewDirs = new ContextMenu();
            rightMenuTreeViewDirs.Items.Add(_itemCopyNodePath);

            _treeViewDirs.ContextMenu = rightMenuTreeViewDirs;
            _treeViewDirs.MouseUp += (sender, e) =>
            {
                if (e.Buttons == MouseButtons.Alternate)
                {
                    _treeViewDirs.ContextMenu.Show(_treeViewDirs);
                }
            };

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
			_gridViewFile.ColumnHeaderClick += _gridViewFile_ColumnHeaderClick;

			_gridViewFile.Columns.Add(new GridColumn
			{
				ID = "Image",
				HeaderText = ">",
				//DataCell = new ImageTextCell("Image","Name"),
				DataCell = new ImageViewCell("Image"),
				Sortable = true,
				Resizable = false,
				Width = 21
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				ID = "Name",
				HeaderText = StrRes.GetString("StrName","Name"),
				DataCell = new TextBoxCell("Name"),
				Sortable = true,
				AutoSize = false,
				Editable = true,
				Width = 200
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				ID = "Time",
				HeaderText = StrRes.GetString("StrTime","Time"),
				DataCell = new TextBoxCell("FileMTime"),
				Sortable = true,
				AutoSize = false,
				Editable = true,
				Width = 150
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				ID = "Size",
				HeaderText = StrRes.GetString("StrSize","Size"),
				DataCell = new TextBoxCell("FileSize"),
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			_gridViewFile.Columns.Add(new GridColumn
			{
				ID = "Attribute",
				HeaderText = StrRes.GetString("StrAttribute","Attribute"),
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
			layout.AddCentered(_textboxUrl, xscale: true, horizontalCenter: false);
			layout.AddCentered(_buttonDir, horizontalCenter: false);
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
	    ButtonMenuItem _itemCopyNodePath;
		TreeViewPlus _treeViewDirs;
		GridView _gridViewFile;
	}
}
