using System;

namespace Altman.Plugin.Interface
{
    public interface IHostUi
    {
        //ui
        void ShowMsgInStatusBar(string msg);
        void ShowMsgInAppDialog(string msg);
        void OpenTabPage(string tabPageName, object control);
	    void CloseTabPage(string tabPageName);
        object GetRightMenu();
	    string GetTranslatedText(string strName, string strDefault);
	    object GetMenuButton(string namePath, Action actionToDo, bool create = true);
    }
}
