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
        private ShellStruct _shellData;
        public MyFirstPlugin(HostService host, ShellStruct data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _host = host;
            _shellData = data;

            ShowShellTypeDataInLable(_shellData);
        }

        private void ShowShellTypeDataInLable(ShellStruct data)
        {
            lbl_shellData.Text = "[ShellUrl]:" + data.ShellUrl + "\n";
            lbl_shellData.Text += "[ShellPwd]:" + data.ShellPwd + "\n";
            lbl_shellData.Text += "[ShellType]:" + data.ShellType + "\n";
            lbl_shellData.Text += "[ServerCoding]:" + data.ServerCoding + "\n";
            lbl_shellData.Text += "[TimeOut]:" + data.TimeOut+ "\n";
            lbl_shellData.Text += "[ShellExtraSetting]:" + data.ShellExtraSetting + "\n";
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
