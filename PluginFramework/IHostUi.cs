using System.Windows.Forms;

namespace PluginFramework
{
    public interface IHostUi
    {
        //ui
        void ShowMsgInStatusBar(string msg);
        void ShowMsgInAppDialog(string msg);
        void CreateNewTabPage(string tabName, UserControl control);
        ContextMenuStrip GetRightMenu();
    }
}
