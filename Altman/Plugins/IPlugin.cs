using System;

namespace Altman.Plugins
{
    public interface IPlugin : IDisposable
    {
        IPluginAttribute PluginAttribute { get; }

        IPluginSetting PluginSetting { get; }

        IHostService HostService { get; }
    }
}
