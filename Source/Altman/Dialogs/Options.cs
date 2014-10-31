using System;
using Altman.Forms;
using Altman.Resources;
using Altman.Util.Logic;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Dialogs
{
	public interface IOptions
	{
		Setting SaveSetting();
		void LoadSetting(Setting setting);
	}

	public class Options : Dialog
    {
		private PanelBasicSetting _panelBasicSetting;
        private PanelUserAgentSetting _panelUserAgentSetting;
        private PanelHttpHeaderSetting _panelHttpHeaderSetting;
        private PanelPolicySetting _panelPolicySetting;
        private PanelProxySetting _panelProxySetting;

        private Button _buttonSaveSetting;
        private Button _buttonCancel;
        public Options()
        {
            Init();

			_panelBasicSetting.LoadSetting((Setting)GlobalSetting.Setting);
			_panelUserAgentSetting.LoadSetting((Setting)GlobalSetting.Setting);
			_panelHttpHeaderSetting.LoadSetting((Setting)GlobalSetting.Setting);
			_panelPolicySetting.LoadSetting((Setting)GlobalSetting.Setting);
			_panelProxySetting.LoadSetting((Setting)GlobalSetting.Setting);
        }

        void Init()
        {
			// panelSettings
	        _panelBasicSetting = new PanelBasicSetting();
            _panelUserAgentSetting = new PanelUserAgentSetting();
            _panelHttpHeaderSetting = new PanelHttpHeaderSetting();
            _panelPolicySetting = new PanelPolicySetting();
            _panelProxySetting = new PanelProxySetting();

	        var tabControl = new TabControl();
	        tabControl.Pages.Add(new TabPage {Text = AltStrRes.Basic, Content = _panelBasicSetting});
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.UserAgent, Content = _panelUserAgentSetting });
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.HttpHeader, Content = _panelHttpHeaderSetting });
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.Policy, Content = _panelPolicySetting });
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.Proxy, Content = _panelProxySetting });

            // buttons
	        _buttonSaveSetting = new Button() {Text = AltStrRes.SaveAndExit};
            _buttonSaveSetting.Click += delegate
            {
                SaveAllSetting();
                Close();
            };

	        _buttonCancel = new Button() {Text = AltStrRes.Cancel};
            _buttonCancel.Click += delegate
            {
                Close();
            };

	        var layout = new DynamicLayout {Padding = new Padding(5, 5), Spacing = new Size(5, 5)};
	        layout.Add(tabControl, yscale: true);
            layout.AddSeparateRow(null, _buttonSaveSetting, _buttonCancel);

            Content = layout;
            ClientSize = new Size(400, 400);
			Icon = Icons.AltmanIcon;
	        Title = AltStrRes.Setting;
        }

        private void SaveAllSetting()
        {
	        // new setting
	        var basic = _panelBasicSetting.SaveSetting().BasicSetting;
	        var userAgent = _panelUserAgentSetting.SaveSetting().UserAgentSetting;
	        var httpHeader = _panelHttpHeaderSetting.SaveSetting().HttpHeaderSetting;
	        var policy = _panelPolicySetting.SaveSetting().PolicySetting;
	        var proxy = _panelProxySetting.SaveSetting().ProxySetting;
			
			var setting = new Setting(basic, userAgent, httpHeader, policy, proxy);
            // save Setting to xml
            InitWorker.SaveSettingToXml(AppEnvironment.AppPath, setting);
            // reinit GlobalSetting
			InitWorker.InitGlobalSetting(AppEnvironment.AppPath);
        }
    }
}
