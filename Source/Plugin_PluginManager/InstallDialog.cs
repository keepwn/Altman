using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Eto.Forms;
using Plugin_PluginManager.Model;

namespace Plugin_PluginManager
{
    public partial class InstallDialog : Dialog
    {
        private string _unzipBaseDir;

        private IHost _host;
        private List<UpdateInfo> _infos;

        public EventHandler InstallPluginCompletedToDo;
        public InstallDialog(IHost host, UpdateInfo[] infos)
        {
            Init();

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
            get { return _labelDownload.Text; }
			set { _labelDownload.Text = "Downloading: " + value; }
        }
        public int Progress
        {
            get { return _progressBarDownload.Value; }
			set { _progressBarDownload.Value = value; }
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
            Close();
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
            var downloadUrl = info.DownloadUrl;
            var saveName = Path.Combine(_unzipBaseDir, Path.GetFileName(info.DownloadUrl) ?? info.Name+".zip");

            DownloadUrl = downloadUrl;
            Progress = 0;

            var client = new WebClient();
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
            var info = e.UserState as UpdateInfo;
            if (e.Error != null)
            {
                var msg = string.Format("Download {0} failed", info.Name);
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
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
            var msg = "";
            //解压
            DownloadUrl = string.Format("Unzip {0} ...", info.Name);
            Progress += 25;
            if (!Unzip(info))
            {
                msg = string.Format("Unzip {0} failed", info.Name);
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
            }
            //拷贝
            DownloadUrl = string.Format("Install {0} ...", info.Name);
            Progress += 25;
            var unzipDir = Path.Combine(_unzipBaseDir, info.Name);
			if (CopyPluginFiles(info.Install.CopyFiles, unzipDir, _host.App.AppCurrentDir, _host.App.AppPluginDir, info.Name))
            {
                msg = string.Format("Install {0} success", info.Name);
				MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxType.Information);
            }
            else
            {
                msg = string.Format("Install {0} failed", info.Name);
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        private bool Unzip(UpdateInfo info)
        {
            var zipFile = Path.Combine(_unzipBaseDir, Path.GetFileName(info.DownloadUrl)??info.Name+".zip");
            var unzipDir = Path.Combine(_unzipBaseDir, info.Name);

            return ZipUtil.Decompress(zipFile, unzipDir, true);
        }
        /// <summary>
        /// 拷贝插件文件
        /// </summary>
        private bool CopyPluginFiles(KeyValuePair<string, string>[] copyFiles, string unzipDir,string appCurrentDir, string appPluginsDir, string pluginName)
        {
            foreach (var a in copyFiles)
            {
                var source = Path.Combine(unzipDir, a.Key);
				//如果目标路径为相对路径，则复制目标到Plugins/AAA/目录
	            var desDir = "";
	            if (!Path.IsPathRooted(a.Value))
	            {
					desDir = Path.Combine(appPluginsDir, pluginName, a.Value);
	            }
                desDir = Path.Combine(appCurrentDir, a.Value);
                var destFile = Path.Combine(appCurrentDir, a.Value, Path.GetFileName(source));
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
