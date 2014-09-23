using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Altman.Common.AltData;
using Altman.Desktop.CustomControls;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;
using FileInfo = Plugin_FileManager.Model.FileInfo;

namespace Plugin_FileManager.Interface
{
	public class FileManagerSection : Panel
	{
		private IHost _host;
		private Shell _shellData;
		private FileManagerService _fileManager;

		TextBox urlTextbox;
		Button dirButton;
		TreeViewPlus dirsTreeView;
		GridView fileGridView;

		private Model.Status status;
		private bool _isWin;
		private string _oldName;
		private string[] _oldFiles;
		private string _oldMTime;
		private DataStoreCollection _dataStore;

		public FileManagerSection(IHost host, Shell data)
		{
			Content = CreateLayout();

			_host = host;
			_shellData = data;

			//
			_fileManager = new FileManagerService(_host, _shellData);
			_fileManager.GetWwwRootPathCompletedToDo += fileManager_GetWwwRootPathCompletedToDo;
			_fileManager.GetFileTreeCompletedToDo += fileManager_GetFileTreeCompletedToDo;
			_fileManager.DeleteFileOrDirCompletedToDo += fileManager_DeleteFileOrDirCompletedToDo;	
			_fileManager.RenameFileOrDirCompletedToDo += fileManager_RenameFileOrDirCompletedToDo;
			_fileManager.CopyFileOrDirCompletedToDo += fileManager_CopyFileOrDirCompletedToDo;
			_fileManager.ModifyFileOrDirTimeCompletedToDo += fileManager_ModifyFileOrDirTimeCompletedToDo;
			_fileManager.CreateDirCompletedToDo += fileManager_CreateDirCompletedToDo;
			_fileManager.WgetCompletedToDo += fileManager_WgetCompletedToDo;

			status = new Model.Status
			{
				PathSeparator = "\\",
				Host = _host,
				ShellData=_shellData,
				FileManager=_fileManager,
				FileGridView = fileGridView,	
			};

			//_dataStore = new List<FileInfo>();

			//获取根路径
			_fileManager.GetWwwRootPath();
		}

		#region Event
		private void fileManager_GetWwwRootPathCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
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
				_host.Ui.ShowMsgInStatusBar("Connect succeed");

				ShowWwwRootDir(dirsTreeView, drives, shellDir, _isWin);			
			}
		}

		private void fileManager_GetFileTreeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
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

				ShowFilesAndDirs(dirsTreeView, fileGridView, dirs, files, _isWin);			
			}
		}

		private void fileManager_DeleteFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
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
		Control UrlTextBox()
		{
			var control = urlTextbox = new TextBox();
			return control;
		}

		Control DirButton()
		{
			var control = dirButton = new Button
			{
				Text = "Dir"
			};
			control.Click += delegate
			{

			};
			return control;
		}

		Control DirsTreeView()
		{
			var control = dirsTreeView = new TreeViewPlus();
			control.Activated += (sender,e)=>
			{
				SetCurrentDirPath(control.SelectedFullPath);
				_fileManager.GetFileTree(GetCurrentDirPath());
			};
			return control;
		}

		ContextMenu RightMenu()
		{
			var menu = new ContextMenu();
			menu.Items.Add(new Actions.ItemRefresh(status));
			menu.Items.AddSeparator();
			menu.Items.Add(new Actions.ItemUpload(status));
			menu.Items.Add(new Actions.ItemDownload(status));
			menu.Items.Add(new Actions.ItemDownloadToServer(status));
			menu.Items.AddSeparator();
			menu.Items.Add(new Actions.ItemDelete(status));
			menu.Items.Add(new Actions.ItemEdit(status));
			menu.Items.Add(new Actions.ItemCopy(status));
			menu.Items.Add(new Actions.ItemPaste(status));
			menu.Items.Add(new Actions.ItemRename(status));
			menu.Items.Add(new Actions.ItemModifyTime(status));

			var create = menu.Items.GetSubmenu("Add");
			create.Items.Add(new Actions.ItemCreateDir(status));
			create.Items.Add(new Actions.ItemCreateFile(status));

			return menu;
		}

		Control FileGridView()
		{
			var control = fileGridView = new GridView
			{
				AllowMultipleSelection = false,
				BackgroundColor = Colors.White,
				ShowCellBorders = false,
				RowHeight=21
			};
			control.CellEditing += control_CellEditing;
			control.CellEdited += control_CellEdited;

			control.Columns.Add(new GridColumn
			{
				HeaderText = "",
				//DataCell = new ImageTextCell("Image","Name"),
				DataCell = new ImageViewCell("Image"),
				Resizable = false,
				Width = 21
			});
			control.Columns.Add(new GridColumn
			{
				HeaderText = "Name",
				DataCell = new TextBoxCell("Name"),
				Sortable = true,
				AutoSize = false,
				Editable = true,
				Width = 200
			});
			control.Columns.Add(new GridColumn
			{
				HeaderText = "Time",
				DataCell = new TextBoxCell("FileMTime"),
				Sortable = true,
				AutoSize = false,
				Editable = true,
				Width = 150
			});
			control.Columns.Add(new GridColumn
			{
				HeaderText = "Size",
				DataCell = new TextBoxCell("FileSize"),
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			control.Columns.Add(new GridColumn
			{
				HeaderText = "Attribute",
				DataCell = new TextBoxCell("FileAttributes"),
				Sortable = true,
				AutoSize = false,
				Width = 70
			});
			control.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					RightMenu().Show(control);
				}
			};

			fileGridView.DataStore = _dataStore = new DataStoreCollection();
			return control;
		}

		void control_CellEditing(object sender, GridViewCellEventArgs e)
		{
			_oldName = ((sender as GridView).SelectedItem as FileInfo).Name;
			_oldFiles = ((sender as GridView).DataStore as DataStoreCollection).Select(r => (r as FileInfo).Name).ToArray();
			_oldMTime = ((sender as GridView).SelectedItem as FileInfo).FileMTime;
		}

		void control_CellEdited(object sender, GridViewCellEventArgs e)
		{
			var editFile = e.Item as FileInfo;
			var items = (sender as GridView).DataStore;
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
					string dirFullPath = currentDir + status.PathSeparator + dirName;
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
					string oldFileNameFullPath = currentDir + status.PathSeparator + oldFileName;
					string newFileNameFullPath = currentDir + status.PathSeparator + newFileName;
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
		void Undo(IEnumerable<object> dataStore, int row, string oldText, EditType editType)
		{
			if (editType == EditType.Rename)
			{
				(((DataStoreCollection)dataStore)[row] as FileInfo).Name= oldText;
				//((List<FileInfo>) dataStore)[row].Name = oldText;
			}		
			else if (editType == EditType.EditMTime)
			{
				(((DataStoreCollection)dataStore)[row] as FileInfo).FileMTime = oldText;
				//((List<FileInfo>) dataStore)[row].FileMTime = oldText;
			}
			else if (editType == EditType.CreateDir)
			{
				((DataStoreCollection)dataStore).RemoveAt(row);
				//Application.Instance.Invoke(() =>((DataStoreCollection)dataStore).RemoveAt(row));
			}
		}

		Control CreateLayout()
		{
			var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.Add(UrlTextBox(), true);
			layout.Add(DirButton());
			layout.EndHorizontal();
			layout.EndVertical();

			layout.AddRow(new Splitter
			{
				Panel1 = DirsTreeView(),
				Panel2 = FileGridView(),
				Orientation = SplitterOrientation.Horizontal,
				Position = 200,
			});
			return layout;
		}
		#endregion

		private void SetPathSeparator(bool isWin)
		{
			status.PathSeparator = dirsTreeView.PathSeparator = isWin ? "\\" : "/";
		}

		private string GetCurrentDirPath()
		{
			return urlTextbox.Text;
		}

		private void SetCurrentDirPath(string currentDir)
		{
			string sep = _isWin ? "\\" : "/";
			string dir = currentDir;
			if (dir.EndsWith(sep))
			{
				status.CurrentDirPath = urlTextbox.Text = dir;
			}
			else
			{
				status.CurrentDirPath = urlTextbox.Text = dir + sep;
			}
		}

		private void ShowWwwRootDir(TreeView treeView, IEnumerable<string> drives, string wwwRootDir, bool isWin)
		{
			var treeItem = new TreeItem();

			AddDriveInDirTree(treeItem, drives);
			AddDirInDirTree(treeItem, wwwRootDir, isWin);
			treeView.DataStore = treeItem;

			//RefreshAllFiles(GetCurrentDirPath());
			new Actions.ItemRefresh(status).Execute();
		}
		
		private void AddDriveInDirTree(TreeItem treeItem, IEnumerable<string> drives)
		{
			foreach (var drive in drives)
			{
				var tmp = new TreeItem
				{
					Text = drive,
					Image = Icons.TreeType.DriveIcon
				};
				treeItem.Children.Add(tmp);
			}
		}
		
		private void AddDirInDirTree(TreeItem treeItem, string dirFullPath, bool isWin)
		{
			string[] paths = dirFullPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
			List<string> pathsList = new List<string>(paths);
			TreeItem tmp = null;
			if (!isWin)
			{
				//若为linux主机，直接用第一个节点赋值（根节点"/"）
				tmp = treeItem.Children[0] as TreeItem;
			}
			else
			{
				//若为windows主机，数组[0]为磁盘
				tmp = treeItem.Children.FirstOrDefault(r => r.Text == pathsList[0]) as TreeItem;
				if (tmp == null)
					return;
				else
					pathsList.RemoveAt(0);
			}
			int index = 0;
			while (index < pathsList.Count)
			{
				string tmpNodeName = pathsList[index];

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
					tmp = find;
				index++;
			}
		}

		private TreeItem AddDirInDirTree(TreeItem treeItem, IEnumerable<string> dirs, string currentDir, bool isWin)
		{
			TreeItem selectedNode = FindDirInDirTree(treeItem, currentDir, isWin);
			if (selectedNode != null)
			{
				foreach (string dir in dirs)
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

		private TreeItem FindDirInDirTree(TreeItem treeItem, string dirFullPath, bool isWin)
		{
			string[] paths = dirFullPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
			List<string> pathsList = new List<string>(paths);
			TreeItem tmp = null;
			if (!isWin)
			{
				//若为linux主机，直接用第一个节点赋值（根节点"/"）
				tmp = treeItem.Children[0] as TreeItem;
			}
			else
			{
				//若为windows主机，数组[0]为磁盘
				tmp = treeItem.Children.FirstOrDefault(r => r.Text == pathsList[0]) as TreeItem;
				if (tmp == null)
					return null;
				else
					pathsList.RemoveAt(0);
			}
			int index = 0;
			while (index < pathsList.Count)
			{
				string tmpNodeName = pathsList[index];

				var find = tmp.Children.FirstOrDefault(r => r.Text == tmpNodeName) as TreeItem;
				if (find == null)
				{
					return null;
				}
				else
					tmp = find;
				index++;
			}
			return tmp;
		}

		private void ShowFilesAndDirs(TreeViewPlus dirView, GridView fileView, List<OsFile> dirs, List<OsFile> files, bool isWin)
		{
			//show dirs in DirTree
			List<string> newDirs = dirs.Select(dir => dir.FileName.Remove(dir.FileName.Length - 1, 1)).ToList();
			string currentDir = GetCurrentDirPath();
			var treeItem = dirView.DataStore as TreeItem;
			var selectedNode = AddDirInDirTree(treeItem, newDirs, currentDir, isWin);
			if (selectedNode == null)
				return;

			//show dirs,files in fileview
			//fileGridView.DataStore = AddDirsInListViewFile(dirs, files, currentDir);
			//fileGridView.DataStore = AddDirsInListViewFile(dirs, files, currentDir);
			AddDirsInListViewFile(dirs, files, currentDir);

			//expanded
			dirView.Expand(selectedNode);
			dirView.RefreshItem(treeItem);
		}

		private void AddDirsInListViewFile(IEnumerable<OsFile> dirs, IEnumerable<OsFile> files, string parentPath)
		{
			_dataStore.Clear();

			var item = new DataStoreCollection();
			//显示dirs
			foreach (OsFile dir in dirs)
			{
				//移除最后的'/'符号
				string dirName = dir.FileName.Remove(dir.FileName.Length - 1, 1);
				//添加fileInfo
				string fullName = Path.Combine(new string[] { parentPath, dirName }) + (_isWin ? "\\" : "/");
				item.Add(new FileInfo(dirName, fullName, true, dir.FileMTime, dir.FileSize, dir.FilePerms));
			}
			//显示files
			foreach (OsFile file in files)
			{
				//添加fileInfo
				string dirName = file.FileName;
				string fullName = Path.Combine(new string[] { parentPath, dirName });
				item.Add(new FileInfo(dirName, fullName, false, file.FileMTime, file.FileSize, file.FilePerms));
			}
			_dataStore.AddRange(item);
		}
	}
}
