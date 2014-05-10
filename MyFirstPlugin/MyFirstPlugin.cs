using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.ModelCore;
using PluginFramework;

namespace MyFirstPlugin
{
    public partial class MyFirstPlugin : UserControl
    {
        private HostService _host;
        private ShellBasicData _shellBasicData;
        public MyFirstPlugin(HostService host,ShellBasicData data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _host = host;
            _shellBasicData = data;

            ShowShellTypeDataInLable(_shellBasicData);
        }

        private void ShowShellTypeDataInLable(ShellBasicData data)
        {
            lbl_shellBasicData.Text = "[ShellUrl]:" + data.ShellUrl + "\n";
            lbl_shellBasicData.Text += "[ShellPwd]:" + data.ShellPwd + "\n";
            lbl_shellBasicData.Text += "[ShellType]:" + data.ShellType + "\n";
            lbl_shellBasicData.Text += "[ShellCoding]:" + data.ShellCoding + "\n";
            lbl_shellBasicData.Text += "[ShellTimeOut]:" + data.ShellTimeOut + "\n";    
        }

        private void btn_showMsgInStatus_Click(object sender, EventArgs e)
        {
            _host.ShowMsgInStatusBar(tbx_msg.Text);
        }

        private void btn_showMessageBox_Click(object sender, EventArgs e)
        {
            _host.ShowMsgInAppDialog(tbx_messagBox.Text);
        }

        private void btn_createNewTabPage_Click(object sender, EventArgs e)
        {
            _host.CreateNewTabPage(tbx_tabPageName.Text,new UserControl1());
        }
    }
}
