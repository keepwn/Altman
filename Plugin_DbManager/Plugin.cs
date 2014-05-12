using System.ComponentModel.Composition;
using System.Windows.Forms;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_DbManager
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPlugin))]
    public class Plugin : IPlugin
    {
        private UserControl _userControl;
        private PluginAttribute _pluginAttribute;
        private HostService _hostService;

        public Plugin()
        {
            _pluginAttribute = new PluginAttribute();
            _hostService = new HostService();
        }

        public IPluginAttribute PluginAttribute
        {
            get { return _pluginAttribute; }
        }

        public IHostService HostService
        {
            get { return _hostService; }
        }

        public UserControl GetUi(ShellStruct data)
        {
            return _userControl = new DbManagerControl(_hostService, data);
        }

        public void Dispose()
        {
            _userControl.Dispose();
            MessageBox.Show("Disposed");
        }
    }
}
