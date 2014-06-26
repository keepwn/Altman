using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Altman.LogicCore;
using Altman.Setting;

namespace Altman
{
    public partial class FormGlobalSetting : Form
    {
        private readonly Func<Setting.Setting.UserAgentStruct> _saveUserAgentSetting;
        private readonly Action<Setting.Setting.UserAgentStruct> _loadUserAgentSetting;
        private readonly Func<Setting.Setting.HttpHeaderStruct> _saveHttpHeaderSetting;
        private readonly Action<Setting.Setting.HttpHeaderStruct> _loadHttpHeaderSetting;
        private readonly Func<Setting.Setting.PolicyStruct> _savePolicySetting;
        private readonly Action<Setting.Setting.PolicyStruct> _loadPolicySetting;
        private readonly Func<Setting.Setting.ProxyStruct> _saveProxySetting;
        private readonly Action<Setting.Setting.ProxyStruct> _loadProxySetting;

        public FormGlobalSetting()
        {
            InitializeComponent();

            _saveUserAgentSetting = controlUserAgentSetting1.SaveUserAgnetSetting;
            _loadUserAgentSetting = controlUserAgentSetting1.LoadUserAgnetSetting;
            _saveHttpHeaderSetting = controlRequestHeaderSetting1.SaveHttpHeaderSetting;
            _loadHttpHeaderSetting = controlRequestHeaderSetting1.LoadHttpHeaderSetting;
            _savePolicySetting = controlPolicySetting1.SavePolicySetting;
            _loadPolicySetting = controlPolicySetting1.LoadPolicySetting;
            _saveProxySetting = controlProxySetting1.SaveProxySetting;
            _loadProxySetting = controlProxySetting1.LoadProxySetting;

            _loadUserAgentSetting(((Setting.Setting)GlobalSetting.Setting).GetUserAgentStruct);
            _loadHttpHeaderSetting(((Setting.Setting)GlobalSetting.Setting).GetHttpHeaderStruct);
            _loadPolicySetting(((Setting.Setting) GlobalSetting.Setting).GetPolicyStruct);
            _loadProxySetting(((Setting.Setting)GlobalSetting.Setting).GetProxyStruct);
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
            SaveAllSetting();
            this.Close();
        }
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
