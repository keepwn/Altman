using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Altman.Model;
using PluginFramework;

namespace Plugin_PluginManager
{
    public partial class PluginManager : Form
    {
        private IHost _host;
        private Shell _shellData;

        private string _updateXmlPath = "./Plugins/update.xml";
        private string _updateXmlUrl = "";
        private UpdateInfo[] _updateInfos = null;
        private IEnumerable<IPlugin> _installedPlugins = null;

        public PluginManager(IHost host, Shell data)
        {
            InitializeComponent();

            this._host = host;
            this._shellData = data;

            _updateXmlUrl = ReadConfigXml();
            _installedPlugins = _host.Core.GetPlugins();
            DownloadUpdateXml();
            LoadInstalledPlugins();
        }

        private void btn_Close_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #region ShowInstalledPlugins
        private void LoadInstalledPlugins()
        {
            if (_installedPlugins == null) return;
            //clear
            lv_InstalledPlugins.Items.Clear();
            //loading
            foreach (var plugin in _installedPlugins)
            {
                string[] items = new string[] {
                    plugin.PluginInfo.Name,
                    plugin.PluginInfo.Author,
                    plugin.PluginInfo.Version,
                    plugin.PluginInfo.FileName
                    };
                ListViewItem viewItem = new ListViewItem(items);
                viewItem.Tag = plugin;
                lv_InstalledPlugins.Items.Add(viewItem);
            }
        }
        private void lv_InstalledPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_InstalledPlugins.SelectedItems.Count == 1)
            {
                IPlugin plugin = (IPlugin)(lv_InstalledPlugins.SelectedItems[0]).Tag;
                tbx_InstalledPluginsDescription.Text = plugin.PluginInfo.Description;
            }
        }
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lv_InstalledPlugins.CheckedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("You Will Remove This/These Plugins, Continue?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lv_InstalledPlugins.CheckedItems)
                    {
                        IPlugin plugin = (IPlugin)item.Tag;
                        string filepath = "./Plugins" + "/" + plugin.PluginInfo.FileName;
                        File.Delete(filepath);
                        item.Remove();
                    }
                    lbl_Msg.Visible = true;
                }
            }
        }
        #endregion

        #region ShowAvailablePlugins
        /// <summary>
        /// 读取xml文件
        /// </summary>
        public UpdateInfo[] ReadUpdateXml()
        {
            string filePath = _updateXmlPath;
            List<UpdateInfo> infos = new List<UpdateInfo>();
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/update/plugins"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "plugin")
                    {
                        UpdateInfo info = new UpdateInfo();

                        //判断name属性是否存在
                        if (child.Attributes["name"] == null) continue;
                        string name = child.Attributes["name"].Value;

                        XmlNode authorNode = child.SelectSingleNode("author");
                        if (authorNode == null) continue;
                        string author = authorNode.InnerText;

                        XmlNode desNode = child.SelectSingleNode("description");
                        if (desNode == null) continue;
                        string des = desNode.InnerText;

                        XmlNode versionNode = child.SelectSingleNode("version");
                        if (versionNode == null) continue;
                        string version = versionNode.Attributes["number"].Value;
                        string required = versionNode.Attributes["required"].Value;
                        string md5 = versionNode.Attributes["md5"].Value;

                        XmlNode downloadNode = child.SelectSingleNode("download");
                        if (downloadNode == null) continue;
                        string download = downloadNode.InnerText;

                        XmlNodeList installNode = child.SelectNodes("install/copy");
                        if (installNode == null) continue;
                        KeyValuePair<string, string>[] copys =
                            (from XmlNode node in installNode
                             select new KeyValuePair<string, string>(PreprocessString(node.Attributes["from"].Value), PreprocessString(node.Attributes["to"].Value))
                             ).ToArray();

                        XmlNodeList uninstallNode = child.SelectNodes("uninstall/delete");
                        if (uninstallNode == null) continue;
                        string[] deletes =
                            (from XmlNode node in uninstallNode
                             select PreprocessString(node.Attributes["file"].Value)
                             ).ToArray();

                        info.Name = name;
                        info.Author = author;
                        info.Description = des;
                        info.Version = new UpdateInfo.VersionInfo(version, required, md5);
                        info.DownloadUrl = download;
                        info.Install.CopyFiles = copys;
                        info.Uninstall.DeleteFiles = deletes;

                        infos.Add(info);
                    }
                }
                return infos.ToArray();
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        /// <summary>
        /// 下载xml文件
        /// </summary>
        private void DownloadUpdateXml()
        {
            //clear
            lv_AvailablePlugins.Items.Clear();
            ListViewItem item = new ListViewItem("Downloading update xml...");
            lv_AvailablePlugins.Items.Add(item);
            lv_AvailablePlugins.CheckBoxes = false;

            WebClient client = new WebClient();
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(_updateXmlUrl), _updateXmlPath);
        }
        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lv_AvailablePlugins.Items[0].Text = "Can't download update xml";
            }
            else
            {
                ShowAvailablePlugins();
                ShowUpdatablePlugins();
            }
        }
        /// <summary>
        /// 字符串预处理
        /// </summary>
        private string PreprocessString(string str)
        {
            str = str.Replace("$PLUGIN$", _host.App.AppPluginDir);
            str = str.Replace("$LANG$", _host.App.AppLanguageDir);
            str = str.Replace("$BIN$", _host.App.AppBinDir);
            return str;
        }
        /// <summary>
        /// 显示可用插件
        /// </summary>
        private void ShowAvailablePlugins()
        {
            if (_installedPlugins == null) return;
            if (!File.Exists(_updateXmlPath)) return;

            //clear
            lv_AvailablePlugins.Items.Clear();
            lv_AvailablePlugins.CheckBoxes = true;

            _updateInfos = ReadUpdateXml();
            if (_updateInfos == null) return;
            foreach (UpdateInfo info in _updateInfos)
            {
                //在列表中排除已安装的插件
                if (_installedPlugins.FirstOrDefault(x => x.PluginInfo.Name == info.Name) != null) continue;

                bool status = _host.App.AppVersion >= info.Version.RequiredVersion;
                info.CanUpdate = status;
                string[] items = new string[]
                        {
                            info.Name,
                            info.Author,
                            info.Version.Version.ToString(),
                            info.Version.RequiredVersion.ToString(),
                            info.CanUpdate.ToString()
                        };
                ListViewItem item = new ListViewItem(items);
                if (!info.CanUpdate) item.BackColor = Color.Gray;
                item.Tag = info;
                lv_AvailablePlugins.Items.Add(item);
            }
        }
        private void lv_AvailablePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_AvailablePlugins.SelectedItems.Count != 1) return;
            UpdateInfo info = (UpdateInfo)(lv_AvailablePlugins.SelectedItems[0]).Tag;
            tbx_AvailablePluginsDescription.Text = info.Description;
        }
        /// <summary>
        /// 刷新按钮
        /// </summary>
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            DownloadUpdateXml();
        }
        /// <summary>
        /// 安装按钮
        /// </summary>
        private void btn_Install_Click(object sender, EventArgs e)
        {
            UpdateInfo[] infos = (from ListViewItem item in lv_AvailablePlugins.CheckedItems
                                  where ((UpdateInfo)item.Tag).CanUpdate
                                  select (UpdateInfo)item.Tag
                                      ).ToArray();
            if (infos.Length <= 0) return;
            InstallForm install = new InstallForm(_host, infos);
            install.InstallPluginCompletedToDo += new EventHandler(install_InstallPluginCompletedToDo);
            install.ShowDialog();
        }
        /// <summary>
        /// 插件安装完成
        /// </summary>
        private void install_InstallPluginCompletedToDo(object sender, EventArgs e)
        {
            lbl_Msg.Visible = true;
        }
        #endregion

        #region ShowUpdatablePlugins
        /// <summary>
        /// 显示可升级插件
        /// </summary>
        private void ShowUpdatablePlugins()
        {
            if (_installedPlugins == null) return;
            if (_updateInfos == null) return;
            //clear
            lv_UpdatablePlugins.Items.Clear();
            foreach (var plugin in _installedPlugins)
            {
                //判断已安装的插件是否可以更新
                UpdateInfo info =
                    _updateInfos.FirstOrDefault(
                        x =>
                            x.Name == plugin.PluginInfo.Name
                            && new Version(plugin.PluginInfo.Version) < x.Version.Version);
                if (info == null) continue;

                bool status = _host.App.AppVersion >= info.Version.RequiredVersion;
                info.CanUpdate = status;
                string[] items = new string[]
                        {
                            info.Name,
                            info.Author,
                            plugin.PluginInfo.Version,
                            info.Version.Version.ToString(),
                            info.CanUpdate.ToString()
                        };
                ListViewItem item = new ListViewItem(items);
                if (!info.CanUpdate) item.BackColor = Color.Gray;
                item.Tag = info;
                lv_UpdatablePlugins.Items.Add(item);
            }
        }
        private void lv_UpdatablePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_UpdatablePlugins.SelectedItems.Count != 1) return;
            UpdateInfo info = (UpdateInfo)(lv_UpdatablePlugins.SelectedItems[0]).Tag;
            tbx_UpdatesPluginsDescription.Text = info.Description;
        }
        /// <summary>
        /// 升级按钮
        /// </summary>
        private void btn_Update_Click(object sender, EventArgs e)
        {
            UpdateInfo[] infos = (from ListViewItem item in lv_UpdatablePlugins.CheckedItems
                                  where ((UpdateInfo)item.Tag).CanUpdate
                                  select (UpdateInfo)item.Tag
                                      ).ToArray();
            if (infos.Length <= 0) return;
            InstallForm install = new InstallForm(_host, infos);
            install.InstallPluginCompletedToDo += new EventHandler(install_InstallPluginCompletedToDo);
            install.ShowDialog();
        }
        #endregion

        #region Init
        public string ReadConfigXml()
        {
            string defaultUpate = "http://www.keepwn.com/static/files/update.xml";
            string configPath = Path.Combine(_host.App.AppPluginConfigDir, "PluginManager.xml");
            if (!File.Exists(configPath))
            {
                return defaultUpate;
            }
            XmlDocument xml = new XmlDocument();
            xml.Load(configPath);
            XmlNode updateXmlNode = xml.SelectSingleNode("/config/updateXml");
            return updateXmlNode == null ? defaultUpate : updateXmlNode.InnerText;
        }
        #endregion
    }
}
