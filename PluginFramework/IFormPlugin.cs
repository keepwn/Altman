using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface IFormPlugin : IPlugin
    {
        Form GetUi(Shell data);
    }
}
