using System;
using Altman.Desktop.Logic;
using Altman.Desktop.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Desktop.Forms
{
    public class FormGlobalSetting : Form
    {
        private readonly Func<Setting.Setting.UserAgentStruct> _saveUserAgentSetting;
        private readonly Action<Setting.Setting.UserAgentStruct> _loadUserAgentSetting;
        private readonly Func<Setting.Setting.HttpHeaderStruct> _saveHttpHeaderSetting;
        private readonly Action<Setting.Setting.HttpHeaderStruct> _loadHttpHeaderSetting;
        private readonly Func<Setting.Setting.PolicyStruct> _savePolicySetting;
        private readonly Action<Setting.Setting.PolicyStruct> _loadPolicySetting;
        private readonly Func<Setting.Setting.ProxyStruct> _saveProxySetting;
        private readonly Action<Setting.Setting.ProxyStruct> _loadProxySetting;

        private ControlUserAgentSetting _controlUserAgentSetting;
        private ControlRequestHeaderSetting _controlRequestHeaderSetting;
        private ControlPolicySetting _controlPolicySetting;
        private ControlProxySetting _controlProxySetting;

        private Button button_SaveSetting;
        private Button button_cancel;
        public FormGlobalSetting()
        {
            Init();
            _saveUserAgentSetting = _controlUserAgentSetting.SaveUserAgnetSetting;
            _loadUserAgentSetting = _controlUserAgentSetting.LoadUserAgnetSetting;

            _saveHttpHeaderSetting = _controlRequestHeaderSetting.SaveHttpHeaderSetting;
            _loadHttpHeaderSetting = _controlRequestHeaderSetting.LoadHttpHeaderSetting;

            _savePolicySetting = _controlPolicySetting.SavePolicySetting;
            _loadPolicySetting = _controlPolicySetting.LoadPolicySetting;

            _saveProxySetting = _controlProxySetting.SaveProxySetting;
            _loadProxySetting = _controlProxySetting.LoadProxySetting;

            _loadUserAgentSetting(((Setting.Setting)GlobalSetting.Setting).GetUserAgentStruct);
            _loadHttpHeaderSetting(((Setting.Setting)GlobalSetting.Setting).GetHttpHeaderStruct);
            _loadPolicySetting(((Setting.Setting) GlobalSetting.Setting).GetPolicyStruct);
            _loadProxySetting(((Setting.Setting)GlobalSetting.Setting).GetProxyStruct);
        }

        void Init()
        {
            //controlUserAgentSetting
            _controlUserAgentSetting = new ControlUserAgentSetting();
            //controlRequestHeaderSetting
            _controlRequestHeaderSetting = new ControlRequestHeaderSetting();
            //controlPolicySetting
            _controlPolicySetting = new ControlPolicySetting();
            //controlProxySetting
            _controlProxySetting = new ControlProxySetting();

            var tabControl = new TabControl()
            {
                Size = new Size(350,300)
            };

            tabControl.Pages.Add(new TabPage { Text = "UserAgent", Content = _controlUserAgentSetting });
            tabControl.Pages.Add(new TabPage { Text = "HttpHeader", Content = _controlRequestHeaderSetting });
            tabControl.Pages.Add(new TabPage { Text = "Policy", Content = _controlPolicySetting });
            tabControl.Pages.Add(new TabPage { Text = "Proxy", Content = _controlProxySetting });

            //button
            button_SaveSetting = new Button()
            {
                Text = "Save&Exit"
            };
            button_SaveSetting.Click += delegate
            {
                SaveAllSetting();
                this.Close();
            };

            button_cancel = new Button()
            {
                Text = "Cancel"
            };
            button_cancel.Click += delegate
            {
                this.Close();
            };

            var layout = new DynamicLayout{Padding=new Padding(0,10),Spacing=new Size(5,5)};
            layout.AddRow(tabControl);
            layout.AddSeparateRow(null, button_SaveSetting, button_cancel);
            layout.Add(null);

            this.Content = layout;
            this.Size = new Size(400, 390);
            this.Title = "Setting";
        }

        private void SaveAllSetting()
        {
            Setting.Setting.UserAgentStruct userAgent=new Setting.Setting.UserAgentStruct();
            Setting.Setting.HttpHeaderStruct httpHeader=new Setting.Setting.HttpHeaderStruct();
            Setting.Setting.PolicyStruct policy=new Setting.Setting.PolicyStruct();
            Setting.Setting.ProxyStruct proxy=new Setting.Setting.ProxyStruct();
            //生成userAgent
            userAgent = _saveUserAgentSetting();
            //生成httpHeader
            httpHeader = _saveHttpHeaderSetting();
            //生成policy
            policy = _savePolicySetting();
            //生成proxy
            proxy = _saveProxySetting();
            //生成Setting
            Setting.Setting setting = new Setting.Setting(userAgent, httpHeader, policy, proxy);
            //保存Setting到xml
            InitWorker.SaveSettingToXml(setting);
            //重新初始化GlobalSetting
            InitWorker.InitGlobalSetting();
        }

        private void button_SaveSetting_Click(object sender, EventArgs e)
        {
            
        }
        private void button_cancel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
