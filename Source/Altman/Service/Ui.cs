using System;
using System.Linq;
using Altman.Forms;
using Altman.Plugin.Interface;
using Altman.Resources;
using Eto.Forms;

namespace Altman.Service
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
		    Application.Instance.Invoke(() =>
		    {
		        MessageBox.Show(_mainForm, msg, "Message");
		    });
		}

		public void ShowMsgInStatusBar(string msg)
		{
            Application.Instance.Invoke(() =>
            {
                _mainForm.MsgInStatusBar = msg;
            });			
		}

        public void ShowMsgInStatusBar(string msg, bool isShowLoadingIcon)
        {
            Application.Instance.Invoke(() =>
            {
                _mainForm.MsgInStatusBar = msg;
                if (isShowLoadingIcon)
                    _mainForm.ShowLoadingInStatusBar();
                else
                    _mainForm.HideLoadingInStatusBar();
            });
        }

		public void OpenTabPage(string tabPageName, object control)
		{
			_mainForm.CreateTabPage(tabPageName, control);
		}

		public void CloseTabPage(string tabPageName)
		{
			_mainForm.CloseTabPage(tabPageName);
		}

		public object GetRightMenu()
		{
			return _mainForm.RightMenu;
		}

		public string GetTranslatedText(string strName, string strDefault)
		{
			return AltStrRes.GetString(strName, strDefault);
		}

		public object GetMenuButton(string namePath, Action actionToDo, bool create = true)
		{
			var paths = namePath.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (!paths.Any())
			{
				throw new Exception("NamePath should not be empty.");
			}
			var tmp = GetSubmenu(_mainForm.Menu.Items, paths[0], create);
			if (tmp == null) return null;
			paths.RemoveAt(0);

			var index = 0;
			while (index < paths.Count)
			{
				var tmpName = paths[index];
				var find = GetSubmenu(tmp.Items, tmpName, create);
				if (find == null) return null;
				tmp = find;
				index++;
			}
			tmp.Click += delegate { actionToDo(); };// Should call `GetMenuButton` in `Load()`, otherwise maybe be registered repeatedly.
			return tmp;
		}

		private ButtonMenuItem GetSubmenu(MenuItemCollection items, string submenuText, bool create = true)
		{
			var submenu = items.OfType<ButtonMenuItem>().FirstOrDefault(r => r.ID == submenuText);
			if (submenu == null && create)
			{
				submenu = new ButtonMenuItem
				{
					ID = submenuText,
					Text = submenuText, 
					Order = 10
				};
				items.Add(submenu);
			}
			return submenu;
		}
	}
}
