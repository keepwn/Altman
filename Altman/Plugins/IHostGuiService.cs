using System.Windows.Forms;

namespace Altman.Plugins
{
    public interface IHostGuiService
    {
        //gui
        void ShowMsgInStatusBar(string msg);
        void ShowMsgInAppDialog(string msg);
        void CreateNewTabPage(string tabName, UserControl control);
    }
}
