using System.ComponentModel.Composition;

using Altman.Model;
using PluginFramework;
using Plugin_FileManager.Interface;

namespace Plugin_FileManager
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

        public object GetUi(Shell data)
        {
			return _userControl = new FileManagerSection(_host, data);
        }

        public void Dispose()
        {
            _userControl = null;
        }
    }
}
