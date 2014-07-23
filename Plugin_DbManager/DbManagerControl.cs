using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Altman.Model;
using PluginFramework;

namespace Plugin_DbManager
{
    public partial class DbManagerControl : UserControl
    {
        private IHost _host;
        private Shell _shellData;

        private DbManagerService dbManagerService;
        public DbManagerControl(IHost host, Shell data)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _host = host;
            _shellData = data;

            //绑定事件
            dbManagerService = new DbManagerService(_host, _shellData, GetDbType());
            dbManagerService.ConnectDbCompletedToDo += dbManagerService_ConnectDbCompletedToDo;
            dbManagerService.GetDbNameCompletedToDo += dbManagerService_GetDbNameCompletedToDo;
            dbManagerService.GetDbTableNameCompletedToDo += dbManagerService_GetTableNameCompletedToDo;
            dbManagerService.GetColumnTypeCompletedToDo += dbManagerService_GetColumnTypeCompletedToDo;
            dbManagerService.ExecuteReaderCompletedToDo += dbManagerService_ExecuteReaderCompletedToDo;
            dbManagerService.ExecuteNonQueryCompletedToDo += dbManagerService_ExecuteNonQueryCompletedToDo;
            
            treeView_Dbs.AfterSelect += treeView_Dbs_AfterSelect;
            rightMenu_TreeView.Opening += rightMenu_TreeView_Opening;

            RefreshServerStatus(false);

            //连接数据库
            dbManagerService.ConnectDb(GetConnStr());
        }



        #region ServiceCompletedToDo
        private void dbManagerService_ExecuteNonQueryCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _host.Ui.ShowMsgInStatusBar(e.Error.Message);
                ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is DataTable)
            {
                dataGridView_result.DataSource = e.Result;

                ShowMsgInStatusBar((e.Result as DataTable).Rows[0][0].ToString());
            }
        }
        private void dbManagerService_ExecuteReaderCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _host.Ui.ShowMsgInStatusBar(e.Error.Message);
                ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is DataTable)
            {
                dataGridView_result.DataSource = e.Result;

                ShowMsgInStatusBar(string.Format("{0} rows", (e.Result as DataTable).Rows.Count));
            }
        }
        private void dbManagerService_GetColumnTypeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);

                //更改table为失败的图标
                treeView_Dbs.SelectedNode.ImageIndex = 4;
                treeView_Dbs.SelectedNode.SelectedImageIndex = 4;
            }
            else if (e.Result is string[])
            {
                string[] columns = e.Result as string[];
                if (columns.Length > 0)
                {
                    RefreshColumnsInDbTree(columns, treeView_Dbs.SelectedNode);
                }
                ShowMsgInStatusBar(string.Format("{0} columns", columns.Length));
            }
        }
        private void dbManagerService_GetTableNameCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);

                //更改db为失败的图标
                treeView_Dbs.SelectedNode.ImageIndex = 1;
                treeView_Dbs.SelectedNode.SelectedImageIndex = 1;
            }
            else if (e.Result is string[])
            {
                string[] tables = e.Result as string[];
                if (tables.Length > 0)
                {
                    RefreshTablesInDbTree(tables, treeView_Dbs.SelectedNode);
                }
                ShowMsgInStatusBar(string.Format("{0} tables", tables.Length));
            }
        }
        private void dbManagerService_GetDbNameCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);
                //更改root为失败的图标
                RefreshRootInDbTree(false);
            }
            else if (e.Result is string[])
            {
                string[] dbs = e.Result as string[];
                if (dbs.Length > 0)
                {
                    RefreshRootInDbTree(true);
                    RefreshDbsInDbTree(dbs, treeView_Dbs.Nodes[0]);
                }
                ShowMsgInStatusBar(string.Format("{0} databases", dbs.Length));
            }
        }
        private void dbManagerService_ConnectDbCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);
                //更改root为失败的图标
                RefreshRootInDbTree(false);
            }
            else if (e.Result is bool)
            {
                if ((bool)e.Result)
                {
                    ShowMsgInStatusBar("Connect Successed");
                    RefreshServerStatus(true);
                    RefreshRootInDbTree(true);
                }
                else
                {
                    ShowMsgInStatusBar("Connect Failed");
                    //更改root为失败的图标
                    RefreshRootInDbTree(false);
                }
            }
        }

        #endregion

        #region Refresh UI
        private void RefreshServerStatus(bool isConnected)
        {
            if (isConnected)
            {
                toolStripButton_connect.Enabled = false;
                toolStripButton_disconnect.Enabled = true;
            }
            else
            {
                toolStripButton_connect.Enabled = true;
                toolStripButton_disconnect.Enabled = false;
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
        private void RefreshDbsInDbTree(string[] dbs, TreeNode selected)
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
            //刷新数据库选择框
            toolStripComboBox_dbs.Items.Clear();
            toolStripComboBox_dbs.Items.AddRange(dbs);
        }
        private void RefreshTablesInDbTree(string[] tables, TreeNode selected)
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
        #endregion

        #region treeView_Dbs Event
        private void treeView_Dbs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView_Dbs.SelectedNode = e.Node;
        }
        private void treeView_Dbs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string name = treeView_Dbs.SelectedNode.Text;
            string type = (string)(treeView_Dbs.SelectedNode.Tag ?? "");
            if (name != "")
            {
                if (type == "root")
                {
                    dbManagerService.GetDbName(GetConnStr());
                }
                else if (type == "db")
                {
                    dbManagerService.GetTableName(GetConnStr(), name);
                }
                else if (type == "table")
                {
                    string dbname = treeView_Dbs.SelectedNode.Parent.Text;

                    dbManagerService.GetColumnType(GetConnStr(), dbname, name);
                }
            }
        }
        private void treeView_Dbs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int index = treeView_Dbs.SelectedNode.Index;
            string type = (string)(treeView_Dbs.SelectedNode.Tag ?? "");
            if (type == "db")
            {
                toolStripComboBox_dbs.SelectedIndex = index;
            }
        }
        
        #endregion

        #region rightMenu_TreeView Event
        private void rightMenu_TreeView_Opening(object sender, CancelEventArgs e)
        {
            if (treeView_Dbs.SelectedNode == null) return;
            string name = treeView_Dbs.SelectedNode.Text;
            string type = (string)(treeView_Dbs.SelectedNode.Tag ?? "");
            if (name != "")
            {
                if (type == "root")
                {
                    rightMenu_TreeView.Items["Tsmi_ViewTable"].Visible = false;
                }
                else if (type == "db")
                {
                    rightMenu_TreeView.Items["Tsmi_ViewTable"].Visible = false;
                }
                else if (type == "table")
                {
                    rightMenu_TreeView.Items["Tsmi_ViewTable"].Visible = true;

                    string dbname = treeView_Dbs.SelectedNode.Parent.Text;
                }
            }
        }
        private void Tsmi_ViewTable_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this function is not yet implemented");
        }
        private void Tsmi_CopyName_Click(object sender, EventArgs e)
        {
            if (treeView_Dbs.SelectedNode == null) return;
            Clipboard.SetDataObject(treeView_Dbs.SelectedNode.Text);
        }
        #endregion

        #region toolStripButton Event
        /// <summary>
        /// 连接数据库
        /// </summary>
        private void toolStripButton_connect_Click(object sender, EventArgs e)
        {
            dbManagerService.ConnectDb(GetConnStr());
        }
        /// <summary>
        /// 断开数据库
        /// </summary>
        private void toolStripButton_disconnect_Click(object sender, EventArgs e)
        {
            RefreshServerStatus(false);
            RefreshRootInDbTree(false);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private void toolStripButton_run_Click(object sender, EventArgs e)
        {
            string sql = tbx_sql.Text;
            if (string.IsNullOrWhiteSpace(sql))
            {
                MessageBox.Show("the query sql cannot be empty");
            }
            else if (toolStripComboBox_dbs.SelectedIndex == -1)
            {
                MessageBox.Show("please select one database");
            }
            else
            {
                //执行前先清空之前结果
                dataGridView_result.DataSource = null;

                string dbName = (string)toolStripComboBox_dbs.Text;
                if (sql.ToLower().StartsWith("select"))
                {
                    dbManagerService.ExecuteReader(GetConnStr(), dbName, sql);
                }
                else
                {
                    dbManagerService.ExecuteNonQuery(GetConnStr(), dbName, sql);
                }
            }
        }
        #endregion

        #region rightMenu_DataTable Event
        private void Tsmi_SaveAsCsv_Click(object sender, EventArgs e)
        {
            object dataSource = dataGridView_result.DataSource;
            if (dataSource != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV|*.CSV";
                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    string fileName = saveFileDialog.FileName;
                    SaveAsCsv(dataSource as DataTable, fileName);
                }
            }
        }
        private void SaveAsCsv(DataTable dt, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            string data = "";

            //columns name
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName;
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);

            //rows data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("Save Ok!");
        }
        #endregion

        private string GetDbType()
        {
            string type = string.Empty;
            XmlNode node = _host.Core.GetShellSqlConnection(_shellData);
            if (node != null)
            {
                XmlNode typeNode = node.SelectSingleNode("type");
                if (typeNode != null)
                    type = typeNode.InnerText;
            }
            return type;
        }
        private string GetConnStr()
        {
            string conn = string.Empty;
            XmlNode node = _host.Core.GetShellSqlConnection(_shellData);
            if (node != null)
            {
                //获取type
                string scriptType = _shellData.ShellType;

                if (scriptType.StartsWith("php"))
                {
                    string host = string.Empty;
                    string user = string.Empty;
                    string pass = string.Empty;
                    string language = string.Empty;
                    XmlNode hostNode = node.SelectSingleNode("host");
                    if (hostNode != null) host = hostNode.InnerText;
                    XmlNode userNode = node.SelectSingleNode("user");
                    if (userNode != null) user = userNode.InnerText;
                    XmlNode passNode = node.SelectSingleNode("pass");
                    if (passNode != null) pass = passNode.InnerText;
                    XmlNode lanNode = node.SelectSingleNode("language");
                    if (lanNode != null) language = lanNode.InnerText;

                    conn = string.Format("{0};{1};{2};{3};", host, user, pass, language);
                }
                else
                {

                    XmlNode connNode = node.SelectSingleNode("conn");
                    if (connNode != null) 
                        conn = connNode.InnerText;
                }
            }
            return conn;           
        }
        private void ShowMsgInStatusBar(string msg)
        {
            _host.Ui.ShowMsgInStatusBar(msg);
        }
    }
}
