using System.Collections.Generic;
using System.ComponentModel.Composition;
using Altman.Plugin;

namespace Altman
{
    public class PluginsImport
    {
		[ImportMany(typeof(IPlugin), AllowRecomposition = true)]
        public IEnumerable<IPlugin> Plugins = null;
    }
}
