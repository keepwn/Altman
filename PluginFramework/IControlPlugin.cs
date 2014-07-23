using System.Windows.Forms;
using Altman.Model;

namespace PluginFramework
{
    public interface IControlPlugin : IPlugin
    {
        UserControl GetUi(Shell data);
    }
}
