using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
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
