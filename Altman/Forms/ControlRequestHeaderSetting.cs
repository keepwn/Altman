using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Altman.Controls;

namespace Altman.Forms
{
    internal partial class ControlRequestHeaderSetting : UserControl
    {
        public ControlRequestHeaderSetting()
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            rightMenu_Header.Opening += rightMenu_Header_Opening;
            lv_header.AfterEditSubItem += lv_header_AfterEditSubItem;
        }

        private void rightMenu_Header_Opening(object sender, CancelEventArgs e)
        {
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
        }
        private void item_add_Click(object sender, EventArgs e)
        {
            if (lv_header.SelectedItems.Count == 0)
            {
                ListViewItem item = new ListViewItem(new string[] { "NewKey", ""});
                lv_header.Items.Add(item);
                lv_header.EditSubItem(item, 0, "add", true);
            }
        }
        private void item_delete_Click(object sender, EventArgs e)
        {
            if (lv_header.SelectedItems.Count > 0)
            {
                if (lv_header.SelectedItems[0].Index > 3)
                {
                    lv_header.Items.Remove(lv_header.SelectedItems[0]);
                }
            }
        }
        private void item_edit_Click(object sender, EventArgs e)
        {
            if (lv_header.SelectedItems.Count > 0)
            {
                lv_header.EditSubItem(lv_header.SelectedItems[0], 1, "edit",false);
            }
        }
        private void lv_header_AfterEditSubItem(object sender, ListViewPlus.EditSubItemEventArgs e)
        {
            if (e.Label == e.OldLabel)
            {
                e.IsCancelEdit = true;
                return;
            }
            if (e.Label == "")
            {
                e.IsCancelEdit = true;
                MessageBox.Show("不许为空");
                return;
            }
        }

        public void LoadHttpHeaderSetting(Setting.Setting.HttpHeaderStruct header)
        {
            if (header.HttpHeaderList != null)
            {
                foreach (var i in header.HttpHeaderList)
                {
                    ListViewItem item = new ListViewItem(new string[] { i.Key, i.Value});
                    lv_header.Items.Add(item);
                }
            }
        }
        public Setting.Setting.HttpHeaderStruct SaveHttpHeaderSetting()
        {
            Setting.Setting.HttpHeaderStruct httpHeader = new Setting.Setting.HttpHeaderStruct();
            httpHeader.HttpHeaderList = new Dictionary<string, string>();
            foreach(ListViewItem item in lv_header.Items)
            {
                string key = item.SubItems[0].Text;
                string value = item.SubItems[1].Text;
                if (!httpHeader.HttpHeaderList.ContainsKey(key))
                {
                    httpHeader.HttpHeaderList.Add(key, value);
                }
            }
            return httpHeader;
        }
    }
}
