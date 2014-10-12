using System.ComponentModel.Composition;
using PluginFramework;

namespace Plugin_Encoder
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
	        EncoderService.RegisterService(_pluginInfo.Name);
        }

        public IPluginInfo PluginInfo
        {
            get { return _pluginInfo; }
        }

        public IPluginSetting PluginSetting
        {
            get { return _pluginSetting; }
        }

        public object Load(PluginParameter data)
        {
            return _userControl = new Encoder(_host, data);
        }

        public void Dispose()
        {
	        _userControl = null;
        }
    }
}
