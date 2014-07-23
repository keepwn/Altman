using System.Collections.Generic;
using System.Windows.Forms;

namespace Altman.Forms
{
    public partial class ControlUserAgentSetting : UserControl
    {
        public ControlUserAgentSetting()
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }


        public void LoadUserAgnetSetting(Setting.Setting.UserAgentStruct userAgent)
        {
            checkBox_IsRandom.Checked = userAgent.IsRandom;
            foreach (var i in userAgent.UserAgentList)
            {
                //不显示Custom值
                if (i.Key == "Custom") continue;
                comboBox_UserAgentList.Items.Add(i);
            }
            //显示选中项
            if (userAgent.Selected.Key == "Custom")
            {
                //作为自定义内容显示在comboBox_UserAgentList.Text
                comboBox_UserAgentList.Text = userAgent.Selected.Value;
            }
            else
            {
                //从现有的items中选择
                foreach (object i in comboBox_UserAgentList.Items)
                {
                    if (((KeyValuePair<string,string>)i).Key == userAgent.Selected.Key)
                    {
                        comboBox_UserAgentList.SelectedItem = i;
                    }
                    break;
                }
            }
        }

        public Setting.Setting.UserAgentStruct SaveUserAgnetSetting()
        {
            Setting.Setting.UserAgentStruct userAgent = new Setting.Setting.UserAgentStruct();
            userAgent.UserAgentList = new Dictionary<string, string>();
            //获取随机化开关
            userAgent.IsRandom = checkBox_IsRandom.Checked;
            //获取选中项
            int index = comboBox_UserAgentList.SelectedIndex;
            if (index == -1)
            {
                userAgent.Selected = new KeyValuePair<string, string>("Custom", comboBox_UserAgentList.Text);
            }
            else
            {
                KeyValuePair<string, string> item = (KeyValuePair<string, string>)comboBox_UserAgentList.SelectedItem;
                userAgent.Selected = item;
                //MessageBox.Show(item.Key);
            }
            //获取所有列表
            userAgent.UserAgentList.Add("Custom", comboBox_UserAgentList.Text);//必须将custom添加进去
            foreach (var i in comboBox_UserAgentList.Items)
            {
                KeyValuePair<string, string> item = (KeyValuePair<string, string>)i;
                if (!userAgent.UserAgentList.ContainsKey(item.Key))
                {
                    userAgent.UserAgentList.Add(item.Key, item.Value);
                }
            }
            return userAgent;
        }
    }
}
