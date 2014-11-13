using System;
using System.ComponentModel.Composition;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Plugin_ShellCmder.Interface;

namespace Plugin_ShellCmder
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
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

        public IHost Host
        {
            get { return _host; }
        }

		public bool Load()
		{
			return true;
		}

        public object Show(PluginParameter data)
        {
            return _userControl = new PanelShellCmder(_host, data);
        }

        public void Dispose()
        {
            _userControl = null;
        }
    }
}
