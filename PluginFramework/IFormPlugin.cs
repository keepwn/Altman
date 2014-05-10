using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginFramework
{
    public interface IFormPlugin : IPlugin
    {
        Form Content { get; }
    }
}
