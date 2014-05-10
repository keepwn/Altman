using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.ModelCore;
using PluginFramework;

namespace MyFirstPlugin
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

        public UserControl GetUi(ShellBasicData data)
        {
             return _userControl = new MyFirstPlugin(_hostService,data);
        }

        public void Dispose()
        {
            _userControl.Dispose();
            MessageBox.Show("Disposed");
        }
    }
}
