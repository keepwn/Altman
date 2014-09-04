using Altman.Model;

namespace PluginFramework
{
    public interface IFormPlugin : IPlugin
    {
        object GetUi(Shell data);
    }
}
