using System.ComponentModel.Composition;
using System.Windows.Forms;
using Altman.Model;
using PluginFramework;

namespace Plugin_PluginManager
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPlugin))]
    public class Plugin:IFormPlugin
    {
        private Form _form;
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

        public IHost Host
        {
            get { return _host; }
        }

        public Form GetUi(Shell data)
        {
            return _form = new PluginManager(_host, data);
        }

        public void Dispose()
        {
            if (_form != null)
                _form.Dispose();
        }
    }
}
