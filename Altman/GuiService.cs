using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Altman.UI;
using Altman.Plugins;

namespace Altman
{
    public class GuiService : IHostGuiService
    {
        private FormMain _mainForm;
        public GuiService(FormMain mainForm)
        {
            _mainForm = mainForm;
        }

        public void ShowMsgInAppDialog(string msg)
        {
            MessageBox.Show(msg);
        }
        public void ShowMsgInStatusBar(string msg)
        {
            _mainForm.MsgInStatusBar = msg;
        }
        public void CreateNewTabPage(string tabPageName, UserControl control)
        {
            TabCore.CreateNewTabPage(tabPageName, control);
        }
    }
}
