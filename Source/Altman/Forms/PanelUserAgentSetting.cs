using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Desktop.Forms
{
	public class PanelUserAgentSetting : Panel
    {
        private CheckBox checkBox_IsRandom;
        private ComboBox comboBox_UserAgentList;
        public PanelUserAgentSetting()
        {
            Init();
        }

        void Init()
        {
            checkBox_IsRandom = new CheckBox()
            {
				ID = "CheckBoxRandomUserAgent",
                Text = "Random User-Agent"
            };
            comboBox_UserAgentList = new ComboBox{Size=new Size(300,25)};

            var layout = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(10, 10) };

            layout.AddRow(checkBox_IsRandom);
			layout.AddRow(new Label { ID = "LabelUserAgentList", Text = "User-Agent List" });
            layout.AddRow(comboBox_UserAgentList,null);
            layout.Add(null);

            this.Content = layout;
        }

        public void LoadUserAgnetSetting(Setting.Setting.UserAgentStruct userAgent)
        {
            checkBox_IsRandom.Checked = userAgent.IsRandom;
            foreach (var i in userAgent.UserAgentList)
            {
                var item = new ListItem {Key = i.Key, Text = i.Value};
	            comboBox_UserAgentList.Items.Add(item);
            }
            //从现有的items中选择
            foreach (var item in comboBox_UserAgentList.Items)
            {
                if (item.Key == userAgent.Selected.Key)
                {
                    comboBox_UserAgentList.SelectedKey = item.Key;
                    break;
                }          
            }
        }

        public Setting.Setting.UserAgentStruct SaveUserAgnetSetting()
        {
            var userAgent = new Setting.Setting.UserAgentStruct();
            userAgent.UserAgentList = new Dictionary<string, string>();
            //获取随机化开关
            userAgent.IsRandom = checkBox_IsRandom.Checked==true;
            //获取选中项
            int index = comboBox_UserAgentList.SelectedIndex;
            if (index == -1)
            {
                userAgent.Selected =new KeyValuePair<string,string>(comboBox_UserAgentList.Items[0].Key,comboBox_UserAgentList.Items[0].Text);
            }
            else
            {
	            var selected = comboBox_UserAgentList.SelectedValue as ListItem;
	            var item = new KeyValuePair<string, string>(selected.Key, selected.Text);
                userAgent.Selected = item;
                //MessageBox.Show(item.Key);
            }
            //获取所有列表
            foreach (var item in comboBox_UserAgentList.Items)
            {
                if (!userAgent.UserAgentList.ContainsKey(item.Key))
                {
                    userAgent.UserAgentList.Add(item.Key, item.Text);
                }
            }
            return userAgent;
        }
    }
}
