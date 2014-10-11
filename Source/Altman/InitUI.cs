using System;
using System.Collections.Generic;
using System.Linq;
using Altman.Desktop.Dialogs;
using Altman.Desktop.Forms;
using Altman.Logic;
using Altman.Setting;
using Eto.Forms;

namespace Altman.Desktop
{
    internal class InitUi
    {
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
                Setting.Setting tmp = (Setting.Setting)GlobalSetting.Setting;
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
