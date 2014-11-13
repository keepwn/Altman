using System;
using System.Text;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Webshell.Model;
using Eto.Drawing;
using Eto.Forms;

namespace Test_DoNetPlugin
{
    public class DoNetPluginTest : Panel
    {
        private TextArea _tbxShellData;
        private TextBox _tbxMsg;
        private Button _btnShowMsgInStatus;
        private Button _btnShowMessageBox;
        private Button _btnCreateNewTabPage;

        private IHost _host;
		private Shell _shellData;
        public DoNetPluginTest(IHost host, PluginParameter data)
        {
			_host = host;
			_shellData = (Shell)data[0];

            Init();
            ShowShellTypeDataInLable(_shellData);
        }

        void Init()
        {
	        _tbxShellData = new TextArea {Size = new Size(-1, 200)};
            _tbxMsg = new TextBox();
	        _btnShowMsgInStatus = new Button {Text = "Show Msg In Status", Width = 150};
            _btnShowMsgInStatus.Click+=btn_showMsgInStatus_Click;
			_btnShowMessageBox = new Button { Text = "Show Msg In Message", Width = 150 };
            _btnShowMessageBox.Click+=btn_showMessageBox_Click;
			_btnCreateNewTabPage = new Button { Text = "Create New TabPage", Width = 150 };
            _btnCreateNewTabPage.Click+=btn_createNewTabPage_Click;

			// Test
	        var btnTest = new Button {Text = "Test", Width = 150};
			btnTest.Click += btnTest_Click;

	        var layout = new DynamicLayout {Padding = new Padding(10, 10), Size = new Size(10, 10)};
            layout.AddRow(new Label() { Text = "ShellData"});
			layout.AddRow(_tbxShellData);
			layout.AddSeparateRow(new Label() { Text = "Msg", VerticalAlign = VerticalAlign.Middle }, _tbxMsg, null);
	        layout.AddAutoSized(_btnShowMsgInStatus);
	        layout.AddAutoSized(_btnShowMessageBox);
	        layout.AddAutoSized(_btnCreateNewTabPage);
			layout.AddAutoSized(btnTest);
			layout.Add(null);

            this.Content = layout;
        }

		void btnTest_Click(object sender, EventArgs e)
		{
//			_host.Ui.GetMenuButton(
//				string.Format("Plugins/ShellManager/Childs/{0}/Option", (new PluginInfo()).Name),
//				ShowMenu);
		}

		public void ShowMenu()
	    {
			_host.Ui.ShowMsgInAppDialog("Test to add menu");
	    }

		private void ShowShellTypeDataInLable(Shell data)
        {
            var strBuilder = new StringBuilder();
			if (data == null) return;

			strBuilder.AppendLine("[ShellUrl]:" + data.ShellUrl);
			strBuilder.AppendLine("[ShellPwd]:" + data.ShellPwd);
			strBuilder.AppendLine("[ShellType]:" + data.ShellType);
			strBuilder.AppendLine("[ServerCoding]:" + data.ServerCoding);
			strBuilder.AppendLine("[WebCoding]:" + data.WebCoding);
			strBuilder.AppendLine("[ShellExtraString]:" + data.ShellExtraString);
			strBuilder.AppendLine("[TimeOut]:" + data.TimeOut);

            _tbxShellData.Text = strBuilder.ToString();
        }

        private void btn_showMsgInStatus_Click(object sender, EventArgs e)
        {
            _host.Ui.ShowMsgInStatusBar(_tbxMsg.Text);
        }

        private void btn_showMessageBox_Click(object sender, EventArgs e)
        {
            _host.Ui.ShowMsgInAppDialog(_tbxMsg.Text);
        }

        private void btn_createNewTabPage_Click(object sender, EventArgs e)
        {
            _host.Ui.OpenTabPage(_tbxMsg.Text, new NewPanel());
        }
    }
}
