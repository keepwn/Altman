using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Altman.Common.AltData;
using Altman.Common.AltEventArgs;
using Altman.Model;
using Altman.Controls;
using PluginFramework;

namespace Plugin_FileManager
{
    public partial class FileManagerControl : UserControl
    {
        private IHost _host;
        private Shell _shellData;

        private FileManagerService _fileManager;

        private bool _isWin;             //是否是windows系统
        private string _sourceCopyPath;  //用于文件复制
        private string _sourceCopyName;  //用于文件复制

        public FileManagerControl(IHost host, Shell data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView_Dirs.MouseDoubleClick += treeView_Dirs_MouseDoubleClick;
            listView_File.DragEnter += listView_File_DragEnter;
            listView_File.DragDrop += listView_File_DragDrop;
            listView_File.DoubleClick += listView_File_DoubleClick;
            listView_File.AfterEditSubItem += listView_File_AfterEditSubItem;
            listView_File.EditSubItemCompleted += listView_File_EditSubItemCompleted;
            rightMenu_FileManager.Opening += rightMenu_FileManager_Opening;

            this._host = host;
            this._shellData = data;

            _fileManager = new FileManagerService(_host, _shellData);
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
                MessageBox.Show(msg);

                _fileManager.GetFileTree(GetCurrentDirPath());
            }
        }
        /// <summary>
        /// 创建文件夹完成事件
        /// </summary>
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
                    string msg = "Failed to create the folder";
                    _host.Ui.ShowMsgInStatusBar(msg);
                    MessageBox.Show(msg);
                }
                _fileManager.GetFileTree(GetCurrentDirPath());
            }
        }
        /// <summary>
        /// 修改时间完成事件
        /// </summary>
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
                    string msg = "Failed to modify file's time";
                    _host.Ui.ShowMsgInStatusBar(msg);
                    MessageBox.Show(msg);
                }
                _fileManager.GetFileTree(GetCurrentDirPath());
            }
        }
        /// <summary>
        /// 复制文件完成事件
        /// </summary>
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
                    string msg = "Failed to copy file";
                    _host.Ui.ShowMsgInStatusBar(msg);
                    MessageBox.Show(msg);
                }
                _fileManager.GetFileTree(GetCurrentDirPath());
            }
        }
        /// <summary>
        /// 文件重命名完成事件
        /// </summary>
        private void fileManager_RenameFileOrDirCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                if (!(bool)e.Result)
                {
                    string msg = "Failed to rename";
                    _host.Ui.ShowMsgInStatusBar(msg);
                    MessageBox.Show(msg);
                }
                _fileManager.GetFileTree(GetCurrentDirPath());
            }
        }
        /// <summary>
        /// 获取文件列表完成事件
        /// </summary>
        private void fileManager_GetFileTreeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is List<OsFile>)
            {
                List<OsFile> filetree = e.Result as List<OsFile>;
                var files = new List<OsFile>();
                var dirs = new List<OsFile>();
                foreach (OsFile file in filetree)
                {
                    if (file.FileName.EndsWith("/"))
                        dirs.Add(file);
                    else
                        files.Add(file);
                }
                ShowFilesAndDirs(treeView_Dirs, listView_File, dirs, files,_isWin);

                _host.Ui.ShowMsgInStatusBar(string.Format("Dirs[{0}] Files[{1}]", dirs.Count, files.Count));
            }
        }
        /// <summary>
        /// 获取根目录完成事件
        /// </summary>
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
                SetPathSeparator(treeView_Dirs, _isWin);

                SetCurrentDirPath(shellDir);
                ShowWwwRootDir(treeView_Dirs, drives, shellDir, _isWin);
                _host.Ui.ShowMsgInStatusBar("Connect succeed");
            }
        }
        /// <summary>
        /// 删除文件完成事件
        /// </summary>
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
                    string msg = "Failed to delete";
                    _host.Ui.ShowMsgInStatusBar(msg);
                    MessageBox.Show(msg);
                }
                _fileManager.GetFileTree(GetCurrentDirPath());
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
                FileUploadOrDownload upload = new FileUploadOrDownload(_host, _shellData, sourceFilePath, targetFilePath);
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
                _host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is bool)
            {
                string msg = string.Empty;
                if (!(bool)e.Result)
                {
                    msg = "Failed to upload file";
                }
                else
                {
                    msg = "Upload file succeed";
                }
                _host.Ui.ShowMsgInStatusBar(msg);
                MessageBox.Show(msg);

                //ShowResultInProgressBar(true, e);
                RefreshAllFiles(GetCurrentDirPath());
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
                FileUploadOrDownload download = new FileUploadOrDownload(_host, _shellData, sourceFilePath, targetFilePath);
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
                string msg = "Download file succeed";
                _host.Ui.ShowMsgInStatusBar(msg);
            }
        }

        /// <summary>
        /// 刷新文件
        /// </summary>
        private void RefreshAllFiles(string selectedPath)
        {
            SetCurrentDirPath(selectedPath);
            _fileManager.GetFileTree(GetCurrentDirPath());
        }

        #endregion

        #region 私有方法
        private enum SelectedFilesStatus
        {
            NoSelected = 0,
            IsDir = 1,
            IsFile = 2,
            IsMuti = 3
        }

        private enum FileType
        {
            unknow=0,
            //可执行文件
            exe=10,
            dll,
            bat,
            bin,
            //压缩文件
            zip=20,
            rar,
            gz,
            tgz,
            cab,
            //图像文件
            bmp=30,
            gif,
            jpeg,
            jpg,      
            png,
            //媒体文件
            rm=40,
            rmvb,
            mov,
            mp4,
            mpeg,
            mpg,
            flv,
            swf,         
            //文档文件
            dat=50,
            doc,
            docx,
            eml,
            ini,
            log,
            mdb,
            pdf,
            ppt,
            pptx,
            psd,
            rtf,
            txt,
            wps,
            xls,
            xlsx,
            //语言文件        
            asa=80,
            asp,
            aspx,
            c,
            cpp,
            cs,
            h,
            htm,
            html,
            jar,
            php,
            ruby,
            sln,
            //其他
            iso=100,
            chm,
        }
        private struct FileInfo
        {
            public string Name;
            public string FullName;
            public bool IsDir;
            public FileType Type;
            
            public FileInfo(string name,string fullname, bool isDir,string fileType)
            {
                Name = name;
                FullName = fullname;
                IsDir = isDir;

                if (!Enum.TryParse<FileType>(fileType, out Type))
                {
                    Type = FileType.unknow;
                }
            }
        }
        /// <summary>
        /// 设置TreeView的分隔符
        /// </summary>
        private void SetPathSeparator(TreeView treeView, bool isWin)
        {
            treeView.PathSeparator = isWin ? "\\" : "/";
        }
        private TreeNode FindDirInDirTree(TreeView treeView, string dirFullPath, bool isWin)
        {
            string[] paths = dirFullPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> pathsList = new List<string>(paths);
            TreeNode tmp = null;
            if (!isWin)
            {
                //若为linux主机，直接用第一个节点赋值（根节点"/"）
                tmp = treeView.Nodes[0];
            }
            else
            {
                //若为windows主机，数组[0]为磁盘
                if (!treeView.Nodes.ContainsKey(pathsList[0]))
                {
                    return null;
                }
                tmp = treeView.Nodes[pathsList[0]];
                pathsList.RemoveAt(0);
            }
            int index = 0;
            while (index < pathsList.Count)
            {
                string tmpNodeName = pathsList[index];
                if (!tmp.Nodes.ContainsKey(tmpNodeName))
                {
                    return null;
                }
                tmp = tmp.Nodes[tmpNodeName];
                index++;
            }
            return tmp;
        }
        private void AddDriveInDirTree(TreeView treeView, IEnumerable<string> drives)
        {
            //refresh
            treeView.Nodes.Clear();
            //load
            foreach (string drive in drives)
            {
                treeView.Nodes.Add(drive, drive, 0, 0);
            }
        }
        private TreeNode AddDirInDirTree(TreeView treeView, IEnumerable<string> dirs, string currentDir,bool isWin)
        {
            TreeNode selectedNode = FindDirInDirTree(treeView, currentDir, isWin);
            if (selectedNode != null)
            {
                foreach (string dir in dirs)
                {
                    if (!selectedNode.Nodes.ContainsKey(dir))
                        selectedNode.Nodes.Add(dir, dir, 1, 1);
                }
            }
            return selectedNode;
        }
        private void AddDirInDirTree(TreeView treeView, string dirFullPath, bool isWin)
        {
            string[] paths = dirFullPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> pathsList = new List<string>(paths);
            TreeNode tmp = null;
            if (!isWin)
            {
                //若为linux主机，直接用第一个节点赋值（根节点"/"）
                tmp = treeView.Nodes[0];
            }
            else
            {
                //若为windows主机，数组[0]为磁盘
                if (treeView.Nodes.ContainsKey(pathsList[0]))
                {
                    tmp = treeView.Nodes[pathsList[0]];
                    pathsList.RemoveAt(0);
                }
                else return;
            }
            int index = 0;
            while (index < pathsList.Count)
            {
                string tmpNodeName = pathsList[index];
                tmp = !tmp.Nodes.ContainsKey(tmpNodeName) ? tmp.Nodes.Add(tmpNodeName, tmpNodeName, 1, 1) : tmp.Nodes[tmpNodeName];
                index++;
            }
        }
        private void RemoveDirInDirTree(TreeView treeView, string dirFullPath, bool isWin)
        {
            TreeNode selectedNode = FindDirInDirTree(treeView, dirFullPath, isWin);
            if (selectedNode != null)
            {
                selectedNode.Remove();
            }
        }

        private void AddDirsInListViewFile(ListViewPlus listViewFile, IEnumerable<OsFile> dirs, string parentPath)
        {
            //显示dirs
            foreach (OsFile dir in dirs)
            {
                //移除最后的'/'符号
                string dirName = dir.FileName.Remove(dir.FileName.Length - 1, 1);
                //添加目录列表
                ListViewItem item = new ListViewItem(dirName, 1);
                //为item添加name,为查找确定key
                item.Name = dirName;
                item.SubItems.Add(dir.FileMTime);
                item.SubItems.Add(dir.FileSize);
                item.SubItems.Add(dir.FilePerms);
                //添加fileInfo
                string fullName = Path.Combine(new string[] { parentPath, dirName }) + (_isWin ? "\\" : "/");
                item.Tag = new FileInfo(dirName, fullName, true, FileType.unknow.ToString());//is dir
                listViewFile.Items.Add(item);
            }
        }
        private void AddFilesInListViewFile(ListViewPlus listViewFile, IEnumerable<OsFile> files, string parentPath)
        {
            //显示files
            foreach (OsFile file in files)
            {          
                //fileInfo
                string dirName = file.FileName;
                string fullName = Path.Combine(new string[] { parentPath, dirName });
                string type = Path.GetExtension(dirName).ToLower();
                if (type.StartsWith("."))
                    type = type.TrimStart('.');
                var fileInfo = new FileInfo(dirName, fullName, false, type);//is file
                //添加文件列表
                ListViewItem item = new ListViewItem(file.FileName);
                item.Name = dirName;//为item添加name,为查找确定key               
                item.SubItems.Add(file.FileMTime);
                item.SubItems.Add(file.FileSize);
                item.SubItems.Add(file.FilePerms);
                item.ImageKey = GetFileTypeInListView(fileInfo.Type);
                item.Tag = fileInfo;
           
                listViewFile.Items.Add(item);
            }
        }
        private void ShowFilesAndDirs(TreeView treeView, ListViewPlus listViewFile, List<OsFile> dirs, List<OsFile> files, bool isWin)
        {
            //Refreash
            listViewFile.Items.Clear();

            //show dirs in DirTree
            List<string> newDirs = dirs.Select(dir => dir.FileName.Remove(dir.FileName.Length - 1, 1)).ToList();
            string currentDir = GetCurrentDirPath();
            TreeNode selectedNode = AddDirInDirTree(treeView_Dirs, newDirs,currentDir,_isWin);
            if (selectedNode == null) 
                return;

            //show dirs/files in ListViewFile
            AddDirsInListViewFile(listViewFile, dirs, currentDir);
            AddFilesInListViewFile(listViewFile, files, currentDir);
         
            treeView.CollapseAll();
            treeView.SelectedNode = selectedNode;
            treeView.SelectedNode.Expand();
        }
        private void ShowWwwRootDir(TreeView treeView, IEnumerable<string> drives, string wwwRootDir, bool isWin)
        {
            AddDriveInDirTree(treeView, drives);
            AddDirInDirTree(treeView, wwwRootDir, isWin);
            RefreshAllFiles(GetCurrentDirPath());
        }

        /// <summary>
        /// 获取当前目录路径
        /// </summary>
        private string GetCurrentDirPath()
        {
            string sep = _isWin ? "\\" : "/";
            if (textBox_url.Text.EndsWith(sep))
            {
                return textBox_url.Text;
            }
            else
            {
                return textBox_url.Text + sep;
            }
        }
        private void SetCurrentDirPath(string currentDir)
        {
            string sep = _isWin ? "\\" : "/";
            string dir = currentDir;
            if (dir.EndsWith(sep))
            {
                textBox_url.Text = dir;
            }
            else
            {
                textBox_url.Text = dir + sep;
            }
        }
        /// <summary>
        /// 获取文件列表中被选择的文件信息
        /// </summary>
        private List<FileInfo> GetSelectedFilesInListView()
        {
            List<FileInfo> files = new List<FileInfo>();
            if (listView_File.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView_File.SelectedItems)
                {
                    files.Add((FileInfo)item.Tag);
                }
            }
            return files;
        }
        /// <summary>
        /// 获取listview中选中项状态
        /// </summary>
        private SelectedFilesStatus GetStatusOfSelectedFilesInListView(List<FileInfo> fileInfos)
        {
            if (fileInfos.Count == 0)
            {
                return SelectedFilesStatus.NoSelected;
            }
            else if (fileInfos.Count == 1)
            {
                return fileInfos[0].IsDir ? SelectedFilesStatus.IsDir : SelectedFilesStatus.IsFile;
            }
            else
            {
                return SelectedFilesStatus.IsMuti;
            }
        }
        private string GetFileTypeInListView(FileType type)
        {
            string tmpName = string.Format("{0}_{1}_32.ico", (int) type, type.ToString());
            if (imageList_FileType.Images.ContainsKey(tmpName))
            {
                return tmpName;
            }
            return imageList_FileType.Images[0].ToString();
        }

        #endregion

        #region btn_Dir事件
        private void btn_Dir_Click(object sender, EventArgs e)
        {
            RefreshAllFiles(GetCurrentDirPath());
        }
        #endregion

        #region treeView_File事件
        /// <summary>
        /// 树节点选择事件
        /// </summary>
        private void treeView_Dirs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeView treeView = (sender as TreeView);
            if (treeView!=null)
            {
                string webDir = treeView.SelectedNode.FullPath;
                RefreshAllFiles(webDir);
            }
        }
        #endregion

        #region listView_File事件
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
            string currentDir = GetCurrentDirPath();
            foreach (string srcfile in files)
            {
                if (Directory.Exists(srcfile))
                {
                    MessageBox.Show("Please don't dragDrop folders");
                }
                else if (File.Exists(srcfile))
                {
                    string fileName = Path.GetFileName(srcfile);
                    string targetFilePath =Path.Combine(currentDir, fileName);
                    UploadFile(srcfile, targetFilePath);
                }
            }
        }
        /// <summary>
        /// 文件列表双击事件
        /// </summary>
        private void listView_File_DoubleClick(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status == SelectedFilesStatus.IsDir)
            {
                string webDir = fileInfos[0].FullName;
                RefreshAllFiles(webDir);
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
                        MessageBox.Show("This name already exists, please rename");
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
        private void listView_File_EditSubItemCompleted(object sender, ListViewPlus.EditSubItemEventArgs e)
        {
            string currentDir = string.Empty;
            switch (e.UserSate)
            {
                case "rename":
                    ((ListView)sender).Items[e.ItemRowIndex].Name = e.Label;
                    //发送重命名请求
                    //oldFileName,newFileName
                    //为了安全起见，这里使用绝对路径
                    currentDir = GetCurrentDirPath();
                    string oldFileName = e.OldLabel;
                    string newFileName = e.Label;
                    string oldFileNameFullPath = Path.Combine(currentDir, oldFileName);
                    string newFileNameFullPath = Path.Combine(currentDir, newFileName);
                    _fileManager.RenameFileOrDir(oldFileNameFullPath, newFileNameFullPath);
                    break;
                case "modifyTime":
                    FileInfo fileInfo = (FileInfo)(((ListView)sender).Items[e.ItemRowIndex]).Tag;
                    //修改时间
                    _fileManager.ModifyFileOrDirTime(fileInfo.FullName, e.Label);
                    break;
                case "createDir":
                    ////打开设置界面
                    currentDir = GetCurrentDirPath();
                    string dirName = e.Label;
                    string dirFullPath = Path.Combine(currentDir, dirName);
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
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status == SelectedFilesStatus.NoSelected)
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
            else if (status == SelectedFilesStatus.IsDir)
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
            else if (status == SelectedFilesStatus.IsFile)
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
            string currentDirPath = GetCurrentDirPath();
            RefreshAllFiles(currentDirPath);
        }
        /// <summary>
        /// 右键菜单上传事件
        /// </summary>
        private void item_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select File To Upload",
                Filter = "Script File|*.asp;*.aspx;*.php;*.jsp|Program|*.exe;*.bat|All Files|*.*",
                FilterIndex = 1,
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string srcfile = openFileDialog.FileName;
                string currentDirPath = GetCurrentDirPath();
                string fileName = Path.GetFileName(srcfile);
                string targetFilePath =Path.Combine(currentDirPath, fileName);
                UploadFile(srcfile, targetFilePath);
            }
        }
        /// <summary>
        /// 右键菜单下载事件
        /// </summary>
        private void item_download_Click(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status == SelectedFilesStatus.IsFile)
            {
                string webFile = fileInfos[0].FullName;
                string name = Path.GetFileName(webFile);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save File As";
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
            DialogResult result = MessageBox.Show("Continue To Delete？", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                List<FileInfo> fileInfos = GetSelectedFilesInListView();
                SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
                if (status == SelectedFilesStatus.IsDir || status == SelectedFilesStatus.IsFile)
                {
                    string webDir = fileInfos[0].FullName;
                    //提前删除文件夹节点，如果删除失败可能会影响(待解决)
                    RemoveDirInDirTree(treeView_Dirs, webDir, _isWin);
                    _fileManager.DeleteFileOrDir(webDir);
                }
            }
        }
        /// <summary>
        /// 右键菜单编辑事件
        /// </summary>
        private void item_edit_Click(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status == SelectedFilesStatus.IsFile)
            {
                string webFile = fileInfos[0].FullName;
                UserControl fileEditer = new FileEditerControl(_host, _shellData, webFile, true);
                _host.Ui.CreateNewTabPage("FileEdit", fileEditer);
            }
        }
        /// <summary>
        /// 右键菜单重命名事件
        /// </summary>
        private void item_rename_Click(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status != SelectedFilesStatus.NoSelected)
            {
                listView_File.EditSubItem(listView_File.SelectedItems[0], 0, "rename", false);
            }
        }
        /// <summary>
        /// 右键菜单修改时间事件
        /// </summary>
        private void item_modifyTime_Click(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status != SelectedFilesStatus.NoSelected)
            {
                listView_File.EditSubItem(listView_File.SelectedItems[0], 1, "modifyTime", false);
            }
        }
        /// <summary>
        /// 右键菜单复制事件
        /// </summary>
        private void item_copy_Click(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status == SelectedFilesStatus.IsDir || status == SelectedFilesStatus.IsFile)
            {
                _sourceCopyPath = fileInfos[0].FullName;
                _sourceCopyName = Path.GetFileName(fileInfos[0].FullName);
            }
        }
        /// <summary>
        /// 右键菜单粘贴事件
        /// </summary>
        private void item_paste_Click(object sender, EventArgs e)
        {
            List<FileInfo> fileInfos = GetSelectedFilesInListView();
            SelectedFilesStatus status = GetStatusOfSelectedFilesInListView(fileInfos);
            if (status == SelectedFilesStatus.NoSelected)
            {
                if (_sourceCopyPath != string.Empty)
                {
                    string targetCopyDir = GetCurrentDirPath();
                    _fileManager.CopyFileOrDir(_sourceCopyPath, Path.Combine(targetCopyDir, _sourceCopyName));
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
            string newFile = Path.Combine(GetCurrentDirPath(), "NewFile.txt");
            UserControl fileEditer = new FileEditerControl(_host, _shellData, newFile, false);
            _host.Ui.CreateNewTabPage("FileEdit", fileEditer);
        }
        /// <summary>
        /// 右键菜单下载文件到远程服务器
        /// </summary>
        private void item_downloadToServer_Click(object sender, EventArgs e)
        {
            //打开设置界面
            FormWget formWget = new FormWget(GetCurrentDirPath());
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

        /**
         * 采用绑定ContextMenuStrip的方式，Mac下可能不会产生该事件
         * 须手动绑定MouseClick事件
         */
        private void listView_File_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightMenu_FileManager.Show(listView_File, e.Location);
            }
        }
    }
}
