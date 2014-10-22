using Altman.Resources;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public class PanelProxySetting : Panel
    {
        private int _isProxy;
        private string _proxyAddr;
        private string _proxyPort;
        private string _proxyUser;
        private string _proxyPasswd;
        private string _proxyDomain;

        private RadioButton _radioButtonNoProxy;
        private RadioButton _radioButtonIeProxy;
        private RadioButton _radioButtonCustomProxy;
        private TextBox _textBoxProxyAddr;
        private TextBox _textBoxProxyPort;
        private TextBox _textBoxProxyUser;
        private TextBox _textBoxProxyPasswd;
        private TextBox _textBoxProxyDomain;

        private GroupBox group;
        public PanelProxySetting()
        {
            Init();

            _radioButtonNoProxy.Checked = true;
         }

        void Init()
        {
            _textBoxProxyAddr = new TextBox();
            _textBoxProxyPort = new TextBox();
            _textBoxProxyUser = new TextBox();
            _textBoxProxyPasswd = new TextBox();
            _textBoxProxyDomain = new TextBox();

            //radioButton_noProxy
	        _radioButtonNoProxy = new RadioButton() {Text = AltStrRes.NotUseProxy};
            _radioButtonNoProxy.CheckedChanged += delegate
            {
                if (_radioButtonNoProxy.Checked)
                {
                    group.Enabled = false;
                }
            };

            //radioButton_ieProxy
	        _radioButtonIeProxy = new RadioButton(_radioButtonNoProxy) {Text = AltStrRes.UseSystemProxySetting};
            _radioButtonIeProxy.CheckedChanged += delegate
            {
                if (_radioButtonIeProxy.Checked)
                {
                    group.Enabled = false;
                }
            };

            //radioButton_customProxy
			_radioButtonCustomProxy = new RadioButton(_radioButtonNoProxy) { Text = AltStrRes.UseCustomProxySetting };
            _radioButtonCustomProxy.CheckedChanged += delegate
            {
                if (_radioButtonCustomProxy.Checked)
                {
                    group.Enabled = true;
                }
            };


            var layout = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(10, 10) };

            layout.AddRow(_radioButtonNoProxy);
            layout.AddRow(_radioButtonIeProxy);
            layout.AddRow(_radioButtonCustomProxy);


            var layout2 = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(5, 5) };
            layout2.AddRow(new Label { Text = "Server: " }, TableLayout.AutoSized(_textBoxProxyAddr));
            layout2.AddRow(new Label { Text = "Port: " }, TableLayout.AutoSized(_textBoxProxyPort));
            layout2.AddRow(new Label { Text = "User: " }, TableLayout.AutoSized(_textBoxProxyUser));
            layout2.AddRow(new Label { Text = "Passwd: " },TableLayout.AutoSized(_textBoxProxyPasswd));
            layout2.AddRow(new Label { Text = "Domain: " }, TableLayout.AutoSized(_textBoxProxyDomain));

            group = new GroupBox()
            {
                Text = AltStrRes.Setting,
                Content = layout2
            };

            layout.AddRow(group);
            layout.AddRow(null);

            this.Content = layout;
        }

        private void GetProxySetting()
        {
            if (_radioButtonNoProxy.Checked)
            {
                _isProxy = 0;
            }
            else if (_radioButtonIeProxy.Checked)
            {
                _isProxy = 1;
            }
            else if (_radioButtonCustomProxy.Checked)
            {
                _isProxy = 2;
            }
            else
            {
                _isProxy = 0;
            }
            _proxyAddr = _textBoxProxyAddr.Text.Trim();
            _proxyPort = _textBoxProxyPort.Text.Trim();
            _proxyUser = _textBoxProxyUser.Text.Trim();
            _proxyPasswd = _textBoxProxyPasswd.Text.Trim();
            _proxyDomain = _textBoxProxyDomain.Text.Trim();
        }
        public Setting.ProxyStruct SaveProxySetting()
        {
            //获取当前窗口配置
            GetProxySetting();
            //组合为ProxyStruct
            var proxy = new Setting.ProxyStruct();
            proxy.IsNoOrIeOrCustomProxy = _isProxy;
            proxy.ProxyAddr = _proxyAddr;
            proxy.ProxyPort = _proxyPort;
            proxy.ProxyUser = _proxyUser;
            proxy.ProxyPassword = _proxyPasswd;
            proxy.ProxyDomain = _proxyDomain;
            return proxy;
        }
        public void LoadProxySetting(Setting.ProxyStruct proxy)
        {
            if (proxy.IsNoOrIeOrCustomProxy == 0)
            {
                _radioButtonNoProxy.Checked = true;
            }
            else if (proxy.IsNoOrIeOrCustomProxy == 1)
            {
                _radioButtonIeProxy.Checked = true;
            }
            else if (proxy.IsNoOrIeOrCustomProxy == 2)
            {
                _radioButtonCustomProxy.Checked = true;
            }
            else
            {
                _radioButtonNoProxy.Checked = true;
            }
            _textBoxProxyAddr.Text = proxy.ProxyAddr;
            _textBoxProxyPort.Text = proxy.ProxyPort;
            _textBoxProxyUser.Text = proxy.ProxyUser;
            _textBoxProxyPasswd.Text = proxy.ProxyPassword;
            _textBoxProxyDomain.Text = proxy.ProxyDomain;
        }
    }
}
