using Altman.Dialogs;
using Altman.Resources;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelProxySetting : Panel, IOptions
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

		public void LoadSetting(Setting setting)
		{
			var proxy = setting.ProxySetting;
			switch (proxy.IsNoOrIeOrCustomProxy)
			{
				case 0:
					_radioButtonNoProxy.Checked = true;
					break;
				case 1:
					_radioButtonIeProxy.Checked = true;
					break;
				case 2:
					_radioButtonCustomProxy.Checked = true;
					break;
				default:
					_radioButtonNoProxy.Checked = true;
					break;
			}
			_textBoxProxyAddr.Text = proxy.ProxyAddr;
			_textBoxProxyPort.Text = proxy.ProxyPort;
			_textBoxProxyUser.Text = proxy.ProxyUser;
			_textBoxProxyPasswd.Text = proxy.ProxyPassword;
			_textBoxProxyDomain.Text = proxy.ProxyDomain;
		}

        public Setting SaveSetting()
        {
            //获取当前窗口配置
            GetProxySetting();
            var proxy = new Setting.ProxyStruct
            {
	            IsNoOrIeOrCustomProxy = _isProxy,
	            ProxyAddr = _proxyAddr,
	            ProxyPort = _proxyPort,
	            ProxyUser = _proxyUser,
	            ProxyPassword = _proxyPasswd,
	            ProxyDomain = _proxyDomain
            };

			var setting = new Setting {ProxySetting = proxy};
	        return setting;
        }
    }
}
