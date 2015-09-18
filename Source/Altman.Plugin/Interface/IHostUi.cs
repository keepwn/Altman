using System;

namespace Altman.Plugin.Interface
{
    public interface IHostUi
    {
        //ui
		/// <summary>
		/// 在状态栏显示消息
		/// </summary>
		/// <param name="msg"></param>
        void ShowMsgInStatusBar(string msg);

        /// <summary>
        /// 在状态栏显示消息，并显示加载图标
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isShowLoadingIcon">是否显示加载图标</param>
        void ShowMsgInStatusBar(string msg, bool isShowLoadingIcon);

		/// <summary>
		/// 在窗体中显示弹窗消息
		/// </summary>
		/// <param name="msg"></param>
        void ShowMsgInAppDialog(string msg);

		/// <summary>
		/// 打开标签页
		/// </summary>
		/// <param name="tabPageName">标签页名</param>
		/// <param name="control">标签页中显示的控件，类型为`Panel`</param>
        void OpenTabPage(string tabPageName, object control);
		
		/// <summary>
		/// 关闭标签页
		/// </summary>
		/// <param name="tabPageName">标签页名</param>
	    void CloseTabPage(string tabPageName);

		/// <summary>
		/// 获取右键菜单
		/// </summary>
		/// <returns>返回类型为由`ContextMenu`封装的object</returns>
        object GetRightMenu();

		/// <summary>
		/// 获取翻译后的文本
		/// </summary>
		/// <param name="strName">翻译字符串</param>
		/// <param name="strDefault">默认字符串</param>
		/// <returns>如果strName不存在，则返回strDefault；如果strName存在，则返回翻译后的字符串</returns>
	    string GetTranslatedText(string strName, string strDefault);

		/// <summary>
		/// 获取菜单按钮
		/// </summary>
		/// <param name="namePath">菜单按钮路径，e.g. Plugins/ShellManager/Test</param>
		/// <param name="actionToDo">绑定点击行为的方法</param>
		/// <param name="create">若不存在是否创建</param>
		/// <returns>
		/// 返回类型为`ButtonMenuItem`封装的object。
		/// 当create=true时，则如果不存在，先创建再返回菜单按钮，否则返回null
		/// </returns>
	    object GetMenuButton(string namePath, Action actionToDo, bool create = true);
    }
}
