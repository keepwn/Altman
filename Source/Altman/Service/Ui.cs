using Altman.Desktop.Forms;
using Altman.Desktop.Resources;
using Eto.Forms;
using PluginFramework;

namespace Altman.Desktop.Service
{
    public class Ui : IHostUi
    {
        private FormMain _mainForm;
        public Ui(FormMain mainForm)
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
        public void CreateNewTabPage(string tabPageName, object control)
        {
            _mainForm.CreateNewTabPage(tabPageName, control);
        }
        public object GetRightMenu()
        {
            return _mainForm.RightMenu;
        }

	    public string GetTranslatedString(string strName, string strDefault)
	    {
		    return AltStrRes.GetString(strName, strDefault);
	    }
    }
}
