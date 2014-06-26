using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface IControlPlugin : IPlugin
    {
        UserControl GetUi(ShellStruct data);
    }
}
