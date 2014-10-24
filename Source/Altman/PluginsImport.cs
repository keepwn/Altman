using System.Collections.Generic;
using System.ComponentModel.Composition;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Altman
{
    public class PluginsImport
    {
		[ImportMany(typeof(IPlugin), AllowRecomposition = true)]
        public IEnumerable<IPlugin> Plugins = null;
    }
}
