using System;

namespace Altman.Plugin.Interface
{
    public interface IHostApp
    {
        Version AppVersion { get; }
        string AppCurrentDir { get; }
        string AppPluginDir { get; }
        string AppPluginConfigDir { get; }
        string AppBinDir { get; }
        string AppLanguageDir { get; }
    }
}
