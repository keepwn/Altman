using System.Collections.Generic;
using System.Xml;
using Altman.Desktop.Forms;
using Altman.Web;
using PluginFramework;

namespace Altman.Desktop.Service
{
    public class Core : IHostCore
    {
        private FormMain _mainForm;
        public Core(FormMain mainForm)
        {
            _mainForm = mainForm;
        }
    }
}
