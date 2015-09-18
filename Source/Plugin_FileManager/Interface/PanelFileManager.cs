using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Util.Common.AltData;
using Altman.Webshell.Model;
using Eto.Drawing;
using Eto.Forms;
using Plugin_FileManager.Controls;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Interface
{
	public partial class PanelFileManager : Panel
	{
		private IHost _host;
		private Shell _shellData;
		private FileManager _fileManager;

		private Status _status;
		private bool _isWin;
		private string _oldName;
		private string[] _oldFiles;
		private string _oldMTime;
		private DataStoreCollection<FileInfoView> _dataStore;

		public PanelFileManager(IHost host, PluginParameter data)
		{
			_host = host;
			_shellData = (Shell)data[0];

			// init StrRes to translate string
			StrRes.SetHost(_host);
			Init();

			//
			_fileManager = new FileManager(_host, _shellData);
			_fileManager.GetWwwRootPathCompletedToDo += fileManager_GetWwwRootPathCompletedToDo;
			_fileManager.GetFileTreeCompletedToDo += fileManager_GetFileTreeCompletedToDo;
			_fileManager.DeleteFileOrDirCompletedToDo += fileManager_DeleteFileOrDirCompletedToDo;	
			_fileManager.RenameFileOrDirCompletedToDo += fileManager_RenameFileOrDirCompletedToDo;
			_fileManager.CopyFileOrDirCompletedToDo += fileManager_CopyFileOrDirCompletedToDo;
			_fileManager.ModifyFileOrDirTimeCompletedToDo += fileManager_ModifyFileOrDirTimeCompletedToDo;
			_fileManager.CreateDirCompletedToDo += fileManager_CreateDirCompletedToDo;
			_fileManager.WgetCompletedToDo += fileManager_WgetCompletedToDo;

			_status = new Status
			{
				PathSeparator = "\\",
				Host = _host,
				ShellData=_shellData,
				FileManager=_fileManager,
				FileGridView = _gridViewFile,	
			};

			_gridViewFile.ContextMenu = CreateFileRightMenu(_status);
			//_dataStore = new List<FileInfoView>();
			_gridViewFile.DataStore = _dataStore = new DataStoreCollection<FileInfoView>();

			//获取根路径
			_fileManager.GetWwwRootPath();
		}

		#region Event
		private void fileManager_GetWwwRootPathCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is OsDisk)
			{
				OsDisk disk = (OsDisk)e.Result;
				string shellDir = disk.ShellDir;
				string[] drives;
				if (disk.AvailableDisk == "/")
				{
					_isWin = false;
					drives = new string[] { "/" };
				}
				else
				{
					_isWin = true;
					drives = disk.AvailableDisk.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
				}
				//设置路径分隔符
				SetPathSeparator(_isWin);
				SetCurrentDirPath(shellDir);
				_host.Ui.ShowMsgInStatusBar("Connect success");

				ShowWwwRootDir(_treeViewDirs, drives, shellDir, _isWin);			
			}
		}

		private void fileManager_GetFileTreeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is List<OsFile>)
			{
				var filetree = e.Result as List<OsFile>;
				var files = new List<OsFile>();
				var dirs = new List<OsFile>();
				foreach (var file in filetree)
				{
					if (file.FileName.EndsWith("/"))
						dirs.Add(file);
					else
						files.Add(file);
				}
				_host.Ui.ShowMsgInStatusBar(string.Format("Dirs[{0}] Files[{1}]", dirs.Count, files.Count));

				ShowFilesAndDirs(_treeViewDirs, _gridViewFile, dirs, files, _isWin);			
			}
		}

		private void fileManager_DeleteFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				if (!(bool)e.Result)
				{
					var msg = "Failed to delete";
				    _host.Ui.ShowMsgInStatusBar(msg);
					_host.Ui.ShowMsgInAppDialog(msg);
				}
				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

		private void fileManager_RenameFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				if (!(bool) e.Result)
				{
					var msg = "Failed to rename";
					_host.Ui.ShowMsgInStatusBar(msg);
					_host.Ui.ShowMsgInAppDialog(msg);
				}
				_host.Ui.ShowMsgInAppDialog("Rename success");

				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

		private void fileManager_CopyFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				if (!(bool)e.Result)
				{
					var msg = "Failed to copy file";
					_host.Ui.ShowMsgInStatusBar(msg);
					_host.Ui.ShowMsgInAppDialog(msg);
				}
				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

		private void fileManager_CreateDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				if (!(bool)e.Result)
				{
					var msg = "Failed to create the folder";
					_host.Ui.ShowMsgInStatusBar(msg);
					_host.Ui.ShowMsgInAppDialog(msg);
				}
				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

		private void fileManager_ModifyFileOrDirTimeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				if (!(bool)e.Result)
				{
					var msg = "Failed to modify file's time";
					_host.Ui.ShowMsgInStatusBar(msg);
					_host.Ui.ShowMsgInAppDialog(msg);
				}
				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

		private void fileManager_WgetCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				string msg = string.Empty;
				if (!(bool)e.Result)
				{
					msg = "Download file to the remote server:Failed";
				}
				else
				{
					msg = "Download file to the remote server:Succeed";
				}
				_host.Ui.ShowMsgInStatusBar(msg);
				_host.Ui.ShowMsgInAppDialog(msg);

				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

		#endregion

		#region UI

		ContextMenu CreateFileRightMenu(Status status)
		{
			//contextMenu
			var contextMenu = new ContextMenu();
			contextMenu.Items.Add(new Actions.ItemRefresh(status));
			contextMenu.Items.AddSeparator();
			contextMenu.Items.Add(new Actions.ItemUpload(status));
			contextMenu.Items.Add(new Actions.ItemDownload(status));
			contextMenu.Items.Add(new Actions.ItemDownloadToServer(status));
			contextMenu.Items.AddSeparator();
			contextMenu.Items.Add(new Actions.ItemEdit(status));
			contextMenu.Items.Add(new Actions.ItemRename(status));
			contextMenu.Items.Add(new Actions.ItemModifyTime(status));
            contextMenu.Items.AddSeparator();
            contextMenu.Items.Add(new Actions.ItemCopy(status));
            contextMenu.Items.Add(new Actions.ItemPaste(status));
            contextMenu.Items.AddSeparator();
			var create = contextMenu.Items.GetSubmenu(StrRes.GetString("StrNew", "New"));
			create.Items.Add(new Actions.ItemCreateDir(status));
			create.Items.Add(new Actions.ItemCreateFile(status));
            contextMenu.Items.Add(new Actions.ItemDelete(status));

			return contextMenu;
		}

		void _buttonDir_Click(object sender, EventArgs e)
		{
            var current = GetCurrentDirPath();
            if (current != "")
			{
                SetCurrentDirPath(current);
                _fileManager.GetFileTree(current);
			}
		}

        void _textboxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.Enter)
            {
                var current = GetCurrentDirPath();
                if (current != "")
                {
                    SetCurrentDirPath(current);
                    MoveCareIndexToEnd();
                    _fileManager.GetFileTree(current);
                }
            }
        }

		void _treeViewDirs_Activated(object sender, TreeViewItemEventArgs e)
		{
			SetCurrentDirPath(_treeViewDirs.SelectedFullPath);
			_fileManager.GetFileTree(GetCurrentDirPath());
		}

        void _itemCopyNodePath_Click(object sender, EventArgs e)
        {
            var path = _treeViewDirs.SelectedFullPath;
            if (!string.IsNullOrEmpty(path))
                new Clipboard().Text = path;
        }

		void _gridViewFile_CellDoubleClick(object sender, GridViewCellEventArgs e)
		{
			var selectFile = e.Item as FileInfoView;
			if (selectFile != null && selectFile.IsDir)
			{
				SetCurrentDirPath(selectFile.FullName);
				_fileManager.GetFileTree(GetCurrentDirPath());
			}
		}

	    void _gridViewFile_CellEditing(object sender, GridViewCellEventArgs e)
	    {
	        var gridView = sender as GridView;
	        if (gridView == null)
	            return;
	        _oldName = (gridView.SelectedItem as FileInfoView).Name;
	        _oldFiles = (gridView.DataStore as DataStoreCollection<FileInfoView>).Select(r => r.Name).ToArray();
	        _oldMTime = (gridView.SelectedItem as FileInfoView).FileMTime;
	    }

	    void _gridViewFile_CellEdited(object sender, GridViewCellEventArgs e)
		{
			var editFile = e.Item as FileInfoView;
            var gridView = sender as GridView;
            // winform: sometimes doubleclick in editing cell also trigger CellEdited event
            if (gridView ==null || editFile == null)
		        return;
            var items = gridView.DataStore as DataStoreCollection<FileInfoView>;
			if (e.Column == 1) //edit name
			{
				if (editFile.IsCreateing) //create dir
				{
					var newText = editFile.Name;
					if (string.IsNullOrEmpty(newText) || newText == _oldName)
					{
						Undo(items, e.Row, _oldName, EditType.CreateDir);
						return;
					}
					if (_oldFiles.FirstOrDefault(r => r == newText) != null)
					{
						MessageBox.Show("This name already exists, please rename");
						Undo(items, e.Row, _oldName, EditType.CreateDir);
						return;
					}
					//创建文件夹
					var currentDir = GetCurrentDirPath();
					string dirName = newText;
					string dirFullPath = currentDir + _status.PathSeparator + dirName;
					_fileManager.CreateDir(dirFullPath);
				}
				else //rename
				{
					var newText = editFile.Name;					
					if (string.IsNullOrEmpty(newText) || newText == _oldName)
					{
						Undo(items, e.Row, _oldName,EditType.Rename);
						return;
					}
					if (_oldFiles.FirstOrDefault(r => r == newText) != null)
					{
						MessageBox.Show("This name already exists, please rename");
						Undo(items, e.Row, _oldName, EditType.Rename);
						return;
					}
					//发送重命名请求
					//oldFileName,newFileName
					//为了安全起见，这里使用绝对路径
					var currentDir = GetCurrentDirPath();
					string oldFileName = _oldName;
					string newFileName = newText;
					string oldFileNameFullPath = currentDir + _status.PathSeparator + oldFileName;
					string newFileNameFullPath = currentDir + _status.PathSeparator + newFileName;
					_fileManager.RenameFileOrDir(oldFileNameFullPath, newFileNameFullPath);
				}					
			}
			else if (e.Column == 2)//edit mtime
			{
				var newText = editFile.FileMTime;
				if (string.IsNullOrEmpty(newText) || newText == _oldMTime)
				{
					Undo(items, e.Row, _oldMTime, EditType.EditMTime);
					return;
				}
				DateTime result;
				if (!DateTime.TryParse(newText, out result))
				{
					MessageBox.Show("This time's format is error, please retry");
					Undo(items, e.Row, _oldMTime, EditType.EditMTime);
					return;
				}
				//修改时间
				_fileManager.ModifyFileOrDirTime(editFile.FullName, newText);
			}
		}

		enum EditType
		{
			Rename,
			EditMTime,
			CreateDir
		}
		void Undo(DataStoreCollection<FileInfoView> dataStore, int row, string oldText, EditType editType)
		{
			if (editType == EditType.Rename)
			{
				(dataStore)[row].Name= oldText;
				//((List<FileInfoView>) dataStore)[row].Name = oldText;
			}		
			else if (editType == EditType.EditMTime)
			{
				(dataStore)[row].FileMTime = oldText;
				//((List<FileInfoView>) dataStore)[row].FileMTime = oldText;
			}
			else if (editType == EditType.CreateDir)
			{
				dataStore.RemoveAt(row);
				//Application.Instance.Invoke(() =>((DataStoreCollection)dataStore).RemoveAt(row));
			}
		}

		#endregion

		#region Sort Columns
		private bool _typeIsAscending;
		private bool _nameIsAscending;
		private bool _timeIsAscending;
		private bool _sizeIsAscending;
		private bool _attributeIsAscending;

		public static int SortIntAscending(string x, string y, bool isAscending)
		{
			if (string.IsNullOrWhiteSpace(x))
			{
				if (string.IsNullOrWhiteSpace(y))
					return 0;
				return isAscending ? -1 : 1;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(y))
				{
					return isAscending ? 1 : -1;
				}
				try
				{
					var a = Int32.Parse(x);
					var b = Int32.Parse(y);
					return isAscending ? (a - b) : -(a - b);
				}
				catch
				{
					return 0;
				}
			}
		}
		public static int SortIntAscending(int x, int y, bool isAscending)
		{
			return isAscending ? (x - y) : -(x - y);
		}
		public static int SortStringAscending(string x, string y, bool isAscending)
		{
			if (isAscending)
				return String.Compare(x, y, StringComparison.Ordinal);
			return -String.Compare(x, y, StringComparison.Ordinal);
		}
		public static int SortTimeAscending(string x, string y, bool isAscending)
		{
			try
			{
				var a = DateTime.Parse(x);
				var b = DateTime.Parse(y);
				return isAscending ? DateTime.Compare(a, b) : -DateTime.Compare(a, b);
			}
			catch
			{
				return 0;
			}
		}

		void _gridViewFile_ColumnHeaderClick(object sender, GridColumnEventArgs e)
		{
			var items = _gridViewFile.DataStore as DataStoreCollection<FileInfoView>;
			if (items == null) return;
			switch (e.Column.ID)
			{
				case "Image": //type
					_typeIsAscending = !_typeIsAscending;
					items.Sort((a, b) => SortIntAscending((int)a.Type, (int)b.Type, _typeIsAscending));
					break;
				case "Name":
					_nameIsAscending = !_nameIsAscending;
					items.Sort((a, b) => SortStringAscending(a.Name, b.Name, _nameIsAscending));
					break;
				case "Time":
					_timeIsAscending = !_timeIsAscending;
					items.Sort((a, b) => SortStringAscending(a.FileMTime, b.FileMTime, _timeIsAscending));
					break;
				case "Size":
					_sizeIsAscending = !_sizeIsAscending;
					items.Sort((a, b) => SortIntAscending(a.FileSizeInt, b.FileSizeInt, _sizeIsAscending));
					break;
				case "Attribute":
					_attributeIsAscending = !_attributeIsAscending;
					items.Sort((a, b) => SortStringAscending(a.FileAttributes, b.FileAttributes, _attributeIsAscending));
					break;
				default:
					break;
			}
		}
		#endregion

	    private void MoveCareIndexToEnd()
	    {
            // move caretIndex to end (winform and gtk)
            // textbox must on focus (mac)
            _textboxUrl.CaretIndex = _textboxUrl.Text.Length;
	    }

		private void SetPathSeparator(bool isWin)
		{
			_status.PathSeparator = _treeViewDirs.PathSeparator = isWin ? "\\" : "/";
		}

		private string GetCurrentDirPath()
		{
			return _textboxUrl.Text;
		}

		private void SetCurrentDirPath(string currentDir)
		{
			string sep = _isWin ? "\\" : "/";
			string dir = currentDir;
			if (dir.EndsWith(sep))
			{
				_status.CurrentDirPath = _textboxUrl.Text = dir;
			}
			else
			{
				_status.CurrentDirPath = _textboxUrl.Text = dir + sep;
			}
		}

        private void ShowWwwRootDir(TreeViewPlus treeView, IEnumerable<string> driveNames, string wwwRootDirPath, bool isWin)
		{
			var treeItem = new TreeItem();

			AddDrivesInDirTree(treeItem, driveNames);
            var foot = AddDirInDirTree(treeItem, wwwRootDirPath, isWin);

			treeView.DataStore = treeItem;
            treeView.Expand(foot);

            // refresh
		    new Actions.ItemRefresh(_status).Execute();
		}
		
		private void AddDrivesInDirTree(TreeItem treeItem, IEnumerable<string> driveNames)
		{
			foreach (var drive in driveNames)
			{
				var tmp = new TreeItem
				{
					Text = drive,
					Image = Icons.TreeType.DriveIcon
				};
				treeItem.Children.Add(tmp);
			}
		}

        private TreeItem AddDirInDirTree(TreeItem treeItem, string dirFullPath, bool isWin)
		{
		    var result = ConvertTreePathToTreeNode(treeItem, dirFullPath, isWin);
            if (result.Item2 == null)
            {
                return result.Item1;
            }
		    else
		    {
		        var tmp = result.Item1;
		        var dirsList = result.Item2;
                var index = 0;

                while (index < dirsList.Count)
                {
                    var tmpNodeName = dirsList[index];

                    var find = tmp.Children.FirstOrDefault(r => r.Text == tmpNodeName) as TreeItem;
                    if (find == null)
                    {
                        var newItem = new TreeItem
                        {
                            Text = tmpNodeName,
                            Image = Icons.TreeType.FloderIcon
                        };
                        tmp.Children.Add(newItem);
                        tmp = newItem;
                    }
                    else
                    {
                        tmp = find;
                    }                   
                    index++;
                }
		        return tmp;
		    }
		}

        private TreeItem FindDirInDirTree(TreeItem treeItem, string dirFullPath, bool isWin)
        {
            var result = ConvertTreePathToTreeNode(treeItem, dirFullPath, isWin);
            return result.Item2 == null ? result.Item1 : null;
        }

        private Tuple<TreeItem, List<string>> ConvertTreePathToTreeNode(TreeItem treeItem, string dirFullPath, bool isWin)
	    {
            var pathsList = dirFullPath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if(!isWin)
                pathsList.Insert(0, "/");

            TreeItem tmp = treeItem;
            int index = 0;
            while (index < pathsList.Count)
            {
                string tmpNodeName = pathsList[index];

                var find = tmp.Children.FirstOrDefault(r => r.Text == tmpNodeName) as TreeItem;
                if (find == null)
                {
                    pathsList.RemoveRange(0, index);
                    return new Tuple<TreeItem, List<string>>(tmp, pathsList);
                }
                else
                    tmp = find;
                index++;
            }
            return new Tuple<TreeItem, List<string>>(tmp, null);
	    }

		private TreeItem AddSubDirsInDirTree(TreeItem treeItem, IEnumerable<string> subDirNames, string currentDirFullPath, bool isWin)
		{
			var selectedNode = FindDirInDirTree(treeItem, currentDirFullPath, isWin);
			if (selectedNode != null)
			{
				foreach (var dir in subDirNames)
				{
					var find = selectedNode.Children.FirstOrDefault(r => r.Text == dir) as TreeItem;
					if (find == null)
					{
						var newItem = new TreeItem
						{
							Text = dir,
							Image = Icons.TreeType.FloderIcon
						};
						selectedNode.Children.Add(newItem);
					}
				}
			}
			return selectedNode;
		}

		private void ShowFilesAndDirs(TreeViewPlus dirView, GridView fileView, List<OsFile> dirs, List<OsFile> files, bool isWin)
		{
			//show dirs in DirTree
			var newDirs = dirs.Select(dir => dir.FileName.Remove(dir.FileName.Length - 1, 1)).ToList();
			var currentDir = GetCurrentDirPath();
			var treeItem = dirView.DataStore as TreeItem;
			var selectedNode = AddSubDirsInDirTree(treeItem, newDirs, currentDir, isWin);
			if (selectedNode == null)
				return;

			//show dirs,files in fileview
			AddDirsInListViewFile(dirs, files, currentDir);

			//expanded
		    selectedNode.Expanded = true;
            dirView.RefreshItem(selectedNode);
		}

		private void AddDirsInListViewFile(IEnumerable<OsFile> dirs, IEnumerable<OsFile> files, string parentPath)
		{
			_dataStore.Clear();

			var item = new DataStoreCollection<FileInfoView>();
			//显示dirs
			foreach (OsFile dir in dirs)
			{
				//移除最后的'/'符号
				string dirName = dir.FileName.Remove(dir.FileName.Length - 1, 1);
				//添加fileInfo
				string fullName = Path.Combine(new string[] { parentPath, dirName }) + (_isWin ? "\\" : "/");
				item.Add(new FileInfoView(dirName, fullName, true, dir.FileMTime, dir.FileSize, dir.FilePerms));
			}
			//显示files
			foreach (OsFile file in files)
			{
				//添加fileInfo
				string dirName = file.FileName;
				string fullName = Path.Combine(new string[] { parentPath, dirName });
				item.Add(new FileInfoView(dirName, fullName, false, file.FileMTime, file.FileSize, file.FilePerms));
			}
			_dataStore.AddRange(item);
		}
	}
}
