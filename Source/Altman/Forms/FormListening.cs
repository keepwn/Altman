using System;
using Altman.Common.AltEventArgs;
using Altman.Desktop.Web;
using Eto.Forms;

namespace Altman.Desktop.Forms
{
    public class FormListening : Form
    {
        private TextBox _textBox_ListenData;
        private CheckBox _checkBox_Capture;
        private Button _button_Clear;

        void Init()
        {
            //_textBox_ListenData
            _textBox_ListenData = new TextBox();

            //_button_Clear
            _button_Clear = new Button()
            {
                Text = "Clear",
            };
            _button_Clear.Click += delegate
            {
                _textBox_ListenData.Text = "";
            };

            //_checkBox_Capture
            _checkBox_Capture = new CheckBox()
            {
                Text = "Capture",
            };
            _checkBox_Capture.CheckedChanged += delegate
            {
                if (_checkBox_Capture.Checked == true)
                {
                    StartToProxy();
                }
                else
                {
                    StopToProxy();
                }
            };
        }


        public FormListening()
        {
            Init();
            ProxyServer.ProxyReceiveToDo += ProxyServer_ProxyReceiveToDo;
        }

        private void StartToProxy()
        {
            ProxyServer.Server.DumpHeaders = true;
            ProxyServer.Server.DumpPostData = true;
            ProxyServer.Server.DumpResponseData = true;

            ProxyServer.Server.Start();
        }
        private void StopToProxy()
        {
            ProxyServer.Server.Stop();
        }
        private void ProxyServer_ProxyReceiveToDo(object sender, ProxyReceiveEventArgs e)
        {
            AppendListenData(e.Data);
        }

        /// <summary>
        /// 用异步的方式打印监听数据（解决多线程调用UI的问题）
        /// </summary>
        /// <param name="data"></param>
        private void AppendListenData(string data)
        {
            //等待异步
            Application.Instance.AsyncInvoke(() =>
            {
                _textBox_ListenData.Text += data;
                _textBox_ListenData.Text += Environment.NewLine;
            });
        }

    }
}
