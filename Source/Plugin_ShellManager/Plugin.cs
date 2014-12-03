using System;
using System.ComponentModel.Composition;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Plugin_ShellManager.Actions;
using Plugin_ShellManager.Interface;

namespace Plugin_ShellManager
{
    [Export(typeof(IPlugin))]
	public class Plugin : IControlPlugin
    {
        private object _userControl;
        private PluginInfo _pluginInfo;
        private IPluginSetting _pluginSetting;
		[Import(typeof(IHost))]
		private IHost _host;

        public Plugin()
        {
            _pluginInfo = new PluginInfo();
            _pluginSetting = new PluginSetting();
        }

        public IPluginInfo PluginInfo
        {
            get { return _pluginInfo; }
        }

        public IPluginSetting PluginSetting
        {
            get { return _pluginSetting; }
        }

		public bool Load()
		{
			ShellManager.Init(_host);
			_host.Ui.GetMenuButton(
				string.Format("Plugins/{0}/{1}", _pluginInfo.Name, "Import CaiDao's Shells"),
				Import.ImportCaidaoShell);
			_host.Ui.GetMenuButton(
				string.Format("Plugins/{0}/{1}", _pluginInfo.Name, "Import Altman's Shells From db3"),
				Import.ImportAltmanShell);
			_host.Ui.GetMenuButton(
				string.Format("Plugins/{0}/{1}", _pluginInfo.Name, "Import Altman's Shells From Xml"),
				Import.ImportAltmanShellFromXml);
			_host.Ui.GetMenuButton(
				string.Format("Plugins/{0}/{1}", _pluginInfo.Name, "Export Shells to Xml"),
				Export.ExportShell);
			return true;
		}

        public object Show(PluginParameter data)
        {
	        return _userControl = new PanelShellManager(data);
        }

        public void Dispose()
        {
            _userControl =null;
        }
    }
}