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
        private IHostService _host;
        private Shell _shellData;
        public MyFirstPlugin(IHostService host, Shell data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _host = host;
            _shellData = data;

            ShowShellTypeDataInLable(_shellData);
        }

        private void ShowShellTypeDataInLable(Shell data)
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("[ShellUrl]:" + data.ShellUrl);
            strBuilder.AppendLine("[ShellPwd]:" + data.ShellPwd);
            strBuilder.AppendLine("[ShellType]:" + data.ShellType);
            strBuilder.AppendLine("[ServerCoding]:" + data.ServerCoding);
            strBuilder.AppendLine("[TimeOut]:" + data.TimeOut);
            strBuilder.AppendLine("[ShellExtraString]:" + data.ShellExtraString);

            tbx_shellData.Text = strBuilder.ToString();
        }

        private void btn_showMsgInStatus_Click(object sender, EventArgs e)
        {
            _host.Gui.ShowMsgInStatusBar(tbx_msg.Text);
        }

        private void btn_showMessageBox_Click(object sender, EventArgs e)
        {
            _host.Gui.ShowMsgInAppDialog(tbx_messagBox.Text);
        }

        private void btn_createNewTabPage_Click(object sender, EventArgs e)
        {
            _host.Gui.CreateNewTabPage(tbx_tabPageName.Text,new UserControl1());
        }
    }
}
