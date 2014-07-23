using System.Windows.Forms;
using Altman.Model;

namespace PluginFramework
{
    public interface IFormPlugin : IPlugin
    {
        Form GetUi(Shell data);
    }
}
