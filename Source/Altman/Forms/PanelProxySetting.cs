using Altman.Resources;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelProxySetting : Panel
    {
		private int _isProxy;
		private string _proxyAddr;
		private string _proxyPort;
		private string _proxyUser;
		private string _proxyPasswd;
		private string _proxyDomain;

		public PanelProxySetting()
		{
			Init();

			_radioButtonNoProxy.Checked = true;
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
