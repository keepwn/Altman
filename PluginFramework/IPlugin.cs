using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface IPlugin : IDisposable
    {

        IPluginAttribute PluginAttribute { get; }
        IHostService HostService { get; }
        UserControl GetUi(ShellBasicData data);
    }
}
