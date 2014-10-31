using System.Collections.Generic;
using Altman.Dialogs;
using Altman.Resources;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelUserAgentSetting : Panel, IOptions
    {
        public PanelUserAgentSetting()
        {
            Init();
        }

        public void LoadSetting(Setting setting)
        {
	        var userAgent = setting.UserAgentSetting;
            _checkBoxIsRandom.Checked = userAgent.IsRandom;
            foreach (var i in userAgent.UserAgentList)
            {
                var item = new ListItem {Key = i.Key, Text = i.Value};
	            _comboBoxUserAgentList.Items.Add(item);
            }
            //从现有的items中选择
            foreach (var item in _comboBoxUserAgentList.Items)
            {
                if (item.Key == userAgent.Selected.Key)
                {
                    _comboBoxUserAgentList.SelectedKey = item.Key;
                    break;
                }          
            }
        }

        public Setting SaveSetting()
        {
            var userAgent = new Setting.UserAgentStruct();
            userAgent.UserAgentList = new Dictionary<string, string>();
            //获取随机化开关
            userAgent.IsRandom = _checkBoxIsRandom.Checked==true;
            //获取选中项
            int index = _comboBoxUserAgentList.SelectedIndex;
            if (index == -1)
            {
                userAgent.Selected =new KeyValuePair<string,string>(_comboBoxUserAgentList.Items[0].Key,_comboBoxUserAgentList.Items[0].Text);
            }
            else
            {
	            var selected = _comboBoxUserAgentList.SelectedValue as ListItem;
	            var item = new KeyValuePair<string, string>(selected.Key, selected.Text);
                userAgent.Selected = item;
                //MessageBox.Show(item.Key);
            }
            //获取所有列表
            foreach (var item in _comboBoxUserAgentList.Items)
            {
                if (!userAgent.UserAgentList.ContainsKey(item.Key))
                {
                    userAgent.UserAgentList.Add(item.Key, item.Text);
                }
            }

			var setting = new Setting {UserAgentSetting = userAgent};
	        return setting;
        }
    }
}
