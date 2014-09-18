using System;
using System.ComponentModel.Composition;
using Altman.Model;
using PluginFramework;

namespace Plugin_ShellManager
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPlugin))]
    public class Plugin : IControlPlugin
    {
        private object _userControl;
        private PluginInfo _pluginInfo;
        private IPluginSetting _pluginSetting;
        private IHost _host;

        [ImportingConstructor]
        public Plugin([Import("IHost")]IHost service)
        {
            _pluginInfo = new PluginInfo();
            _pluginSetting = new PluginSetting();
            _host = service;
        }

        public IPluginInfo PluginInfo
        {
            get { return _pluginInfo; }
        }

        public IPluginSetting PluginSetting
        {
            get { return _pluginSetting; }
        }

		public EventHandler PluginLoad { get; set; }
        public object GetUi(Shell data)
        {
			var control = new ShellManagerControl(_host, data);
			control.Load += (sender, e) =>
	        {
				if (PluginLoad != null)
				{
					PluginLoad(sender, e);
				}
	        };
	        return _userControl = control;
        }

        public void Dispose()
        {
            _userControl =null;
        }
    }
}