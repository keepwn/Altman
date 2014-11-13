using System.ComponentModel.Composition;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Test_DoNetPlugin
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
			_host.Ui.GetMenuButton(
				string.Format("Plugins/ShellManager/Childs/{0}/Option", _pluginInfo.Name),
				() => new PanelOption().ShowModal());
			return true;
		}

	    public object Show(PluginParameter data)
	    {
			return _userControl = new DoNetPluginTest(_host, data);
	    }

        public void Dispose()
        {
            _userControl = null;
        }
    }
}
