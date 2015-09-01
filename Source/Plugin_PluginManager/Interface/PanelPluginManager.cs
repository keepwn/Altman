using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Eto.Forms;
using Plugin_PluginManager.Model;
using Plugin_PluginManager.Resources;

namespace Plugin_PluginManager.Interface
{
    public partial class PanelPluginManager : Form
    {
        private IHost _host;

	    private string _updateXmlPath;
	    private string _updateXmlUrl;
        private UpdateInfo[] _updateInfos = null;
        private IEnumerable<IPlugin> _installedPlugins = null;

		public PanelPluginManager(IHost host, PluginParameter data)
        {
			this._host = host;

			// init StrRes to translate string
			StrRes.SetHost(_host);
            Init();

			_updateXmlPath = Path.Combine(_host.App.AppPluginDir, new PluginInfo().Name, "update.xml");
			_updateXmlUrl = ReadConfigXml();
			_installedPlugins = PluginProvider.GetPlugins();
			DownloadUpdateXml();
			LoadInstalledPlugins();
        }		

		void _buttonSetting_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}

		void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}

        void _buttonInstallFrom_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select Altman Plugin To Install",
                Filters = {new FileDialogFilter("Altman Plugin Zip File", ".zip")}
            };
            if (openFileDialog.ShowDialog(Application.Instance.MainForm) == DialogResult.Ok)
            {
                var srcfiles = openFileDialog.Filenames;

                var install = new InstallForm(_host);
                install.InstallPluginCompletedToDo += new EventHandler(install_InstallPluginCompletedToDo);               
                install.Show();
                install.InstallOffline(srcfiles);
            }
        }

		void _buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

        #region ShowInstalledPlugins
        private void LoadInstalledPlugins()
        {
            if (_installedPlugins == null) return;
	        var items = _installedPlugins.Select(plugin => new PluginModel(plugin)).ToList();
	        _gridViewInstalled.DataStore = items;
        }

		void _gridViewInstalled_SelectionChanged(object sender, EventArgs e)
		{
			if (_gridViewInstalled.SelectedItem != null)
			{
				var plugin = (_gridViewInstalled.SelectedItem as PluginModel).Plugin;
				_textAreatInstalledDes.Text = plugin != null ? plugin.PluginInfo.Description : "";
			}
		}

		void _buttonRemove_Click(object sender, EventArgs e)
		{
			var items = _gridViewInstalled.DataStore as List<PluginModel>;
			if (items != null && items.Any(r => r.Checked))
			{
				DialogResult result = MessageBox.Show(
					this,
					"You Will Remove This/These Dir Of Plugins , Continue?",
					"Warning",
					MessageBoxButtons.YesNo,
					MessageBoxType.Error);
				if (result == DialogResult.Yes)
				{
                    var delItems = new List<PluginModel>();
                    foreach (var item in items.Where(r => r.Checked))
                    {
                        var plugin = item.Plugin;
                        //删除插件所在文件夹
                        var dirPath = Path.Combine(_host.App.AppPluginDir, plugin.PluginInfo.Name);
                        if (Directory.Exists(dirPath))
                            Directory.Delete(dirPath, true);
                        delItems.Add(item);
                    }
                    foreach (var item in delItems)
                    {
                        items.Remove(item);
                    }
                    _gridViewInstalled.DataStore = items;
                    _labelMsg.Visible = true;
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
            var filePath = _updateXmlPath;
            var infos = new List<UpdateInfo>();
            try
            {
                var xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/update/plugins"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "plugin")
                    {
                        var info = new UpdateInfo();

                        //判断name属性是否存在
                        if (child.Attributes["name"] == null) continue;
                        var name = child.Attributes["name"].Value;

                        XmlNode authorNode = child.SelectSingleNode("author");
                        if (authorNode == null) continue;
                        var author = authorNode.InnerText;

                        XmlNode desNode = child.SelectSingleNode("description");
                        if (desNode == null) continue;
                        var des = desNode.InnerText;

                        XmlNode versionNode = child.SelectSingleNode("version");
                        if (versionNode == null) continue;
                        var version = versionNode.Attributes["number"].Value;
                        var required = versionNode.Attributes["required"].Value;
                        var md5 = versionNode.Attributes["md5"].Value;

                        XmlNode downloadNode = child.SelectSingleNode("download");
                        if (downloadNode == null) continue;
                        var download = downloadNode.InnerText;

                        info.Name = name;
                        info.Author = author;
                        info.Description = des;
                        info.Version = new UpdateInfo.VersionInfo(version, required, md5);
                        info.DownloadUrl = download;

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
	        var tip = new PluginModel("Downloading update xml...");
			_gridViewAvailable.DataStore = new List<PluginModel> { tip };

            var client = new WebClient();
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(_updateXmlUrl), _updateXmlPath);
        }
        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
				_buttonInstall.Enabled = false;
				var tip = new PluginModel("Can't download update xml");
				_gridViewAvailable.DataStore = new List<PluginModel> { tip };            
            }
            else
            {
				_buttonInstall.Enabled = true;
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

	        var items = new List<PluginModel>();
	        _updateInfos = ReadUpdateXml();
	        if (_updateInfos != null)
	        {
		        foreach (var info in _updateInfos)
		        {
			        //在列表中排除已安装的插件
			        if (_installedPlugins.FirstOrDefault(x => x.PluginInfo.Name == info.Name) != null)
						continue;

					var canInstall = _host.App.AppVersion >= info.Version.RequiredVersion;
			        info.CanUpdate = canInstall;

			        var item = new PluginModel(info.Name);
			        item.Author = info.Author;
			        item.AvailableVersion = info.Version.Version.ToString();
			        item.RequiredVersion = info.Version.RequiredVersion.ToString();
					item.CanInstall = info.CanUpdate.ToString();
			        item.Tag = info;

			        items.Add(item);
		        }
	        }
	        _gridViewAvailable.DataStore = items;
        }

		void _gridViewAvailable_SelectionChanged(object sender, EventArgs e)
		{
			if (_gridViewAvailable.SelectedItem != null)
			{
				var info = (UpdateInfo)(_gridViewAvailable.SelectedItem as PluginModel).Tag;
				_textAreatAvailableDes.Text = info != null ? info.Description : "";
			}
		}
        /// <summary>
        /// 刷新按钮
        /// </summary>
		void _buttonRefresh_Click(object sender, EventArgs e)
		{
			DownloadUpdateXml();
		}
        /// <summary>
        /// 安装按钮
        /// </summary>
		void _buttonInstall_Click(object sender, EventArgs e)
		{
			var items = _gridViewAvailable.DataStore as List<PluginModel>;
	        if (items != null && items.Any(r => r.Checked))
	        {
				var infos = items.Select(r => (r.Tag as UpdateInfo)).Where(r => r.CanUpdate).ToArray();
				if (infos.Length <= 0) return;
	            var install = new InstallForm(_host);
				install.InstallPluginCompletedToDo += new EventHandler(install_InstallPluginCompletedToDo);
		        install.Show();
                install.InstallOnline(infos.Select(r => r.DownloadUrl));
	        }
		}
        /// <summary>
        /// 插件安装完成
        /// </summary>
        private void install_InstallPluginCompletedToDo(object sender, EventArgs e)
        {
	        _labelMsg.Visible = true;
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

			var items = new List<PluginModel>();
            foreach (var plugin in _installedPlugins)
            {
                //判断已安装的插件是否可以更新
                var info =_updateInfos.FirstOrDefault(x =>
					x.Name == plugin.PluginInfo.Name
					&& new Version(plugin.PluginInfo.Version) < x.Version.Version);
                if (info == null) continue;

                var canUpdate = _host.App.AppVersion >= info.Version.RequiredVersion;
	            info.CanUpdate = canUpdate;

	            var item = new PluginModel(info.Name);
	            item.Author = info.Author;
	            item.InstalledVersion = plugin.PluginInfo.Version;
	            item.AvailableVersion = info.Version.Version.ToString();
				item.CanUpdate = info.CanUpdate.ToString();
				item.Tag = info;

	            items.Add(item);
            }
	        _gridViewUpdatable.DataStore = items;
        }

		void _gridViewUpdatable_SelectionChanged(object sender, EventArgs e)
		{
			if (_gridViewUpdatable.SelectedItem != null)
			{
				var info = (UpdateInfo)(_gridViewUpdatable.SelectedItem as PluginModel).Tag;
				_textAreatUpdatesDes.Text = info != null ? info.Description : "";
			}
		}
        /// <summary>
        /// 升级按钮
        /// </summary>
		void _buttonUpdate_Click(object sender, EventArgs e)
		{
			var items = _gridViewUpdatable.DataStore as List<PluginModel>;
			if (items != null && items.Any(r => r.Checked))
			{
				var infos = items.Select(r => (r.Tag as UpdateInfo)).Where(r => r.CanUpdate).ToArray();
				if (infos.Length <= 0) return;
				var install = new InstallForm(_host);
				install.InstallPluginCompletedToDo += new EventHandler(install_InstallPluginCompletedToDo);
				install.Show();
                install.InstallOnline(infos.Select(r => r.DownloadUrl));
			}
		}
        #endregion

        #region Init
        public string ReadConfigXml()
        {
            var defaultUpate = "http://altman.keepwn.com/update.xml";
			var configPath = Path.Combine(_host.App.AppPluginDir, new PluginInfo().Name, "PluginManager.xml");
			if (!File.Exists(configPath))
            {
                return defaultUpate;
            }
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            var updateXmlNode = xmlDoc.SelectSingleNode("/config/updateXml");
            return updateXmlNode == null ? defaultUpate : updateXmlNode.InnerText;
        }
		#endregion

		#region Sort Columns
		void _gridViewUpdatable_ColumnHeaderClick(object sender, GridColumnEventArgs e)
		{
			Sort(_gridViewUpdatable, e.Column.ID);
		}

		void _gridViewAvailable_ColumnHeaderClick(object sender, GridColumnEventArgs e)
		{
			Sort(_gridViewAvailable, e.Column.ID);
		}

		void _gridViewInstalled_ColumnHeaderClick(object sender, GridColumnEventArgs e)
		{
			Sort(_gridViewInstalled, e.Column.ID);
		}

		private bool _nameIsAscending;
		public static int SortStringAscending(string x, string y, bool isAscending)
		{
			if (isAscending)
				return String.Compare(x, y, StringComparison.Ordinal);
			return -String.Compare(x, y, StringComparison.Ordinal);
		}

		public void Sort(GridView gridView, string columnText)
	    {
			var items = gridView.DataStore as List<PluginModel>;
			if (items == null) return;
			switch (columnText)
			{
				case "Name":
					_nameIsAscending = !_nameIsAscending;
					items.Sort((a, b) => SortStringAscending(a.Name, b.Name, _nameIsAscending));
					gridView.DataStore = items;
					break;
			}			
	    }
		#endregion
	}
}
