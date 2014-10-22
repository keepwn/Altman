using System;
using Altman.Forms;
using Altman.Resources;
using Altman.Util.Logic;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Dialogs
{
	public class DialogSetting : Dialog
    {
        private readonly Func<Setting.UserAgentStruct> _saveUserAgentSetting;
        private readonly Action<Setting.UserAgentStruct> _loadUserAgentSetting;
        private readonly Func<Setting.HttpHeaderStruct> _saveHttpHeaderSetting;
        private readonly Action<Setting.HttpHeaderStruct> _loadHttpHeaderSetting;
        private readonly Func<Setting.PolicyStruct> _savePolicySetting;
        private readonly Action<Setting.PolicyStruct> _loadPolicySetting;
        private readonly Func<Setting.ProxyStruct> _saveProxySetting;
        private readonly Action<Setting.ProxyStruct> _loadProxySetting;

        private PanelUserAgentSetting _panelUserAgentSetting;
        private PanelRequestHeaderSetting _panelRequestHeaderSetting;
        private PanelPolicySetting _panelPolicySetting;
        private PanelProxySetting _panelProxySetting;

        private Button button_SaveSetting;
        private Button button_cancel;
        public DialogSetting()
        {
            Init();
            _saveUserAgentSetting = _panelUserAgentSetting.SaveUserAgnetSetting;
            _loadUserAgentSetting = _panelUserAgentSetting.LoadUserAgnetSetting;

            _saveHttpHeaderSetting = _panelRequestHeaderSetting.SaveHttpHeaderSetting;
            _loadHttpHeaderSetting = _panelRequestHeaderSetting.LoadHttpHeaderSetting;

            _savePolicySetting = _panelPolicySetting.SavePolicySetting;
            _loadPolicySetting = _panelPolicySetting.LoadPolicySetting;

            _saveProxySetting = _panelProxySetting.SaveProxySetting;
            _loadProxySetting = _panelProxySetting.LoadProxySetting;

            _loadUserAgentSetting(((Setting)GlobalSetting.Setting).GetUserAgentStruct);
            _loadHttpHeaderSetting(((Setting)GlobalSetting.Setting).GetHttpHeaderStruct);
            _loadPolicySetting(((Setting) GlobalSetting.Setting).GetPolicyStruct);
            _loadProxySetting(((Setting)GlobalSetting.Setting).GetProxyStruct);
        }

        void Init()
        {
            //controlUserAgentSetting
            _panelUserAgentSetting = new PanelUserAgentSetting();
            //controlRequestHeaderSetting
            _panelRequestHeaderSetting = new PanelRequestHeaderSetting();
            //controlPolicySetting
            _panelPolicySetting = new PanelPolicySetting();
            //controlProxySetting
            _panelProxySetting = new PanelProxySetting();

            var tabControl = new TabControl()
            {
                Size = new Size(350,320)
            };

			tabControl.Pages.Add(new TabPage { Text = AltStrRes.UserAgent, Content = _panelUserAgentSetting });
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.HttpHeader, Content = _panelRequestHeaderSetting });
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.Policy, Content = _panelPolicySetting });
			tabControl.Pages.Add(new TabPage { Text = AltStrRes.Proxy, Content = _panelProxySetting });

            //button
	        button_SaveSetting = new Button() {Text = AltStrRes.SaveAndExit};
            button_SaveSetting.Click += delegate
            {
                SaveAllSetting();
                Close();
            };

	        button_cancel = new Button() {Text = AltStrRes.Cancel};
            button_cancel.Click += delegate
            {
                Close();
            };

            var layout = new DynamicLayout{Padding=new Padding(0,10),Spacing=new Size(5,5)};
            layout.AddRow(tabControl);
            layout.AddSeparateRow(null, button_SaveSetting, button_cancel);
            layout.Add(null);

            Content = layout;
            Size = new Size(400, 400);       
			Icon = Icons.AltmanIcon;
	        Title = AltStrRes.Setting;
        }

        private void SaveAllSetting()
        {
            Setting.UserAgentStruct userAgent=new Setting.UserAgentStruct();
            Setting.HttpHeaderStruct httpHeader=new Setting.HttpHeaderStruct();
            Setting.PolicyStruct policy=new Setting.PolicyStruct();
            Setting.ProxyStruct proxy=new Setting.ProxyStruct();
            //生成userAgent
            userAgent = _saveUserAgentSetting();
            //生成httpHeader
            httpHeader = _saveHttpHeaderSetting();
            //生成policy
            policy = _savePolicySetting();
            //生成proxy
            proxy = _saveProxySetting();
            //生成Setting
            Setting setting = new Setting(userAgent, httpHeader, policy, proxy);
            //保存Setting到xml
            InitWorker.SaveSettingToXml(AppEnvironment.AppPath, setting);
            //重新初始化GlobalSetting
			InitWorker.InitGlobalSetting(AppEnvironment.AppPath);
        }
    }
}
