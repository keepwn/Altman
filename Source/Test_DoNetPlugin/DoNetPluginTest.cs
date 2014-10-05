using System;
using System.Text;
using Altman.Model;
using Eto.Forms;
using PluginFramework;

namespace Test_DoNetPlugin
{
    public class DoNetPluginTest : Panel
    {
        private TextArea tbx_shellData;
        private TextBox tbx_msg;
        private Button btn_showMsgInStatus;
        private Button btn_showMessageBox;
        private Button btn_createNewTabPage;

        private IHost _host;
        private Shell _shellData;
        public DoNetPluginTest(IHost host, Shell data)
        {
            Init();

            _host = host;
            _shellData = data;

            ShowShellTypeDataInLable(_shellData);
        }

        void Init()
        {
            tbx_shellData = new TextArea();
            tbx_msg = new TextBox();
            btn_showMsgInStatus = new Button() {Text = "Show Msg In Status"};
            btn_showMsgInStatus.Click+=btn_showMsgInStatus_Click;
            btn_showMessageBox = new Button() {Text = "Show Msg In Message"};
            btn_showMessageBox.Click+=btn_showMessageBox_Click;
            btn_createNewTabPage = new Button(){Text = "Create New TabPage"};
            btn_createNewTabPage.Click+=btn_createNewTabPage_Click;

            var layout = new DynamicLayout();
            layout.AddSeparateRow(new Label() { Text = "ShellData"}, tbx_shellData);
            layout.AddSeparateRow(new Label(){Text="Msg"},tbx_msg,null);
            layout.AddSeparateRow(btn_showMsgInStatus, btn_showMessageBox, btn_createNewTabPage,null);
            layout.AddRow(null);

            this.Content = layout;
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
            _host.Ui.ShowMsgInStatusBar(tbx_msg.Text);
        }

        private void btn_showMessageBox_Click(object sender, EventArgs e)
        {
            _host.Ui.ShowMsgInAppDialog(tbx_msg.Text);
        }

        private void btn_createNewTabPage_Click(object sender, EventArgs e)
        {
            _host.Ui.CreateNewTabPage(tbx_msg.Text, new UserControl1());
        }
    }
}
