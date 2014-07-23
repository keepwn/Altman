using System;
using System.Windows.Forms;
using Altman.Common.AltEventArgs;
using Altman.Web;

namespace Altman.Forms
{
    public partial class FormListening : Form
    {
        public FormListening()
        {
            InitializeComponent();
            ProxyServer.ProxyReceiveToDo += ProxyServer_ProxyReceiveToDo;
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_ListenData.Clear();
        }

        private void checkBox_Capture_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Capture.Checked)
            {
                StartToProxy();
            }
            else
            {
                StopToProxy();
            }
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

        private delegate void AppendListenDataClient(string data);//代理
        /// <summary>
        /// 用异步的方式打印监听数据（解决多线程调用UI的问题）
        /// </summary>
        /// <param name="data"></param>
        private void AppendListenData(string data)
        {
            //等待异步
            if (this.InvokeRequired)
            {
                AppendListenDataClient c = new AppendListenDataClient(AppendListenData);
                this.Invoke(c, new object[1] { data });//通过代理调用刷新方法
            }
            else
            {
                textBox_ListenData.AppendText(data);
                textBox_ListenData.AppendText(Environment.NewLine);
            }
        }

    }
}
