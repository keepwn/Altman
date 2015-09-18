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
		}

		private void GetProxySetting()
		{
            _isProxy = _radioButtonList.SelectedIndex;

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
				case 1:
				case 2:
                    _radioButtonList.SelectedIndex = proxy.IsNoOrIeOrCustomProxy;
					break;
				default:
			        _radioButtonList.SelectedIndex = 0;
					break;
			}
            _groupBoxSetting.Enabled = _radioButtonList.SelectedIndex == 2;
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
