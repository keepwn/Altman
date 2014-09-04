using Altman.Model;

namespace PluginFramework
{
    public interface IControlPlugin : IPlugin
    {
        object GetUi(Shell data);
    }
}
