using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

using Altman.Plugins;

namespace Altman
{
    public class PluginsImport
    {
        [ImportMany(typeof(IPlugin), RequiredCreationPolicy = CreationPolicy.NonShared, AllowRecomposition = true)]
        public IEnumerable<IPlugin> Plugins = null;
    }
}
