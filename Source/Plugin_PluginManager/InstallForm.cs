using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PluginFramework;

namespace Plugin_PluginManager
{
    public partial class InstallForm : Form
    {
        private string _unzipBaseDir;

        private IHost _host;
        private List<UpdateInfo> _infos;

        public EventHandler InstallPluginCompletedToDo;
        public InstallForm(IHost host, UpdateInfo[] infos)
        {
            InitializeComponent();

            _host = host;
            _infos = new List<UpdateInfo>(infos);

            _unzipBaseDir = Path.Combine(_host.App.AppCurrentDir, "tmp");
            if (!Directory.Exists(_unzipBaseDir))
            {
                Directory.CreateDirectory(_unzipBaseDir);
            }

            DownloadNextPlugin();
        }

        #region 属性
        public string DownloadUrl
        {
            get { return lbl_download.Text; }
            set { lbl_download.Text = "Downloading: " + value; }
        }
        public int Progress
        {
            get { return progressBar_download.Value; }
            set { progressBar_download.Value = value; }
        }
        #endregion

        private void CloseForm()
        {
            if (InstallPluginCompletedToDo != null)
            {
                InstallPluginCompletedToDo(null,null);
            }
            //清理临时文件夹
            if (Directory.Exists(_unzipBaseDir))
            {
                Directory.Delete(_unzipBaseDir, true);
            }
            if (!this.IsDisposed)
            {
                this.Close();
            }
        }

        private void DownloadNextPlugin()
        {
            //下载第一个
            if (_infos.Count > 0)
            {
                DownloadPlugin(_infos[0]);
            }
            else
            {
                CloseForm();
            }
        }
        private void DownloadPlugin(UpdateInfo info)
        {
            string downloadUrl = info.DownloadUrl;
            string saveName = Path.Combine(_unzipBaseDir, Path.GetFileName(info.DownloadUrl) ?? info.Name+".zip");

            DownloadUrl = downloadUrl;
            Progress = 0;

            WebClient client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(downloadUrl), saveName, info);
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage/2;
        }
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            UpdateInfo info = e.UserState as UpdateInfo;
            if (e.Error != null)
            {
                string msg = string.Format("Download {0} failed", info.Name);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                InstallPlugin(info);
            }

            //出队列
            _infos.RemoveAt(0);
            DownloadNextPlugin();
        }
        
        /// <summary>
        /// 安装插件
        /// </summary>
        private void InstallPlugin(UpdateInfo info)
        {
            string msg = "";
            //解压
            DownloadUrl = string.Format("Unzip {0} ...", info.Name);
            Progress += 25;
            if (!Unzip(info))
            {
                msg = string.Format("Unzip {0} failed", info.Name);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //拷贝
            DownloadUrl = string.Format("Install {0} ...", info.Name);
            Progress += 25;
            string unzipDir = Path.Combine(_unzipBaseDir, info.Name);
            if (CopyPluginFiles(info.Install.CopyFiles, unzipDir, _host.App.AppCurrentDir))
            {
                msg = string.Format("Install {0} success", info.Name);
                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                msg = string.Format("Install {0} failed", info.Name);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        private bool Unzip(UpdateInfo info)
        {
            string zipFile = Path.Combine(_unzipBaseDir, Path.GetFileName(info.DownloadUrl)??info.Name+".zip");
            string unzipDir = Path.Combine(_unzipBaseDir, info.Name);

            return ZipUtil.Decompress(zipFile, unzipDir, true);
        }
        /// <summary>
        /// 拷贝插件文件
        /// </summary>
        private bool CopyPluginFiles(KeyValuePair<string, string>[] copyFiles, string unzipDir, string appCurrentDir)
        {
            foreach (KeyValuePair<string, string> a in copyFiles)
            {
                string source = Path.Combine(unzipDir, a.Key);
                string desDir = Path.Combine(appCurrentDir, a.Value);
                string destFile = Path.Combine(appCurrentDir, a.Value, Path.GetFileName(source));
                try
                {
                    if (!Directory.Exists(desDir))
                        Directory.CreateDirectory(desDir);
                    File.Copy(source, destFile, true);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
