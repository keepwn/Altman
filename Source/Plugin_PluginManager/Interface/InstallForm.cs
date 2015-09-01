using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using Altman.Plugin.Interface;
using Eto.Forms;
using Plugin_PluginManager.Model;

namespace Plugin_PluginManager.Interface
{
    public partial class InstallForm : Form
    {
        private string _unzipBaseDir;
        private bool _isOffline;

        private IHost _host;
        private List<string> _pluginZipPaths;

        public EventHandler InstallPluginCompletedToDo;
        public InstallForm(IHost host)
        {
            Init();

            _host = host;
            _unzipBaseDir = Path.Combine(_host.App.AppCurrentDir, "tmp");

            if (!Directory.Exists(_unzipBaseDir))
            {
                Directory.CreateDirectory(_unzipBaseDir);
            }
        }

        public void InstallOffline(IEnumerable<string> pluginZipLocalPaths)
        {
            _pluginZipPaths = pluginZipLocalPaths.ToList();
            _isOffline = true;

            DownloadNextPlugin();
        }

        public void InstallOnline(IEnumerable<string> pluginZipUrlPaths)
        {
            _pluginZipPaths = pluginZipUrlPaths.ToList();
            _isOffline = false;

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
            if (_pluginZipPaths.Count > 0)
            {
                if (!_isOffline)
                {
                    DownloadPlugin(_pluginZipPaths[0]);
                }
                else
                {
                    InstallPlugin(_pluginZipPaths[0], true);
                    _pluginZipPaths.RemoveAt(0);
                    DownloadNextPlugin();
                }
            }
            else
            {
                CloseForm();
            }
        }
        private void DownloadPlugin(string downloadUrl)
        {
            var saveName = Path.Combine(_unzipBaseDir, Path.GetFileName(downloadUrl) ?? "tmp000.zip");

            DownloadUrl = downloadUrl;
            Progress = 0;

            var client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(downloadUrl), saveName, downloadUrl);
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage/2;
        }
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var downloadUrl = e.UserState as string;
            if (e.Error != null)
            {
                var msg = string.Format("Download {0} failed", downloadUrl);
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
            }
            else
            {
                var zip = Path.GetFileName(downloadUrl) ?? "tmp000.zip";
                InstallPlugin(zip, false);
            }

            //出队列
            _pluginZipPaths.RemoveAt(0);
            DownloadNextPlugin();
        }
        
        /// <summary>
        /// 安装插件
        /// </summary>
        private void InstallPlugin(string pluginZipName, bool offline)
        {
            var span = offline ? 1 : 2;
            var msg = "";
            // unzip
            DownloadUrl = string.Format("Unzip {0} ...", pluginZipName);
            Progress += 50/span;
            if (!Unzip(pluginZipName))
            {
                msg = string.Format("Unzip {0} failed", pluginZipName);
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
                return;
            }

            // get plugin info
            var pluginUnzipDir = Path.Combine(_unzipBaseDir, Path.GetFileNameWithoutExtension(pluginZipName));
            var pluginInfoXmlPath = Path.Combine(pluginUnzipDir, "info.xml");
            var info = ReadInfoXml(pluginInfoXmlPath);

            // pre check
            if (info == null)
            {
                msg = string.Format("PluginZipFile {0}'s info.xml don't exists or wrong format", pluginZipName);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
                return;
            }
            if (!info.CanUpdate)
            {
                msg = string.Format("Plugin {0}'s version < required version", info.Name);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxType.Error);
                return;
            }
            
            // copys
            DownloadUrl = string.Format("Install {0} ...", info.Name);
            Progress += 50/span;
            var targetPluginDir = Path.Combine(_host.App.AppPluginDir, info.Name);
            if (Directory.Exists(targetPluginDir))
            {
                msg = string.Format("Plugin {0}'s dir already exists or not empty, to continue?", info.Name);
                var result = MessageBox.Show(msg, MessageBoxButtons.YesNo, MessageBoxType.Question);
                if (result == DialogResult.No)
                    return;
            }

            if (CopyPluginFiles(pluginUnzipDir, targetPluginDir))
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

        private bool Unzip(string pluginZipName)
        {
            var zipFile = Path.Combine(_unzipBaseDir, pluginZipName);
            var unzipDir = Path.Combine(_unzipBaseDir, Path.GetFileNameWithoutExtension(pluginZipName));

            return ZipUtil.Decompress(zipFile, unzipDir, true);
        }

        private UpdateInfo ReadInfoXml(string infoPath)
        {
            if (!File.Exists(infoPath)) return null;
            try
            {
                var info = new UpdateInfo();
                var xml = new XmlDocument();
                xml.Load(infoPath);             
                var root = xml.SelectSingleNode("/plugin");

                var nameNode = root.SelectSingleNode("name");
                var name = nameNode != null ? nameNode.InnerText : "";

                var authorNode = root.SelectSingleNode("author");
                var author = authorNode != null ? authorNode.InnerText : "";

                var desNode = root.SelectSingleNode("description");
                var des = desNode != null ? desNode.InnerText : "";

                var versionNode = root.SelectSingleNode("version");
                var version = versionNode != null ? versionNode.Attributes["number"].Value : "";
                var required = versionNode != null ? versionNode.Attributes["required"].Value : "";

                info.Name = name;
                info.Author = author;
                info.Description = des;
                info.Version = new UpdateInfo.VersionInfo(version, required, "");
                info.CanUpdate = _host.App.AppVersion >= info.Version.RequiredVersion;

                return info;
            }
            catch (Exception ex)
            {
            }
            return null;      
        }

        private bool CopyPluginFiles(string unzipDir, string targetPluginDir)
        {
            try
            {
                CopyFilesOfDir(unzipDir, targetPluginDir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CopyFilesOfDir(string srcDirPath, string targetDirPath)
        {
            if (targetDirPath[targetDirPath.Length - 1] != Path.DirectorySeparatorChar)
            {
                targetDirPath += Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(targetDirPath))
            {
                Directory.CreateDirectory(targetDirPath);
            }

            var fileList = Directory.GetFileSystemEntries(srcDirPath);
            foreach (var file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyFilesOfDir(file, Path.Combine(targetDirPath + Path.GetFileName(file)));
                }
                else
                {
                    File.Copy(file, Path.Combine(targetDirPath + Path.GetFileName(file)), true);
                }
            }
        }

    }
}
