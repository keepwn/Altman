using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Altman.Dialogs;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelHttpHeaderSetting : Panel, IOptions
    {
        class HeaderItem
        {
	        public string Key { get; private set; }

	        public string Value { get; private set; }

	        public HeaderItem(string key, string value)
            {
                this.Key = key;
                this.Value = value;
            }
        }

        public PanelHttpHeaderSetting()
        {
            Init();
        }

		void delItem_Click(object sender, EventArgs e)
		{
			if (_gridViewHeader.SelectedRows.Any())
			{
				var rows = _gridViewHeader.SelectedRows;
				var items = _gridViewHeader.DataStore as DataStoreCollection<HeaderItem>;
				foreach (var row in rows)
				{
					items.RemoveAt(row);
				}
			}
		}

		void addItem_Click(object sender, EventArgs e)
		{
			var items = _gridViewHeader.DataStore as DataStoreCollection<HeaderItem>;
			var newItem = new HeaderItem("NewKey", "");		
			items.Add(newItem);

			var row = items.IndexOf(newItem);
			_gridViewHeader.BeginEdit(row, 0);
		}

        public void LoadSetting(Setting setting)
        {
			var header = setting.HttpHeaderSetting;
            if (header.HttpHeaderList != null)
            {
	            var items = new DataStoreCollection<HeaderItem>();
	            foreach (var i in header.HttpHeaderList)
	            {
					items.Add(new HeaderItem(i.Key, i.Value));
	            }
	            _gridViewHeader.DataStore = items;
            }
        }

        public Setting SaveSetting()
        {       
            var httpHeader = new Setting.HttpHeaderStruct();
	        httpHeader.HttpHeaderList = new Dictionary<string, string>();
	        var items = _gridViewHeader.DataStore as DataStoreCollection<HeaderItem>;
            if (items != null)
            {
                foreach (var item in items)
                {
                    var key = item.Key;
                    var value = item.Value;
                    if (!httpHeader.HttpHeaderList.ContainsKey(key))
                    {
                        httpHeader.HttpHeaderList.Add(key, value);
                    }
                }
            }

			var setting = new Setting {HttpHeaderSetting = httpHeader};
	        return setting;
        }
    }
}
