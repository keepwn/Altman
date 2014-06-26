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
        public ICoreInHostService Core
        {
            get { return _core; }
        }

        public IGuiInHostService Gui
        {
            get { return _gui; }
        }

        public IDbInHostService Db
        {
            get { return _db; }
        }


        private ICoreInHostService _core;
        private IGuiInHostService _gui;
        private IDbInHostService _db;

        public HostService(FormMain mainForm)
        {
            _core = new CoreService(mainForm);
            _gui = new GuiService(mainForm);
            _db = new DbService();
        }
    }
}
