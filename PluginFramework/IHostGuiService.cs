using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginFramework
{
    public interface IHostGuiService
    {
        //gui
        void ShowMsgInStatusBar(string msg);
        void ShowMsgInAppDialog(string msg);
        void CreateNewTabPage(string tabName, UserControl control);
    }
}
