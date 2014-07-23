using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Altman.Common.AltData;
using Altman.Model;
using PluginFramework;

namespace Plugin_FileManager
{
    public class FileManagerService
    {
        private IHost _host;
        private Shell _shellData;
        public FileManagerService(IHost host, Shell data)
        {
            this._host = host;
            this._shellData = data;
        }

        private void RunBackground(DoWorkEventHandler doWork, object argument, RunWorkerCompletedEventHandler runWorkerCompleted)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                backgroundWorker.DoWork += doWork;
                backgroundWorker.RunWorkerCompleted += runWorkerCompleted;
                backgroundWorker.RunWorkerAsync(argument);
            }
        }


        #region 获取wwwroot路径
        public event EventHandler<RunWorkerCompletedEventArgs> GetWwwRootPathCompletedToDo;
        public void GetWwwRootPath()
        {
            RunBackground(getWwwRootPath_DoWork, null, getWwwRootPath_RunWorkerCompleted);
        }
        private void getWwwRootPath_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/WwwRootPathCode", null);
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
            RunBackground(getFileTree_DoWork, new string[]{ dirPath }, getFileTree_RunWorkerCompleted);
        }
        private void getFileTree_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] dirPath = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/FileTreeCode", dirPath);
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
            RunBackground(readFile_DoWork, new string[] { filePath }, readFile_RunWorkerCompleted);
        }
        private void readFile_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filePath = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/ReadFileCode", filePath);
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
            RunBackground(writeFile_DoWork, new string[] { filePath, fileData }, writeFile_RunWorkerCompleted);
        }
        private void writeFile_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/WriteFileCode", par);
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
            RunBackground(deleteFileOrDir_DoWork, new string[] { fileOrDirPath }, deleteFileOrDir_RunWorkerCompleted);
        }
        private void deleteFileOrDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] fileOrDirPath = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/DeleteFileOrDirCode", fileOrDirPath);
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
            RunBackground(copyFileOrDir_DoWork, new string[] { sourceFilePath, targetFilePath }, copyFileOrDir_RunWorkerCompleted);
        }
        private void copyFileOrDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/CopyFileOrDirCode", par);
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
            RunBackground(renameFileOrDir_DoWork, new string[] { oldName, newName }, renameFileOrDir_RunWorkerCompleted);
        }
        private void renameFileOrDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/RenameFileOrDirCode", par);
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
            RunBackground(createDir_DoWork, new string[] { dirPath }, createDir_RunWorkerCompleted);
        }
        private void createDir_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] dirPath = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/CreateDirCode", dirPath);
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
            RunBackground(modifyFileOrDirTime_DoWork, new string[] { filePath, aTime }, modifyFileOrDirTime_RunWorkerCompleted);
        }
        private void modifyFileOrDirTime_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/ModifyFileOrDirTimeCode", par);
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
            RunBackground(wget_DoWork, new string[] { urlPath, savePath }, wget_RunWorkerCompleted);
        }
        private void wget_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];

            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/WgetCode", par);
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

