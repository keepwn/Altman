using System.Windows.Forms;
using Altman.ModelCore;

namespace Altman.Plugins
{
    public interface IControlPlugin : IPlugin
    {
        UserControl GetUi(Shell data);
    }
}
