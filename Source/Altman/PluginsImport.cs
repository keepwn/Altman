using System.Collections.Generic;
using System.ComponentModel.Composition;
using PluginFramework;

namespace Altman.Desktop
{
    public class PluginsImport
    {
        [ImportMany(typeof(IPlugin), RequiredCreationPolicy = CreationPolicy.NonShared, AllowRecomposition = true)]
        public IEnumerable<IPlugin> Plugins = null;
    }
}
