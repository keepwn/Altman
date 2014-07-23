using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Altman.Common.AltData;
using Altman.Common.AltEventArgs;
using Altman.Common.AltException;
using Altman.Model;
using PluginFramework;

namespace Plugin_FileManager
{
    public class FileUploadOrDownload
    {
        private IHost _host;
        private Shell _shellData;

        private string _sourceFilePath;
        private string _targetFilePath;
        private BackgroundWorker _backgroundWorker;

        public FileUploadOrDownload(IHost host, Shell shellData, string sourceFilePath, string targetFilePath)
        {
            _host = host;
            _shellData = shellData;
            _sourceFilePath = sourceFilePath;
            _targetFilePath = targetFilePath;
        }

        public void StartToUploadFile()
        {
            FilePreproccess(_sourceFilePath);
        }
        public void StartToDownloadFile()
        {
            DownloadFile(_sourceFilePath, _targetFilePath);
        }

        #region 文件预处理
        /// <summary>
        /// 读文件转化为hex
        /// </summary>
        private string ReadFileToHexStr(string sourceFilePath)
        {
            FileStream fs = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
            long length = fs.Length;
            BinaryReader r = new BinaryReader(fs);
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                byte tmp = r.ReadByte();
                strBuilder.Append(tmp.ToString("X2"));
                if (i % (1024 * 10) == 0)
                    _backgroundWorker.ReportProgress((int)(100 * i / length));
            }
            r.Close();
            fs.Close();
            return strBuilder.ToString();
        }
        private void FilePreproccess(string sourceFilePath)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerAsync(sourceFilePath);
        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (UploadFileProgressChangedToDo != null)
            {
                UploadFileProgressChangedToDo(null, new AltProgressChangedEventArgs(e.ProgressPercentage / 2));
            }
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string sourceFilePath = e.Argument as string;
            e.Result = ReadFileToHexStr(sourceFilePath);
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool isSuccess = false;
            Exception exception = null;
            try
            {
                if (e.Error != null)
                {
                    exception = e.Error;
                    throw new LocalCustomException("上传准备阶段", "上传文件预处理出现错误", e.Error);
                }
                else
                {
                    UploadFile(_targetFilePath, (string)e.Result);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                isSuccess = false;
            }
            finally
            {
                if (!isSuccess)
                {
                    if (UploadFileCompletedToDo != null)
                    {
                        UploadFileCompletedToDo(null, new RunWorkerCompletedEventArgs(null, exception,true));
                    }
                }
            }
        }
        #endregion

        #region 下载文件到本地
        public void DownloadFile(string filePath,string savePath)
        {
            DownloadFileBackground(filePath, savePath);
        }
        private void DownloadFileBackground(string filePath, string savePath)
        {
            using (BackgroundWorker download = new BackgroundWorker())
            {
                download.DoWork += download_DoWork;
                download.RunWorkerCompleted += download_RunWorkerCompleted;
                download.RunWorkerAsync(new string[]{filePath, savePath});
            }
        }
        private void download_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];

            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/DownloadFileCode", new string[] { par[0] });
            byte[] fileBytes = ResultMatch.MatchResultToFile(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
            e.Result = SaveFile(par[1], fileBytes);
        }
        private void download_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DownloadFileCompletedToDo != null)
            {
                DownloadFileCompletedToDo(null, e);
            }
        }
        private bool SaveFile(string savePath,byte[] bytes)
        {
            bool returnResult = true;
            try
            {
                File.WriteAllBytes(savePath, bytes);
            }
            catch
            {
                returnResult = false;
            }
            return returnResult;
        }

        #endregion

        #region 上传文件到服务器
        public void UploadFile(string saveFilePath, string fileHexData)
        {
            UploadFileBackground(saveFilePath, fileHexData);
        }
        private void UploadFileBackground(string saveFilePath, string fileHexData)
        {
            using (BackgroundWorker upload = new BackgroundWorker())
            {
                upload.DoWork += upload_DoWork;
                upload.RunWorkerCompleted += upload_RunWorkerCompleted;
                upload.RunWorkerAsync(new string[] { saveFilePath, fileHexData });
            }
        }
        private void upload_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "FileManager/UploadFileCode", par);
            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void upload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (UploadFileCompletedToDo != null)
            {
                UploadFileCompletedToDo(null, e);
            }
        }
        #endregion

        public event EventHandler<RunWorkerCompletedEventArgs> UploadFileCompletedToDo;
        public event EventHandler<RunWorkerCompletedEventArgs> DownloadFileCompletedToDo;
        public event EventHandler<AltProgressChangedEventArgs> UploadFileProgressChangedToDo;
        public event EventHandler<AltProgressChangedEventArgs> DownloadFileProgressChangedToDo;
    }
}
