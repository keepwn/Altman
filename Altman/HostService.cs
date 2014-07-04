using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PluginFramework;

namespace Altman
{
    public class HostService : IHostService
    {
        public IHostCoreService Core
        {
            get { return _core; }
        }

        public IHostGuiService Gui
        {
            get { return _gui; }
        }

        public IHostDbService Db
        {
            get { return _db; }
        }


        private IHostCoreService _core;
        private IHostGuiService _gui;
        private IHostDbService _db;

        public HostService(FormMain mainForm)
        {
            _core = new CoreService(mainForm);
            _gui = new GuiService(mainForm);
            _db = new DbService();
        }
    }
}
