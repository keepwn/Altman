using System;
using System.Windows.Forms;

namespace Altman.Forms
{
    public partial class ControlProxySetting : UserControl
    {
        private int _isProxy;
        private string _proxyAddr;
        private string _proxyPort;
        private string _proxyUser;
        private string _proxyPasswd;
        private string _proxyDomain;

        public ControlProxySetting()
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            radioButton_noProxy.Checked = true;
        }

        private void radioButton_noProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_noProxy.Checked)
            {
                foreach (Control c in panel_customProxy.Controls)
                {
                    c.Enabled = false;
                }
            }
        }
        private void radioButton_ieProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_ieProxy.Checked)
            {
                foreach (Control c in panel_customProxy.Controls)
                {
                    c.Enabled = false;
                }
            }
        }
        private void radioButton_customProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_customProxy.Checked)
            {
                foreach (Control c in panel_customProxy.Controls)
                {
                    c.Enabled = true;
                }
            }
        }

        private void GetProxySetting()
        {
            if (radioButton_noProxy.Checked)
            {
                _isProxy = 0;
            }
            else if (radioButton_ieProxy.Checked)
            {
                _isProxy = 1;
            }
            else if (radioButton_customProxy.Checked)
            {
                _isProxy = 2;
            }
            else
            {
                _isProxy = 0;
            }
            _proxyAddr = textBox_proxyAddr.Text.Trim();
            _proxyPort = textBox_proxyPort.Text.Trim();
            _proxyUser = textBox_proxyUser.Text.Trim();
            _proxyPasswd = textBox_proxyPasswd.Text.Trim();
            _proxyDomain = textBox_proxyDomain.Text.Trim();
        }
        public Setting.Setting.ProxyStruct SaveProxySetting()
        {
            //获取当前窗口配置
            GetProxySetting();
            //组合为ProxyStruct
            Setting.Setting.ProxyStruct proxy = new Setting.Setting.ProxyStruct();
            proxy.IsNoOrIeOrCustomProxy = _isProxy;
            proxy.ProxyAddr = _proxyAddr;
            proxy.ProxyPort = _proxyPort;
            proxy.ProxyUser = _proxyUser;
            proxy.ProxyPassword = _proxyPasswd;
            proxy.ProxyDomain = _proxyDomain;
            return proxy;
        }
        public void LoadProxySetting(Setting.Setting.ProxyStruct proxy)
        {
            if (proxy.IsNoOrIeOrCustomProxy == 0)
            {
                radioButton_noProxy.Checked = true;
            }
            else if (proxy.IsNoOrIeOrCustomProxy == 1)
            {
                radioButton_ieProxy.Checked = true;
            }
            else if (proxy.IsNoOrIeOrCustomProxy == 2)
            {
                radioButton_customProxy.Checked = true;
            }
            else
            {
                radioButton_noProxy.Checked = true;
            }
            textBox_proxyAddr.Text = proxy.ProxyAddr;
            textBox_proxyPort.Text = proxy.ProxyPort;
            textBox_proxyUser.Text = proxy.ProxyUser;
            textBox_proxyPasswd.Text = proxy.ProxyPassword;
            textBox_proxyDomain.Text = proxy.ProxyDomain;
        }
    }
}
