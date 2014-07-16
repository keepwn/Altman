using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Altman.ModelCore;
using Altman.Plugins;

namespace Plugin_PluginManager
{
    public partial class PluginManager : Form
    {
        private IHostService _hostService;
        private Shell _shellData;
        public PluginManager(IHostService hostService, Shell data)
        {
            InitializeComponent();

            this._hostService = hostService;
            this._shellData = data;

            LoadInstalledPlugins();
        }


        private void LoadInstalledPlugins()
        {
            //clear
            lv_InstalledPlugins.Items.Clear();
            //loading
            IEnumerable<IPlugin> plugins = _hostService.Core.GetPlugins();
            foreach (var plugin in plugins)
            {
                string[] items = new string[] {
                    plugin.PluginAttribute.Name,
                    plugin.PluginAttribute.Author,
                    plugin.PluginAttribute.Version,
                    plugin.PluginAttribute.FileName
                    };
                ListViewItem viewItem = new ListViewItem(items);
                viewItem.Tag = plugin;
                lv_InstalledPlugins.Items.Add(viewItem);               
            }
        }

        private void btn_Close_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void lv_InstalledPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_InstalledPlugins.SelectedItems.Count == 1)
            {
                IPlugin plugin = (IPlugin)(lv_InstalledPlugins.SelectedItems[0]).Tag;
                tbx_InstalledPluginsDescription.Text = plugin.PluginAttribute.Description;
            }
        }
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lv_InstalledPlugins.CheckedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("You Will Remove This/These Plugins, Continue?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lv_InstalledPlugins.CheckedItems)
                    {
                        IPlugin plugin = (IPlugin)item.Tag;
                        string filepath = "./Plugins" + "/" + plugin.PluginAttribute.FileName;
                        File.Delete(filepath);
                        item.Remove();
                    }
                    lbl_Msg.Visible = true;
                }
            }
        }


        private void lv_AvailablePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lv_UpdatesPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {

        }

        private void btn_Install_Click(object sender, EventArgs e)
        {

        }


    }
}
