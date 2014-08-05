using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Altman.Model;
using PluginFramework;

namespace Plugin_ShellManager
{
    public partial class ShellManagerControl : UserControl
    {
        private IHost _host;
        private Shell _shellData;
        private ShellManagerService _shellManagerService = null;

        public ShellManagerControl(IHost host, Shell data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            //创建listview
            //CreateListView();

            this._host = host;
            this._shellData = data;

            //注册事件
            _shellManagerService = new ShellManagerService(_host);
            _shellManagerService.GetDataTableCompletedToDo += ShellManagerServiceGetDataTableCompletedToDo;
            _shellManagerService.DeleteCompletedToDo += ShellManagerServiceDeleteCompletedToDo;
            _shellManagerService.InsertCompletedToDo += ShellManagerServiceInsertCompletedToDo;
            _shellManagerService.UpdateCompletedToDo += ShellManagerServiceUpdateCompletedToDo;

            //载入shell数据
            LoadWebshellData();

            //添加插件到右键菜单
            foreach (var plugin in host.Core.GetPlugins())
            {
                //IsShowInRightContext
                if (plugin.PluginSetting.LoadPath=="webshell" && plugin.PluginSetting.IsShowInRightContext)
                {
                    string title = plugin.PluginInfo.Name;

                    //添加到Tsmi_Plugins中
                    ToolStripMenuItem pluginItem = new ToolStripMenuItem();
                    pluginItem.Name = title;
                    pluginItem.Text = title;
                    pluginItem.Click += pluginItem_Click;
                    pluginItem.Tag = plugin;
                    rightMenu_Webshell.Items.Add(pluginItem);
                }           
            }
        }

        private void pluginItem_Click(object sender, EventArgs e)
        {
            if (lv_shell.SelectedItems.Count > 0)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item != null)
                {
                    IPlugin plugin = item.Tag as IPlugin;

                    Shell shell = (Shell) lv_shell.SelectedItems[0].Tag;
                    shell.TimeOut = 8000;

                    if (plugin is IControlPlugin)
                    {
                        UserControl view = (plugin as IControlPlugin).GetUi(shell);
                        //创建新的tab标签
                        //设置标题为FileManager|TargetId
                        string title = plugin.PluginInfo.Name + "|" + shell.TargetId;
                        _host.Ui.CreateNewTabPage(title, view);
                    }
                    else if (plugin is IFormPlugin)
                    {
                        Form form = (plugin as IFormPlugin).GetUi(shell);
                        form.Show();
                    }
                }
            }
        }

        /// <summary>
        /// 创建listview
        /// </summary>
        private void CreateListView()
        {
            lv_shell = new ListView
            {
                ContextMenuStrip = this.rightMenu_Webshell,
                Dock = System.Windows.Forms.DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true,
                Location = new System.Drawing.Point(0, 0),
                MultiSelect = false,
                Name = "lv_shell",
                Size = new System.Drawing.Size(648, 315),
                TabIndex = 2,
                UseCompatibleStateImageBehavior = false,
                View = System.Windows.Forms.View.Details
            };

            //添加webshell列
            lv_shell.Columns.Add("Id", 0);
            //1-3
            lv_shell.Columns.Add("序号", 30, HorizontalAlignment.Left);
            lv_shell.Columns[1].TextAlign = HorizontalAlignment.Left;
            lv_shell.Columns.Add("项目编号", 80);
            lv_shell.Columns[2].TextAlign = HorizontalAlignment.Left;
            lv_shell.Columns.Add("级别", 40);
            lv_shell.Columns[3].TextAlign = HorizontalAlignment.Center;
            //4-7
            lv_shell.Columns.Add("状态", 40);
            lv_shell.Columns[4].TextAlign = HorizontalAlignment.Center;
            lv_shell.Columns.Add("Shell地址", 250);
            lv_shell.Columns[5].TextAlign = HorizontalAlignment.Left;
            lv_shell.Columns.Add("类型", 50);
            lv_shell.Columns[6].TextAlign = HorizontalAlignment.Center;
            lv_shell.Columns.Add("服务器编码", 0);
            lv_shell.Columns[7].TextAlign = HorizontalAlignment.Left;
            //8-10
            lv_shell.Columns.Add("国家", 60);
            lv_shell.Columns[8].TextAlign = HorizontalAlignment.Left;
            lv_shell.Columns.Add("备注", 120);
            lv_shell.Columns[9].TextAlign = HorizontalAlignment.Left;            
            lv_shell.Columns.Add("添加时间", 100);
            lv_shell.Columns[10].TextAlign = HorizontalAlignment.Center;

            this.Controls.Add(lv_shell);
        }

        private Shell ConvertDataRowToShellStruct(DataRow row)
        {
            Shell shell = new Shell();

            shell.Id = row["id"].ToString();
            shell.TargetId = row["target_id"].ToString();
            shell.TargetLevel = row["target_level"].ToString();
            shell.Status = row["status"].ToString();

            shell.ShellUrl = row["shell_url"].ToString();
            shell.ShellPwd = row["shell_pwd"].ToString();
            shell.ShellType = row["shell_type"].ToString();
            shell.ShellExtraString = row["shell_extra_setting"].ToString();
            shell.ServerCoding = row["server_coding"].ToString();
            shell.WebCoding = row["web_coding"].ToString();

            shell.Area = row["area"].ToString();
            shell.Remark = row["remark"].ToString();
            shell.AddTime = row["add_time"].ToString();

            return shell;
        }


        /// <summary>
        /// 载入webshell数据
        /// </summary>
        public void LoadWebshellData()
        {
            int num = 1;
            lv_shell.Items.Clear();
            DataTable dataTable = _shellManagerService.GetDataTable();
            if (dataTable == null)
            {
                return;
            }
            foreach (DataRow row in dataTable.Rows)
            {
                Shell shell = ConvertDataRowToShellStruct(row);
                string[] items = new string[] { 
                    shell.Id, 
                    num++.ToString(), 
                    shell.TargetId, 
                    shell.TargetLevel,
                    shell.Status,
                    shell.ShellUrl, 
                    shell.ShellType,
                    shell.ServerCoding, 
                    shell.Area,
                    shell.Remark,
                    shell.AddTime
                    };

                ListViewItem viewItem = new ListViewItem(items);
                viewItem.Tag = shell;
                lv_shell.Items.Add(viewItem);
            }
        }

        #region 数据获取/插入/删除/更新事件
        private void ShellManagerServiceUpdateCompletedToDo(object sender, ShellManagerService.CompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }
        private void ShellManagerServiceInsertCompletedToDo(object sender, ShellManagerService.CompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }
        private void ShellManagerServiceDeleteCompletedToDo(object sender, ShellManagerService.CompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }
        private void ShellManagerServiceGetDataTableCompletedToDo(object sender, ShellManagerService.CompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        #endregion

        #region 右键菜单事件
        private void OnWebshellChange(object sender, EventArgs e)
        {
            LoadWebshellData();
        }
        private void item_add_Click(object sender, EventArgs e)
        {
            FormEditWebshell editwebshell = new FormEditWebshell(_host);
            editwebshell.WebshellWatchEvent += OnWebshellChange;
            editwebshell.Show();
        }
        private void item_alter_Click(object sender, EventArgs e)
        {
            if (lv_shell.SelectedItems.Count > 0)
            {
                Shell shell = (Shell)lv_shell.SelectedItems[0].Tag;
                //Shell Shell = (Shell)lv_shell.SelectedItems[0].Tag;

                FormEditWebshell editwebshell = new FormEditWebshell(_host, shell);
                editwebshell.WebshellWatchEvent += OnWebshellChange;
                editwebshell.Show();
            }
        }
        private void item_del_Click(object sender, EventArgs e)
        {
            if (lv_shell.SelectedItems.Count > 0)
            {
                int id = int.Parse(((Shell)lv_shell.SelectedItems[0].Tag).Id);
                _shellManagerService.Delete(id);
                LoadWebshellData();
            }
        }
        #endregion

        #region 批量检测shell状态
        private void item_refreshStatus_Click(object sender, EventArgs e)
        {
            RefreshAllStatus();
        }
        private void RefreshAllStatus()
        {
            DataTable dataTable = _shellManagerService.GetDataTable();
            if (dataTable == null)
            {
                return;
            }
            foreach (ListViewItem item in lv_shell.Items)
            {
                Thread thread = new Thread(RefreshStatus);
                thread.Start(item);
            }
        }
        private void RefreshStatus(object listViewItem)
        {
            ListViewItem item = listViewItem as ListViewItem;
            Shell shell = (Shell)item.Tag;
            string shellUrl = shell.ShellUrl;

            HttpWebRequest myRequest = null;
            HttpWebResponse myResponse = null;
            try
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 1024;
                myRequest = (HttpWebRequest)WebRequest.Create(shellUrl);
                myRequest.Method = "HEAD";
                myRequest.Timeout = 5000;
                myRequest.KeepAlive = false;
                myRequest.UseDefaultCredentials = true;
                myRequest.AllowAutoRedirect = false;
                myResponse = (HttpWebResponse)myRequest.GetResponse();
            }
            catch (WebException ex)
            {
                myResponse = (HttpWebResponse)ex.Response;
            }
            finally
            {
                if (myRequest != null)
                    myRequest.Abort();
                if (myResponse != null)
                    myResponse.Close();
            }
            if (myResponse == null)
            {
                RefreshShellStatusInListView(item, "-1");
            }
            else
            {
                RefreshShellStatusInListView(item, Convert.ToInt32(myResponse.StatusCode).ToString());
            }
        }

        public delegate void RefreshShellStatusInvoke(ListViewItem item, string status);
        private void RefreshShellStatusInListView(ListViewItem item, string status)
        {
            //等待异步
            if (this.InvokeRequired)
            {
                RefreshShellStatusInvoke invoke = new RefreshShellStatusInvoke(RefreshShellStatusInListView);
                this.Invoke(invoke, new object[] { item, status });
            }
            else
            {
                item.SubItems[4].Text = status;

                Shell shell = (Shell)item.Tag;
                shell.Status = status;
                _shellManagerService.Update(int.Parse(shell.Id), shell);
            }
        }
        #endregion

        private void lv_shell_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lv_shell.SelectedItems.Count == 1)
            {
                //如果存在FileManager插件，则双击进入FileManager
                if (rightMenu_Webshell.Items.ContainsKey("FileManager"))
                {
                    ToolStripItem item = rightMenu_Webshell.Items["FileManager"];
                    pluginItem_Click(item, null);
                }
            }
        }

        private void item_copyServerCode_Click(object sender, EventArgs e)
        {
            if (lv_shell.SelectedItems.Count > 0)
            {
                Shell shell = (Shell)lv_shell.SelectedItems[0].Tag;
                string code = _host.Core.GetCustomShellTypeServerCode(shell.ShellType);

                if (string.IsNullOrWhiteSpace(code))
                {
                    MessageBox.Show("ServerCode is NUll!");
                }
                Clipboard.SetDataObject(code);
            }      
        }

        /**
         *  采用绑定ContextMenuStrip的方式，Mac下可能不会产生该事件
         *  须手动绑定MouseClick事件
         */
        private void lv_shell_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightMenu_Webshell.Show(lv_shell, e.Location);
            }
        }
    }
}
