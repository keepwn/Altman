using System.Windows.Forms;
using Altman.ModelCore;

namespace Altman.Plugins
{
    public interface IFormPlugin : IPlugin
    {
        Form GetUi(Shell data);
    }
}
