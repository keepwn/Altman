namespace Altman.Plugin.Interface
{
    public interface IHostUi
    {
        //ui
        void ShowMsgInStatusBar(string msg);
        void ShowMsgInAppDialog(string msg);
        void CreateNewTabPage(string tabName, object control);
        object GetRightMenu();
	    string GetTranslatedString(string strName, string strDefault);
    }
}
