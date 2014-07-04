using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Altman.Common.AltData;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_FileManager
{
    public class FileManagerService
    {
        private IHostService _hostService;
        private Shell _shellData;
        public FileManagerService(IHostService hostService, Shell data)
        {
            this._hostService = hostService;
            this._shellData = data;
        }

        #region 获取wwwroot路径
        public event EventHandler<RunWorkerCompletedEventArgs> GetWwwRootPathCompletedToDo;
        public void GetWwwRootPath()
        {
            GetWwwRootPathBackground();
        }
        private void GetWwwRootPathBackground()
        {
            using (BackgroundWorker getWwwRootPath = new BackgroundWorker())
            {
                getWwwRootPath.DoWork += new DoWorkEventHandler(getWwwRootPath_DoWork);
                getWwwRootPath.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getWwwRootPath_RunWorkerCompleted);
                getWwwRootPath.RunWorkerAsync();
            }
        }
        private void getWwwRootPath_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/WwwRootPathCode", null);
            e.Result = ResultMatch.MatchResultToOsDisk(resultBytes,Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void getWwwRootPath_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetWwwRootPathCompletedToDo != null)
            {
                GetWwwRootPathCompletedToDo(null, e);
            }
        }

        #endregion

        #region 获取文件清单
        public event EventHandler<RunWorkerCompletedEventArgs> GetFileTreeCompletedToDo;
        public void GetFileTree(string dirPath)
        {
            GetFileTreeBackground(dirPath);
        }
        private void GetFileTreeBackground(string path)
        {
            using (BackgroundWorker getFileTree = new BackgroundWorker())
            {
                getFileTree.DoWork += new DoWorkEventHandler(getFileTree_DoWork);
                getFileTree.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getFileTree_RunWorkerCompleted);
                getFileTree.RunWorkerAsync(path);
            }
        }
        private void getFileTree_DoWork(object sender, DoWorkEventArgs e)
        {
            string dirPath = e.Argument as string;
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/FileTreeCode", new string[] { dirPath });
            e.Result = ResultMatch.MatchResultToOsFile(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void getFileTree_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetFileTreeCompletedToDo != null)
            {
                GetFileTreeCompletedToDo(null, e);
            }
        }

        #endregion

        #region 读文件操作

        public event EventHandler<RunWorkerCompletedEventArgs> ReadFileCompletedToDo;
        public void ReadFile(string filePath)
        {
            ReadFileBackground(filePath);
        }
        private void ReadFileBackground(string filePath)
        {
            using (BackgroundWorker readFile = new BackgroundWorker())
            {
                readFile.DoWork += readFile_DoWork;
                readFile.RunWorkerCompleted += readFile_RunWorkerCompleted;
                readFile.RunWorkerAsync(filePath);
            }
        }
        private void readFile_DoWork(object sender, DoWorkEventArgs e)
        {
            string filePath = e.Argument as string;
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/ReadFileCode", new string[] { filePath });
            e.Result = ResultMatch.MatchResultToString(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void readFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ReadFileCompletedToDo != null)
            {
                ReadFileCompletedToDo(null, e);
            }
        }
        #endregion

        #region 写文件操作

        public event EventHandler<RunWorkerCompletedEventArgs> WriteFileCompletedToDo;
        public void WriteFile(string filePath, string fileData)
        {
            WriteFileBackground(filePath, fileData);
        }
        private void WriteFileBackground(string filePath, string fileData)
        {
            using (BackgroundWorker writeFile = new BackgroundWorker())
            {
                writeFile.DoWork += writeFile_DoWork;
                writeFile.RunWorkerCompleted += writeFile_RunWorkerCompleted;
                writeFile.RunWorkerAsync(new string[] { filePath, fileData });
            }
        }
        private void writeFile_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/WriteFileCode", par);
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void writeFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (WriteFileCompletedToDo != null)
            {
                WriteFileCompletedToDo(null, e);
            }
        }
        #endregion

        #region 删除文件或目录操作
        public event EventHandler<RunWorkerCompletedEventArgs> DeleteFileOrDirCompletedToDo;
        public void DeleteFileOrDir(string fileOrDirPath)
        {
            DeleteFileOrDirBackground(fileOrDirPath);
        }
        private void DeleteFileOrDirBackground(string fileOrDirPath)
        {
            using (BackgroundWorker deleteFileOrDir = new BackgroundWorker())
            {
                deleteFileOrDir.DoWork += deleteFileOrDir_DoWork;
                deleteFileOrDir.RunWorkerCompleted += deleteFileOrDir_RunWorkerCompleted;
                deleteFileOrDir.RunWorkerAsync(fileOrDirPath);
            }
        }
        private void deleteFileOrDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileOrDirPath = e.Argument as string;
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/DeleteFileOrDirCode", new string[] { fileOrDirPath });
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void deleteFileOrDir_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DeleteFileOrDirCompletedToDo != null)
                DeleteFileOrDirCompletedToDo(null, e);
        }

        #endregion

        #region 复制文件或目录操作
        public event EventHandler<RunWorkerCompletedEventArgs> CopyFileOrDirCompletedToDo;
        public void CopyFileOrDir(string sourceFilePath, string targetFilePath)
        {
            CopyFileOrDirBackground(sourceFilePath, targetFilePath);
        }
        private void CopyFileOrDirBackground(string sourceFilePath, string targetFilePath)
        {
            using (BackgroundWorker copyFileOrDir = new BackgroundWorker())
            {
                copyFileOrDir.DoWork += copyFileOrDir_DoWork;
                copyFileOrDir.RunWorkerCompleted += copyFileOrDir_RunWorkerCompleted;
                copyFileOrDir.RunWorkerAsync(new string[] { sourceFilePath, targetFilePath });
            }
        }
        private void copyFileOrDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/CopyFileOrDirCode", par);
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void copyFileOrDir_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CopyFileOrDirCompletedToDo != null)
            {
                CopyFileOrDirCompletedToDo(null, e);
            }
        }
        #endregion

        #region 重命名文件或目录操作
        public event EventHandler<RunWorkerCompletedEventArgs> RenameFileOrDirCompletedToDo;
        public void RenameFileOrDir(string oldName, string newName)
        {
            RenameFileOrDirBackground(oldName, newName);
        }
        private void RenameFileOrDirBackground(string oldName, string newName)
        {
            using (BackgroundWorker renameFileOrDir = new BackgroundWorker())
            {
                renameFileOrDir.DoWork += renameFileOrDir_DoWork;
                renameFileOrDir.RunWorkerCompleted += renameFileOrDir_RunWorkerCompleted;
                renameFileOrDir.RunWorkerAsync(new string[] { oldName, newName });
            }
        }
        private void renameFileOrDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/RenameFileOrDirCode", par);
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void renameFileOrDir_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RenameFileOrDirCompletedToDo != null)
            {
                RenameFileOrDirCompletedToDo(null, e);
            }
        }
        #endregion

        #region 创建文件夹
        public event EventHandler<RunWorkerCompletedEventArgs> CreateDirCompletedToDo;
        public void CreateDir(string dirPath)
        {
            CreateDirBackground(dirPath);
        }
        private void CreateDirBackground(string dirPath)
        {
            using (BackgroundWorker createDir = new BackgroundWorker())
            {
                createDir.DoWork += createDir_DoWork;
                createDir.RunWorkerCompleted += createDir_RunWorkerCompleted;
                createDir.RunWorkerAsync(dirPath);
            }
        }
        private void createDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string dirPath = e.Argument as string;
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/CreateDirCode", new string[] { dirPath });
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void createDir_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CreateDirCompletedToDo != null)
            {
                CreateDirCompletedToDo(null, e);
            }
        }

        #endregion

        #region 修改文件或文件夹时间
        public event EventHandler<RunWorkerCompletedEventArgs> ModifyFileOrDirTimeCompletedToDo;
        public void ModifyFileOrDirTime(string filePath, string aTime)
        {
            ModifyFileOrDirTimeBackground(filePath, aTime);
        }
        private void ModifyFileOrDirTimeBackground(string filePath, string aTime)
        {
            using (BackgroundWorker modifyFileOrDirTime = new BackgroundWorker())
            {
                modifyFileOrDirTime.DoWork += modifyFileOrDirTime_DoWork;
                modifyFileOrDirTime.RunWorkerCompleted += modifyFileOrDirTime_RunWorkerCompleted;
                modifyFileOrDirTime.RunWorkerAsync(new string[] { filePath, aTime });
            }
        }
        private void modifyFileOrDirTime_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/ModifyFileOrDirTimeCode", par);
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void modifyFileOrDirTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ModifyFileOrDirTimeCompletedToDo != null)
            {
                ModifyFileOrDirTimeCompletedToDo(null, e);
            }
        }
        #endregion

        #region 下载文件到服务器
        public event EventHandler<RunWorkerCompletedEventArgs> WgetCompletedToDo;
        public void Wget(string urlPath, string savePath)
        {
            WgetBackground(urlPath, savePath);
        }
        private void WgetBackground(string urlPath, string savePath)
        {
            using (BackgroundWorker wget = new BackgroundWorker())
            {
                wget.DoWork += wget_DoWork;
                wget.RunWorkerCompleted += wget_RunWorkerCompleted;
                wget.RunWorkerAsync(new string[] { urlPath, savePath });
            }
        }
        private void wget_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];

            byte[] resultBytes = _hostService.Core.SubmitCommand(_shellData, "FileManager/WgetCode", par);
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void wget_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (WgetCompletedToDo != null)
            {
                WgetCompletedToDo(null, e);
            }
        }
        #endregion
    }
}

