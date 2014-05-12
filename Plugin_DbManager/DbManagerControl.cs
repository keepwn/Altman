using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.Common.AltData;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_DbManager
{
    public partial class DbManagerControl : UserControl
    {
        private HostService _hostService;
        private ShellStruct _shellData;

        private DbManagerService dbManagerService;
        public DbManagerControl(HostService hostService, ShellStruct data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _hostService = hostService;
            _shellData = data;

            dbManagerService = new DbManagerService(_hostService, _shellData);
            dbManagerService.GetDbNameCompletedToDo += dbManagerService_GetDbNameCompletedToDo;
            dbManagerService.GetDbTableNameCompletedToDo += dbManagerService_GetDbTableNameCompletedToDo;
            dbManagerService.GetColumnTypeCompletedToDo += dbManagerService_GetColumnTypeCompletedToDo;

            //获取数据库
            dbManagerService.GetDbName(_shellData.ShellExtraSetting);
        }

        private void dbManagerService_GetColumnTypeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _hostService.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is string[])
            {
                string[] columns = e.Result as string[];
                if (columns.Length > 0)
                {
                    RefreshColumnsInDbTree(columns, treeView_Dbs.SelectedNode);
                }
                else
                {
                    //更改table为失败的图标
                    treeView_Dbs.SelectedNode.ImageIndex = 4;
                    treeView_Dbs.SelectedNode.SelectedImageIndex = 4;
                }
            }
        }
        private void dbManagerService_GetDbTableNameCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _hostService.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is string[])
            {
                string[] tables = e.Result as string[];
                if (tables.Length > 0)
                {
                    RefreshTablesInDbTree(tables, treeView_Dbs.SelectedNode);
                }
                else
                {
                    //更改db为失败的图标
                    treeView_Dbs.SelectedNode.ImageIndex = 1;
                    treeView_Dbs.SelectedNode.SelectedImageIndex = 1;
                }
            }
        }
        private void dbManagerService_GetDbNameCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _hostService.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is string[])
            {
                string[] dbs = e.Result as string[];
                if (dbs.Length > 0)
                {
                    RefreshRootInDbTree(true);
                    RefreshDbsInDbTree(dbs, treeView_Dbs.Nodes[0]);
                }
                else
                {
                    //更改root为失败的图标
                    RefreshRootInDbTree(false);
                }
            }
        }

        private void RefreshRootInDbTree(bool isSuccess)
        {
            treeView_Dbs.Nodes.Clear();
            int index = isSuccess ? 0 : 1;
            string root = "(local)";
            TreeNode node = new TreeNode(root, index, index)
            {
                Name = root,
                Tag = "root"
            };           
            treeView_Dbs.Nodes.Add(node);
        }

        private void RefreshDbsInDbTree(string[] dbs,TreeNode selected)
        {
            selected.Nodes.Clear();
            foreach (string db in dbs)
            {
                TreeNode node = new TreeNode(db, 2, 2)
                {
                    Name = db,
                    Tag = "db"
                };
                selected.Nodes.Add(node);
            }           
            selected.Expand();
        }

        private void RefreshTablesInDbTree(string[] tables,TreeNode selected)
        {
            selected.Nodes.Clear();
            foreach (string table in tables)
            {
                TreeNode node = new TreeNode(table, 3, 3)
                {
                    Name = table,
                    Tag = "table"
                };
                selected.Nodes.Add(node);
            }           
            selected.Expand();
        }

        private void RefreshColumnsInDbTree(string[] columns, TreeNode selected)
        {
            selected.Nodes.Clear();
            foreach (string column in columns)
            {
                TreeNode node = new TreeNode(column, 5, 5)
                {
                    Name = column,
                    Tag = "column"
                };
                selected.Nodes.Add(node);
            }           
            selected.Expand();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //dbManagerService.GetDbName(_shellData.ShellExtraSetting);
        }

        private void treeView_Dbs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string name = treeView_Dbs.SelectedNode.Text;
            string type = (string)(treeView_Dbs.SelectedNode.Tag??"");
            if (name != "")
            {
                if (type == "root")
                {
                    dbManagerService.GetDbName(_shellData.ShellExtraSetting);
                }
                else if (type == "db")
                {
                    dbManagerService.GetTableName(_shellData.ShellExtraSetting, name);
                }
                else if (type == "table")
                {
                    string dbname = treeView_Dbs.SelectedNode.Parent.Text;
                    dbManagerService.GetColumnType(_shellData.ShellExtraSetting, dbname,name);
                }
            }         
        }
    }
}
