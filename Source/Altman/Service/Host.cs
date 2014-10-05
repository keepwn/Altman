using System.ComponentModel.Composition;
using Altman.Desktop.Forms;
using Eto.Forms;
using PluginFramework;

namespace Altman.Desktop.Service
{
    public class Host : IHost
    {
        public IHostApp App
        {
            get { return _app; }
        }
        public IHostCore Core
        {
            get { return _core; }
        }

        public IHostUi Ui
        {
            get { return _ui; }
        }

        public IHostDb Database
        {
            get { return _database; }
        }

        private IHostApp _app;
        private IHostCore _core;
        private IHostUi _ui;
        private IHostDb _database;

		public Host(FormMain mainForm)
        {
            _app = new App();
            _core = new Core(mainForm);
            _ui = new Ui(mainForm);
            _database = new Db();
        }
    }
}
