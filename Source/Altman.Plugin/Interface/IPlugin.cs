using System;

namespace Altman.Plugin
{
    public interface IPlugin : IDisposable
    {
        IPluginInfo PluginInfo { get; }

        IPluginSetting PluginSetting { get; }
    }
}
