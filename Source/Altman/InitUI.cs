using System;
using Altman.Dialogs;
using Altman.Util.Logic;
using Altman.Util.Setting;
using Eto.Forms;

namespace Altman
{
    internal class InitUi
    {
	    public static void InitCustomShellType(string customShellTypePath)
	    {
		    try
		    {
			    //初始化CustomShellType
				Altman.Webshell.InitWorker.RegisterCustomShellType(customShellTypePath);
		    }
		    catch (Exception ex)
		    {
			    //MessageBox.Show(ex.Message);
		    }
	    }

	    public static void InitGlobalSetting(string settingXmlPath)
        {
            try
            {
                //初始化GlobalSetting
				InitWorker.InitGlobalSetting(settingXmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InitWelcome()
        {
            try
            {
                //获取setting
                Setting tmp = (Setting)GlobalSetting.Setting;
                if (tmp.IsShowDisclaimer)
                {
                    var w = new Welcome(tmp);
                    w.ShowModal(Application.Instance.MainForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
