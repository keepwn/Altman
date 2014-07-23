using System;

namespace PluginFramework
{
    public interface IPlugin : IDisposable
    {
        IPluginInfo PluginInfo { get; }

        IPluginSetting PluginSetting { get; }

        IHost Host { get; }
    }
}
