using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.LogicCore;
using Altman.Setting;

namespace Altman
{
    public partial class Welcome : Form
    {
        private Setting.Setting _setting;
        public Welcome(Setting.Setting setting)
        {
            InitializeComponent();

            this._setting = setting;
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            if (checkBox_noDisplay.Checked)
            {
                _setting.IsShowDisclaimer = false;

                //保存Setting到xml
                InitWorker.SaveSettingToXml(_setting);
                //重新初始化GlobalSetting
                InitWorker.InitGlobalSetting();
            }
            Environment.Exit(0);
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            if (checkBox_noDisplay.Checked)
            {
                _setting.IsShowDisclaimer = false;
                //保存Setting到xml
                InitWorker.SaveSettingToXml(_setting);
                //重新初始化GlobalSetting
                InitWorker.InitGlobalSetting();
            }
            this.Close();
        }
    }
}
