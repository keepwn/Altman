using System.ComponentModel.Composition;
using System.Windows.Forms;

using Altman.ModelCore;
using Altman.Plugins;

namespace Plugin_PluginManager
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPlugin))]
    public class Plugin:IFormPlugin
    {
        private Form _form;
        private PluginAttribute _pluginAttribute;
        private IPluginSetting _pluginSetting;
        private IHostService _hostService;

        [ImportingConstructor]
        public Plugin([Import("IHostService")]IHostService service)
        {
            _pluginAttribute = new PluginAttribute();
            _pluginSetting = new PluginSetting();
            _hostService = service;
        }

        public IPluginAttribute PluginAttribute
        {
            get { return _pluginAttribute; }
        }

        public IPluginSetting PluginSetting
        {
            get { return _pluginSetting; }
        }

        public IHostService HostService
        {
            get { return _hostService; }
        }

        public Form GetUi(Shell data)
        {
            return _form = new PluginManager(_hostService, data);
        }

        public void Dispose()
        {
            if (_form != null)
                _form.Dispose();
        }
    }
}
