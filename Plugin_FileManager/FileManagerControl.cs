using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Altman.Common.AltData;
using Altman.Common.AltEventArgs;
using Altman.ModelCore;
using Controls;
using PluginFramework;

namespace Plugin_FileManager
{
    public partial class FileManagerControl : UserControl
    {
        private HostService _hostService;
        private ShellStruct _shellData;

        private FileManagerService _fileManager;

        private string _selectDirInTree; //选中的文件夹
        private bool _isWin;             //是否是windows系统
        private string _sourceCopyPath;  //用于文件复制
        private string _sourceCopyName;  //用于文件复制

        public FileManagerControl(HostService hostService, ShellStruct data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView_Dirs.AfterSelect += treeView_File_AfterSelect;
            listView_File.DragEnter += listView_File_DragEnter;
            listView_File.DragDrop += listView_File_DragDrop;
            listView_File.DoubleClick += listView_File_DoubleClick;
            listView_File.AfterEditSubItem += listView_File_AfterEditSubItem;
            listView_File.EditSubItemCompleted += listView_File_EditSubItemCompleted;
            rightMenu_FileManager.Opening += rightMenu_FileManager_Opening;

            this._hostService = hostService;
            this._shellData = data;

            _fileManager = new FileManagerService(_hostService, _shellData);
            _fileManager.GetWwwRootPathCompletedToDo += fileManager_GetWwwRootPathCompletedToDo;
            _fileManager.DeleteFileOrDirCompletedToDo += fileManager_DeleteFileOrDirCompletedToDo;
            _fileManager.GetFileTreeCompletedToDo += fileManager_GetFileTreeCompletedToDo;
            _fileManager.RenameFileOrDirCompletedToDo += fileManager_RenameFileOrDirCompletedToDo;
            _fileManager.CopyFileOrDirCompletedToDo += fileManager_CopyFileOrDirCompletedToDo;
            _fileManager.ModifyFileOrDirTimeCompletedToDo += fileManager_ModifyFileOrDirTimeCompletedToDo;
            _fileManager.CreateDirCompletedToDo += fileManager_CreateDirCompletedToDo;
            _fileManager.WgetCompletedToDo += fileManager_WgetCompletedToDo;

            //获取根路径
            _fileManager.GetWwwRootPath();
        }


        #region 文件操作事件
        /// <summary>
        /// 下载文件到远程服务器完成事件
        /// </summary>
        private void fileManager_WgetCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("下载文件到远程服务器:失败");
                }
                else
                {
                    MessageBox.Show("下载文件到远程服务器:成功");
                }
                _selectDirInTree = GetSelectedDirPathInDirTree();
                _fileManager.GetFileTree(_selectDirInTree);
            }
        }
        /// <summary>
        /// 创建文件夹完成事件
        /// </summary>
        private void fileManager_CreateDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("创建文件夹失败");
                }
                _selectDirInTree = GetSelectedDirPathInDirTree();
                _fileManager.GetFileTree(_selectDirInTree);
            }
        }
        /// <summary>
        /// 修改时间完成事件
        /// </summary>
        private void fileManager_ModifyFileOrDirTimeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("修改时间失败");
                }
                _selectDirInTree = GetSelectedDirPathInDirTree();
                _fileManager.GetFileTree(_selectDirInTree);
            }
        }
        /// <summary>
        /// 复制文件完成事件
        /// </summary>
        private void fileManager_CopyFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("复制文件失败");
                }
                _selectDirInTree = GetSelectedDirPathInDirTree();
                _fileManager.GetFileTree(_selectDirInTree);
            }
        }
        /// <summary>
        /// 文件重命名完成事件
        /// </summary>
        private void fileManager_RenameFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("重命名失败");
                }
                _selectDirInTree = GetSelectedDirPathInDirTree();
                _fileManager.GetFileTree(_selectDirInTree);
            }
        }
        /// <summary>
        /// 获取文件列表完成事件
        /// </summary>
        private void fileManager_GetFileTreeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is List<OsFile>)
            {
                List<OsFile> filetree = e.Result as List<OsFile>;
                ShowFilesAndDirs(filetree);
            }
        }
        /// <summary>
        /// 获取根目录完成事件
        /// </summary>
        private void fileManager_GetWwwRootPathCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is OsDisk)
            {
                OsDisk disk = (OsDisk)e.Result;
                string shellDir = disk.ShellDir;
                if (disk.AvailableDisk == "/")
                {
                    _isWin = false;
                    AddDriveInDirTree(new string[] { "/" });
                }
                else
                {
                    _isWin = true;
                    string[] drives = disk.AvailableDisk.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    AddDriveInDirTree(drives);
                }
                SetPathSeparator(_isWin);
                AddPathInDirTree(shellDir);
            }
        }
        /// <summary>
        /// 删除文件完成事件
        /// </summary>
        private void fileManager_DeleteFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("删除失败");
                }
                _selectDirInTree = GetSelectedDirPathInDirTree();
                _fileManager.GetFileTree(_selectDirInTree);
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadFile(string sourceFilePath, string targetFilePath)
        {
            try
            {
                //判断窗体是否已经被关闭
                //if (trafficManager == null || trafficManager.IsDisposed)
                //{
                //    trafficManager = new FormTrafficManager();
                //}
                //ControlProgressBar progressBar = ShowProgressBar(true, sourceFilePath, targetFilePath);
                //FileUploadOrDownload upload = new FileUploadOrDownload(_shellData, sourceFilePath, targetFilePath);
                //upload.UploadFileProgressChangedToDo += upload_UploadFileProgressChangedToDo;
                //upload.UploadFileCompletedToDo += upload_UploadFileCompletedToDo;
                //upload.StartToUploadFile();
                FileUploadOrDownload upload = new FileUploadOrDownload(_hostService, _shellData, sourceFilePath, targetFilePath);
                upload.UploadFileProgressChangedToDo += upload_UploadFileProgressChangedToDo;
                upload.UploadFileCompletedToDo += upload_UploadFileCompletedToDo;
                upload.StartToUploadFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void upload_UploadFileProgressChangedToDo(object sender, AltProgressChangedEventArgs e)
        {
            //ControlProgressBar progressBar = e.UserState as ControlProgressBar;
            //progressBar.Value = e.ProgressPercentage;
        }
        private void upload_UploadFileCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //ShowResultInProgressBar(false, e);
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    MessageBox.Show("上传失败");
                }
                else
                {
                    MessageBox.Show("上传成功");
                }
                //ShowResultInProgressBar(true, e);
                _selectDirInTree = GetSelectedDirPathInDirTree();
                RefreshAllFiles(_selectDirInTree);
            }
        }

        ///// <summary>
        ///// 显示进度条
        ///// </summary>
        //private ControlProgressBar ShowProgressBar(bool upOrDown, string sourceFilePath, string targetFilePath)
        //{
        //    ControlProgressBar progressBar = trafficManager.AddProgressBar(upOrDown?"UP":"DOWN");
        //    progressBar.TextSourceInfo = sourceFilePath;
        //    progressBar.TextTargetInfo = targetFilePath;
        //    progressBar.TextStatus = upOrDown?"uping":"downing";
        //    trafficManager.Show();
        //    return progressBar;
        //}
        //private void ShowPercentageInProgressBar(Alt_ProgressChangedEventArgs e)
        //{
        //    ControlProgressBar progressBar = e.UserState as ControlProgressBar;
        //    progressBar.Value = e.ProgressPercentage;
        //}
        //private void ShowResultInProgressBar(bool isSuccess,AltCompletedEventArgs e)
        //{
        //    ControlProgressBar progressBar = e.UserState as ControlProgressBar;
        //    progressBar.TextStatus =isSuccess?"successed":"failed";
        //}


        /// <summary>
        /// 下载文件
        /// </summary>
        private void DownloadFile(string sourceFilePath, string targetFilePath)
        {
            try
            {
                ////判断窗体是否已经被关闭
                //if (trafficManager == null || trafficManager.IsDisposed)
                //{
                //    trafficManager = new FormTrafficManager();
                //}
                //ControlProgressBar progressBar = ShowProgressBar(false, sourceFilePath, targetFilePath);
                //FileUploadOrDownload download = new FileUploadOrDownload(_shellData, sourceFilePath, targetFilePath);
                //download.DownloadFileProgressChangedToDo += download_DownloadFileProgressChangedToDo;
                //download.DownloadFileCompletedToDo+=download_DownloadFileCompletedToDo;
                //download.StartToDownloadFile();
                FileUploadOrDownload download = new FileUploadOrDownload(_hostService, _shellData, sourceFilePath, targetFilePath);
                download.DownloadFileProgressChangedToDo += download_DownloadFileProgressChangedToDo;
                download.DownloadFileCompletedToDo += download_DownloadFileCompletedToDo;
                download.StartToDownloadFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void download_DownloadFileProgressChangedToDo(object sender, AltProgressChangedEventArgs e)
        {
            //ShowPercentageInProgressBar(e);
        }
        private void download_DownloadFileCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //ShowResultInProgressBar(false, e);
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                //ShowResultInProgressBar(true,e);
                MessageBox.Show("下载成功");
            }
        }

        /// <summary>
        /// 刷新文件列表
        /// </summary>
        public void RefreshAllFiles(string selectedPath)
        {
            textBox_url.Text = selectedPath;
            _fileManager.GetFileTree(selectedPath);
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 设置TreeView的分隔符
        /// </summary>
        /// <param name="isWindows"></param>
        private void SetPathSeparator(bool isWindows)
        {
            if (isWindows)
            {
                treeView_Dirs.PathSeparator = @"\";
            }
            else
            {
                treeView_Dirs.PathSeparator = "/";
            }

        }
        /// <summary>
        /// 添加磁盘节点
        /// </summary>
        /// <param name="drives"></param>
        private void AddDriveInDirTree(string[] drives)
        {
            for (int i = 0; i < drives.Length; i++)
            {
                TreeNode node = new TreeNode(drives[i], 0, 0)
                {
                    Name = drives[i]
                };
                treeView_Dirs.Nodes.Add(node);
            }
        }
        /// <summary>
        /// 添加路径的节点
        /// </summary>
        /// <param name="webPath"></param>
        private void AddPathInDirTree(string webPath)
        {
            string[] strArray;
            if (_isWin)
            {
                //windows
                strArray = webPath.Split(new string[] { @"\", "/" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                //linux
                strArray = webPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            }
            TreeNode topNode = treeView_Dirs.TopNode;
            TreeNodeCollection nodes = treeView_Dirs.Nodes;
            for (int i = 0; i < strArray.Length; i++)
            {
                string key = strArray[i];
                if (nodes.ContainsKey(key))
                {
                    topNode = nodes[key];
                }
                else
                {
                    TreeNode node = new TreeNode(key, 1, 1)
                    {
                        Name = key
                    };
                    if (topNode == null)
                    {
                        treeView_Dirs.Nodes.Add(node);
                        topNode = treeView_Dirs.Nodes[key];
                    }
                    else
                    {
                        topNode.Nodes.Add(node);
                        topNode = topNode.Nodes[key];
                    }
                }
                nodes = topNode.Nodes;
            }
            treeView_Dirs.SelectedNode = topNode;
            treeView_Dirs.SelectedNode.Expand();
        }
        /// <summary>
        /// 显示文件和文件夹
        /// </summary>
        /// <param name="fileList"></param>
        private void ShowFilesAndDirs(List<OsFile> fileList)
        {
            listView_File.Items.Clear();
            List<OsFile> files = new List<OsFile>();
            List<OsFile> dirs = new List<OsFile>();
            foreach (OsFile file in fileList)
            {
                if (file.FileName.EndsWith("/"))
                {
                    dirs.Add(file);
                }
                else
                {
                    files.Add(file);
                }
            }
            //显示dirs
            foreach (OsFile file in dirs)
            {
                //移除最后的'/'符号
                string dirName = file.FileName.Remove(file.FileName.Length - 1, 1);
                //添加文件夹树节点
                if (!treeView_Dirs.SelectedNode.Nodes.ContainsKey(dirName))
                {
                    TreeNode node = new TreeNode(dirName, 1, 1)
                    {
                        Name = dirName
                    };
                    treeView_Dirs.SelectedNode.Nodes.Add(node);
                }
                //添加目录列表
                ListViewItem item = new ListViewItem(dirName, 1);
                //为item添加name,为查找确定key
                item.Name = dirName;
                item.SubItems.Add(file.FileMTime);
                item.SubItems.Add(file.FileSize);
                item.SubItems.Add(file.FilePerms);
                item.Tag = SelectedItemsStatus.IsDir;//is dir
                listView_File.Items.Add(item);
            }
            //显示files
            foreach (OsFile file in files)
            {
                //添加文件列表
                ListViewItem item = new ListViewItem(file.FileName, 2);
                //为item添加name,为查找确定key
                item.Name = file.FileName;
                item.SubItems.Add(file.FileMTime);
                item.SubItems.Add(file.FileSize);
                item.SubItems.Add(file.FilePerms);
                item.Tag = SelectedItemsStatus.IsFile;//is file
                listView_File.Items.Add(item);
            }
            //展开选中节点
            treeView_Dirs.SelectedNode.Expand();
        }
        /// <summary>
        /// 获取目录树中选择的文件夹
        /// </summary>
        /// <returns></returns>
        private string GetSelectedDirPathInDirTree()
        {
            TreeNode selectedNode = treeView_Dirs.SelectedNode;
            string selectedPath = selectedNode.FullPath;
            return _isWin ? selectedPath + @"\" : (selectedPath + "/").Substring(1);
        }
        /// <summary>
        /// 获取文件列表中选择的文件或文件夹
        /// </summary>
        /// <returns></returns>
        private string GetSelectedPathInListView(bool isDir)
        {
            string str = GetSelectedDirPathInDirTree();
            if (str != "")
            {
                if (isDir)
                {
                    if (_isWin)
                    {
                        //str += listView_File.SelectedItems[0].SubItems[0].Text + @"\";
                        str += listView_File.SelectedItems[0].SubItems[0].Text;
                    }
                    else
                    {
                        str += listView_File.SelectedItems[0].SubItems[0].Text + "/";
                    }
                }
                else
                {
                    str += listView_File.SelectedItems[0].SubItems[0].Text;
                }
            }
            return str;
        }

        private enum SelectedItemsStatus
        {
            NoSelected = -1,
            IsDir = 0,
            IsFile = 1
        }
        /// <summary>
        /// 获取listview中选中项状态
        /// </summary>
        /// <returns></returns>
        private SelectedItemsStatus GetStatusOfSelectedItemsInListView()
        {
            if (listView_File.SelectedItems.Count > 0)
            {
                SelectedItemsStatus tmp = (SelectedItemsStatus)listView_File.SelectedItems[0].Tag;
                return tmp;
            }
            else
            {
                return SelectedItemsStatus.NoSelected;
            }
        }
        private SelectedItemsStatus GetFileTypeInListView(ListViewItem item)
        {
            SelectedItemsStatus type = (SelectedItemsStatus)item.Tag;
            return type;
        }

        #endregion

        #region treeView_File事件
        /// <summary>
        /// 树节点选择事件
        /// </summary>
        private void treeView_File_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string webDir = GetSelectedDirPathInDirTree();
            try
            {
                RefreshAllFiles(webDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 外部文件拖动事件
        /// </summary>
        private void listView_File_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// 外部文件拖动事件(上传)
        /// </summary>
        private void listView_File_DragDrop(object sender, DragEventArgs e)
        {
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            string selectDirPath = GetSelectedDirPathInDirTree();
            foreach (string srcfile in files)
            {
                if (Directory.Exists(srcfile))
                {
                    MessageBox.Show("请不要拖放文件夹");
                }
                else if (File.Exists(srcfile))
                {
                    string fileName = Path.GetFileName(srcfile);
                    string targetFilePath = selectDirPath + fileName;
                    UploadFile(srcfile, targetFilePath);
                }
            }
        }
        /// <summary>
        /// 文件列表双击事件
        /// </summary>
        private void listView_File_DoubleClick(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status == SelectedItemsStatus.IsDir)
            {
                string webDir = GetSelectedPathInListView(true);
                if (webDir != "")
                {
                    textBox_url.Text = webDir;
                    try
                    {
                        RefreshAllFiles(webDir);
                        AddPathInDirTree(webDir);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 文件列表编辑后事件
        /// </summary>
        private void listView_File_AfterEditSubItem(object sender, ListViewPlus.EditSubItemEventArgs e)
        {
            //是否为空
            if (string.IsNullOrEmpty(e.Label))
            {
                e.IsCancelEdit = true;
                return;
            }
            //检查是否发生变化
            if (e.OldLabel == e.Label)
            {
                e.IsCancelEdit = true;
                return;
            }

            switch (e.UserSate)
            {
                case "createDir":
                case "rename":
                    //查找新文件名/文件夹名是否已经存在
                    if (((ListView)sender).Items.ContainsKey(e.Label))
                    {
                        e.IsCancelEdit = true;
                        MessageBox.Show("已经存在此名称，请重新命名");
                        return;
                    }
                    //MessageBox.Show("重命名发生变化");
                    break;
                case "modifyTime":
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 文件列表编辑完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_File_EditSubItemCompleted(object sender, ListViewPlus.EditSubItemEventArgs e)
        {
            switch (e.UserSate)
            {
                case "rename":
                    ((ListView)sender).Items[e.ItemRowIndex].Name = e.Label;
                    //发送重命名请求
                    //oldFileName,newFileName
                    //为了安全起见，这里使用绝对路径
                    _selectDirInTree = GetSelectedDirPathInDirTree();
                    string oldFileName = e.OldLabel;
                    string newFileName = e.Label;
                    string oldFileNameFullPath = Path.Combine(_selectDirInTree, oldFileName);
                    string newFileNameFullPath = Path.Combine(_selectDirInTree, newFileName);
                    _fileManager.RenameFileOrDir(oldFileNameFullPath, newFileNameFullPath);
                    break;
                case "modifyTime":
                    SelectedItemsStatus status = (SelectedItemsStatus)(((ListView)sender).Items[e.ItemRowIndex]).Tag;
                    if (status == SelectedItemsStatus.IsDir)
                    {
                        string file = GetSelectedPathInListView(true);
                        //修改时间
                        _fileManager.ModifyFileOrDirTime(file, e.Label);
                    }
                    else if (status == SelectedItemsStatus.IsFile)
                    {
                        string file = GetSelectedPathInListView(false);
                        //修改时间
                        _fileManager.ModifyFileOrDirTime(file, e.Label);
                    }
                    break;
                case "createDir":
                    ////打开设置界面
                    _selectDirInTree = GetSelectedDirPathInDirTree();
                    string dirName = e.Label;
                    string dirFullPath = Path.Combine(_selectDirInTree, dirName);
                    _fileManager.CreateDir(dirFullPath);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 右键菜单事件
        /// <summary>
        /// 右键菜单打开事件
        /// </summary>
        private void rightMenu_FileManager_Opening(object sender, CancelEventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status == SelectedItemsStatus.NoSelected)
            {
                rightMenu_FileManager.Items["item_refresh"].Visible = true;
                rightMenu_FileManager.Items["item_upload"].Visible = true;
                rightMenu_FileManager.Items["item_download"].Visible = false;
                rightMenu_FileManager.Items["item_downloadToServer"].Visible = true;
                rightMenu_FileManager.Items["item_del"].Visible = false;
                rightMenu_FileManager.Items["item_edit"].Visible = false;
                rightMenu_FileManager.Items["item_copy"].Visible = false;
                rightMenu_FileManager.Items["item_paste"].Visible = true;
                rightMenu_FileManager.Items["item_paste"].Enabled = string.IsNullOrEmpty(_sourceCopyPath) ? false : true;
                rightMenu_FileManager.Items["item_rename"].Visible = false;
                rightMenu_FileManager.Items["item_modifyTime"].Visible = false;
                rightMenu_FileManager.Items["item_add"].Visible = true;
            }
            else if (status == SelectedItemsStatus.IsDir)
            {
                rightMenu_FileManager.Items["item_refresh"].Visible = true;
                rightMenu_FileManager.Items["item_upload"].Visible = true;
                rightMenu_FileManager.Items["item_download"].Visible = false;
                rightMenu_FileManager.Items["item_downloadToServer"].Visible = true;
                rightMenu_FileManager.Items["item_del"].Visible = true;
                rightMenu_FileManager.Items["item_edit"].Visible = false;
                rightMenu_FileManager.Items["item_copy"].Visible = true;
                rightMenu_FileManager.Items["item_paste"].Visible = false;
                rightMenu_FileManager.Items["item_rename"].Visible = true;
                rightMenu_FileManager.Items["item_modifyTime"].Visible = true;
                rightMenu_FileManager.Items["item_add"].Visible = true;
            }
            else if (status == SelectedItemsStatus.IsFile)
            {
                rightMenu_FileManager.Items["item_refresh"].Visible = true;
                rightMenu_FileManager.Items["item_upload"].Visible = true;
                rightMenu_FileManager.Items["item_download"].Visible = true;
                rightMenu_FileManager.Items["item_downloadToServer"].Visible = true;
                rightMenu_FileManager.Items["item_del"].Visible = true;
                rightMenu_FileManager.Items["item_edit"].Visible = true;
                rightMenu_FileManager.Items["item_copy"].Visible = true;
                rightMenu_FileManager.Items["item_paste"].Visible = false;
                rightMenu_FileManager.Items["item_rename"].Visible = true;
                rightMenu_FileManager.Items["item_modifyTime"].Visible = true;
                rightMenu_FileManager.Items["item_add"].Visible = true;
            }
        }
        /// <summary>
        /// 右键菜单刷新事件
        /// </summary>
        private void item_refresh_Click(object sender, EventArgs e)
        {
            string selectDirPath = GetSelectedDirPathInDirTree();
            RefreshAllFiles(selectDirPath);
        }
        /// <summary>
        /// 右键菜单上传事件
        /// </summary>
        private void item_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择一个文件上传到服务器",
                Filter = "脚本文件|*.asp;*.aspx;*.php;*.jsp|执行程序|*.exe;*.bat|所有文件|*.*",
                FilterIndex = 1,
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string srcfile = openFileDialog.FileName;
                string selectDirPath = GetSelectedDirPathInDirTree();
                string fileName = Path.GetFileName(srcfile);
                string targetFilePath = selectDirPath + fileName;
                UploadFile(srcfile, targetFilePath);
            }
        }
        /// <summary>
        /// 右键菜单下载事件
        /// </summary>
        private void item_download_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status == SelectedItemsStatus.IsFile)
            {
                string webFile = GetSelectedPathInListView(false);
                string name = Path.GetFileName(webFile);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "将文件另存为";
                saveFileDialog.FileName = name;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DownloadFile(webFile, saveFileDialog.FileName);
                }
            }
        }
        /// <summary>
        /// 右键菜单删除事件
        /// </summary>
        private void item_del_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否继续删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
                if (status == SelectedItemsStatus.IsDir)
                {
                    string webDir = GetSelectedPathInListView(true);
                    _fileManager.DeleteFileOrDir(webDir);
                }
                else if (status == SelectedItemsStatus.IsFile)
                {
                    string webFile = GetSelectedPathInListView(false);
                    _fileManager.DeleteFileOrDir(webFile);
                }
            }
        }
        /// <summary>
        /// 右键菜单编辑事件
        /// </summary>
        private void item_edit_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status == SelectedItemsStatus.IsFile)
            {
                string webFile = GetSelectedPathInListView(false);
                UserControl fileEditer = new FileEditerControl(_hostService, _shellData, webFile, true);
                _hostService.CreateNewTabPage("FileEdit", fileEditer);
            }
        }
        /// <summary>
        /// 右键菜单重命名事件
        /// </summary>
        private void item_rename_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status != SelectedItemsStatus.NoSelected)
            {
                listView_File.EditSubItem(listView_File.SelectedItems[0], 0, "rename", false);
            }
        }
        /// <summary>
        /// 右键菜单修改时间事件
        /// </summary>
        private void item_modifyTime_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status != SelectedItemsStatus.NoSelected)
            {
                listView_File.EditSubItem(listView_File.SelectedItems[0], 1, "modifyTime", false);
            }
        }
        /// <summary>
        /// 右键菜单复制事件
        /// </summary>
        private void item_copy_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status == SelectedItemsStatus.IsDir)
            {
                _sourceCopyPath = GetSelectedPathInListView(true);
            }
            else if (status == SelectedItemsStatus.IsFile)
            {
                _sourceCopyPath = GetSelectedPathInListView(false);
            }
            _sourceCopyName = listView_File.SelectedItems[0].Text;
        }
        /// <summary>
        /// 右键菜单粘贴事件
        /// </summary>
        private void item_paste_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            if (status == SelectedItemsStatus.NoSelected)
            {
                if (_sourceCopyPath != string.Empty)
                {
                    string targetCopyDir = GetSelectedDirPathInDirTree();
                    _fileManager.CopyFileOrDir(_sourceCopyPath, targetCopyDir + _sourceCopyName);
                    //清空复制记录
                    _sourceCopyPath = string.Empty;
                    _sourceCopyName = string.Empty;
                }
            }
        }
        /// <summary>
        /// 右键菜单创建文件夹事件
        /// </summary>
        private void item_createDir_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] subItems = new string[] { "NewFolder", time, "0", "0777" };
            ListViewItem item = new ListViewItem(subItems, 1);
            listView_File.Items.Add(item);
            listView_File.EditSubItem(item, 0, "createDir", true);
        }
        /// <summary>
        /// 右键菜单创建文件事件
        /// </summary>
        private void item_createFile_Click(object sender, EventArgs e)
        {
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            string newFile = GetSelectedDirPathInDirTree() + "NewFile.txt";
            UserControl fileEditer = new FileEditerControl(_hostService, _shellData, newFile, false);
            _hostService.CreateNewTabPage("FileEdit", fileEditer);
        }
        /// <summary>
        /// 右键菜单下载文件到远程服务器
        /// </summary>
        private void item_downloadToServer_Click(object sender, EventArgs e)
        {
            //FormWget
            SelectedItemsStatus status = GetStatusOfSelectedItemsInListView();
            //打开设置界面
            _selectDirInTree = GetSelectedDirPathInDirTree();
            FormWget formWget = new FormWget(_selectDirInTree);
            formWget.ShowDialog();
            if (formWget.DialogResult == DialogResult.Yes)
            {
                string urlPath = formWget.UrlPath;
                string savePath = formWget.SavePath;
                formWget.Close();

                _fileManager.Wget(urlPath, savePath);
            }
        }

        #endregion
    }
}
