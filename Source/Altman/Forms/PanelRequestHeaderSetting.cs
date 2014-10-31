using System;
using System.Collections.Generic;
using System.ComponentModel;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelRequestHeaderSetting : Panel
    {
        class HeaderItem
        {
            private string _key;
            private string _value;

            public string Key
            {
                get { return _key; }
                set { _key = value; }
            }
            public string Value
            {
                get { return _value; }
                set { _value = value; }
            }
            public HeaderItem(string key, string value)
            {
                this._key = key;
                this._value = value;
            }
        }

        public PanelRequestHeaderSetting()
        {
            Init();
        }

        void _gridViewHeader_CellEdited(object sender, GridViewCellArgs e)
        {

        }

        void _gridViewHeader_CellEditing(object sender, GridViewCellArgs e)
        {

        }

        private void rightMenu_Header_Opening(object sender, CancelEventArgs e)
        {
            /*
            if (lv_header.SelectedItems.Count > 0)
            {
                if (lv_header.SelectedItems[0].Index > 3)
                {
                    rightMenu_Header.Items["item_delete"].Enabled = true;
                }
                else
                {
                    rightMenu_Header.Items["item_delete"].Enabled = false;
                }
                rightMenu_Header.Items["item_add"].Enabled = false;
                rightMenu_Header.Items["item_edit"].Enabled = true;

            }
            else
            {
                rightMenu_Header.Items["item_add"].Enabled = true;
                rightMenu_Header.Items["item_edit"].Enabled = false;
                rightMenu_Header.Items["item_delete"].Enabled = false;
            }
             */
        }
        private void item_add_Click(object sender, EventArgs e)
        {
            /*
            if (lv_header.SelectedItems.Count == 0)
            {
                ListViewItem item = new ListViewItem(new string[] { "NewKey", ""});
                lv_header.Items.Add(item);
                lv_header.EditSubItem(item, 0, "add", true);
            }
             */
        }
        private void item_delete_Click(object sender, EventArgs e)
        {
            /*
            if (lv_header.SelectedItems.Count > 0)
            {
                if (lv_header.SelectedItems[0].Index > 3)
                {
                    lv_header.Items.Remove(lv_header.SelectedItems[0]);
                }
            }
             */
        }
        private void item_edit_Click(object sender, EventArgs e)
        {
            /*
            if (lv_header.SelectedItems.Count > 0)
            {
                lv_header.EditSubItem(lv_header.SelectedItems[0], 1, "edit",false);
            }
             */
        }

        public void LoadHttpHeaderSetting(Setting.HttpHeaderStruct header)
        {
            if (header.HttpHeaderList != null)
            {
                foreach (var i in header.HttpHeaderList)
                {
                    var items = _gridViewHeader.DataStore as DataStoreCollection;
                    items.Add(new HeaderItem(i.Key, i.Value));
                }
            }
        }

        public Setting.HttpHeaderStruct SaveHttpHeaderSetting()
        {
            Setting.HttpHeaderStruct httpHeader = new Setting.HttpHeaderStruct();
            httpHeader.HttpHeaderList = new Dictionary<string, string>();

            var items = _gridViewHeader.DataStore as DataStoreCollection;
            if (items != null)
            {
                foreach (var item in items)
                {
                    string key = (item as HeaderItem).Key;
                    string value = (item as HeaderItem).Value;
                    if (!httpHeader.HttpHeaderList.ContainsKey(key))
                    {
                        httpHeader.HttpHeaderList.Add(key, value);
                    }
                }
            }
            return httpHeader;
        }
    }
}
