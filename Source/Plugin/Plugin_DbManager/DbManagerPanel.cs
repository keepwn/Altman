using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_DbManager
{
    public partial class DbManagerPanel : Panel
    {
        private IHost _host;
        private Shell _shellData;

        private DbManagerService dbManagerService;
		
        public DbManagerPanel(IHost host, Shell data)
        {
            Init();

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

			RefreshServerStatus(false);

			//连接数据库
			dbManagerService.ConnectDb(GetConnStr());
        }

        #region ServiceCompletedToDo
        private void dbManagerService_ExecuteNonQueryCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is DataTable)
            {
	            RefreshGridViewResult((e.Result as DataTable));
                ShowMsgInStatusBar((e.Result as DataTable).Rows[0][0].ToString());
            }
        }
        private void dbManagerService_ExecuteReaderCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is DataTable)
            {
				RefreshGridViewResult((e.Result as DataTable));

                ShowMsgInStatusBar(string.Format("{0} rows", (e.Result as DataTable).Rows.Count));
            }
        }
        private void dbManagerService_GetColumnTypeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowMsgInStatusBar(e.Error.Message);

                //更改table为失败的图标
				//_treeViewDbs.SelectedItem.Image = Icons.Database.TableFailedIcon;
            }
            else if (e.Result is string[])
            {
                string[] columns = e.Result as string[];
                if (columns.Length > 0)
                {
	                RefreshColumnsInDbTree((TreeItem) _treeViewDbs.SelectedItem, columns);
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
	            //_treeViewDbs.SelectedItem.Image = Icons.Database.DatabaseFailedIcon;
            }
            else if (e.Result is string[])
            {
                var tables = e.Result as string[];
                if (tables.Length > 0)
                {
	                RefreshTablesInDbTree((TreeItem) _treeViewDbs.SelectedItem, tables);
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
                var dbs = e.Result as string[];
                if (dbs.Length > 0)
                {
					RefreshDbsInDbTree((TreeItem)_treeViewDbs.SelectedItem, dbs);
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
	    private void RefreshGridViewResult(DataTable dataTable)
	    {
			var i = 0;
			foreach (var col in dataTable.Columns.Cast<DataColumn>())
			{
				_gridViewResult.Columns.Add(new GridColumn()
				{
					DataCell = new TextBoxCell(i++),
					HeaderText = col.ColumnName
				});
			}

			var tmp = dataTable.Rows.Cast<DataRow>().Select(x => x.ItemArray.Select(y => y.ToString()).ToArray());
			_gridViewResult.DataStore = tmp;			
	    }
        private void RefreshServerStatus(bool isConnected)
        {
            if (isConnected)
            {
	            _buttonConnect.Enabled = false;
				_buttonDisconnect.Enabled = true;
            }
            else
            {
				_buttonConnect.Enabled = true;
				_buttonDisconnect.Enabled = false;
            }
        }
		private void RefreshRootInDbTree(bool isSuccess)
		{
			var	rootTreeItem = new TreeItem();
			var node = new TreeItem
			{
				Text = "(local)",
				Tag = "root",
				Image = isSuccess ? Icons.Database.DatabaseStartIcon : Icons.Database.DatabaseFailedIcon
			};
			rootTreeItem.Children.Add(node);
			_treeViewDbs.DataStore = rootTreeItem;
		}
		private void RefreshDbsInDbTree(TreeItem selected, string[] dbs)
		{
			selected.Children.Clear();
            foreach (var db in dbs)
            {
	            var node = new TreeItem
	            {
		            Text = db,
					Tag = "db",
					Image = Icons.Database.DatabaseIcon
	            };
	            selected.Children.Add(node);
            }
			selected.Expanded = true;
			_treeViewDbs.RefreshItem(selected);
            //刷新数据库选择框
			_comboboxDbs.Items.Clear();
			_comboboxDbs.Items.AddRange(dbs.Select(db => new ListItem { Text = db, Key = db }));
        }
        private void RefreshTablesInDbTree(TreeItem selected, string[] tables)
        {
	        selected.Children.Clear();
            foreach (var table in tables)
            {
				var node = new TreeItem
				{
					Text = table,
					Tag = "table",
					Image = Icons.Database.TableIcon
				};
				selected.Children.Add(node);
            }
			selected.Expanded = true;
			_treeViewDbs.RefreshItem(selected);
        }
		private void RefreshColumnsInDbTree(TreeItem selected, string[] columns)
        {
			selected.Children.Clear();
            foreach (var column in columns)
            {
				var node = new TreeItem
				{
					Text = column,
					Tag = "column",
					Image = Icons.Database.ColumnIcon
				};
				selected.Children.Add(node);
            }
			selected.Expanded = true;
			_treeViewDbs.RefreshItem(selected);
        }
        #endregion

        #region _treeViewDbs Event
		void _treeViewDbs_Activated(object sender, TreeViewItemEventArgs e)
		{
			if (e.Item == null) return;
			var name = e.Item.Text;
			var type = (string)((e.Item as TreeItem).Tag ?? "");
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
					var dbname = e.Item.Parent.Text;

					dbManagerService.GetColumnType(GetConnStr(), dbname, name);
				}
			}
		}

		void _treeViewDbs_SelectionChanged(object sender, EventArgs e)
		{
			if (_treeViewDbs.SelectedItem == null) return;
			var select = _treeViewDbs.SelectedItem as TreeItem;
			var type = (string)(select.Tag ?? "");
			if (type == "db")
			{
				_comboboxDbs.SelectedKey = select.Key;
			}
		}
        #endregion

        #region Button Event
        /// <summary>
        /// 连接数据库
        /// </summary>
		void ButtonConnect_Click(object sender, EventArgs e)
		{
			dbManagerService.ConnectDb(GetConnStr());
		}
        /// <summary>
        /// 断开数据库
        /// </summary>
		void _buttonDisconnect_Click(object sender, EventArgs e)
        {
			RefreshServerStatus(false);
			RefreshRootInDbTree(false);
		}
        /// <summary>
        /// 执行sql语句
        /// </summary>
		void _buttonRunScript_Click(object sender, EventArgs e)
		{
			var sql = _textAreaSql.Text;
			if (string.IsNullOrWhiteSpace(sql))
			{
				MessageBox.Show("the query sql cannot be empty");
			}
			else if (_comboboxDbs.SelectedIndex == -1)
			{
				MessageBox.Show("please select one database");
			}
			else
			{
				//执行前先清空之前结果
				_gridViewResult.DataStore = null;
				_gridViewResult.Columns.Clear();

				var dbName = _comboboxDbs.SelectedKey;
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

        #region RightMenu Event
		private void MenuDbViewLoad_Opening()
		{
			if (_treeViewDbs.SelectedItem == null) return;
			var select = _treeViewDbs.SelectedItem as TreeItem;
			string name = select.Text;
			string type = (string)(select.Tag ?? "");
            if (name != "")
            {
				if (type == "table")
                {
	                _menuDbView.Items.First(r => r.Text == "ViewTable").Enabled = true;

					var dbname = select.Parent.Text;
                }
                else
                {
					_menuDbView.Items.First(r => r.Text == "ViewTable").Enabled = false;
                }
            }
		}
        private void SaveAsCsv(DataTable dt, string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs, Encoding.Default);
	        var data = string.Empty;

            //columns name
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName;
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);

            //rows data
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (var j = 0; j < dt.Columns.Count; j++)
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
		void _itemSaveAsCsv_Click(object sender, EventArgs e)
		{
			object dataSource = _gridViewResult.DataStore;
			if (dataSource != null)
			{
				var saveFileDialog = new SaveFileDialog();
				var filters = new List<IFileDialogFilter> {new FileDialogFilter("CSV|*.CSV")};
				saveFileDialog.Filters = filters;
				if (DialogResult.Ok == saveFileDialog.ShowDialog(_gridViewResult))
				{
					var fileName = saveFileDialog.FileName;
					SaveAsCsv(dataSource as DataTable, fileName);
				}
			}
		}
		void _itemCopyName_Click(object sender, EventArgs e)
		{
			if (_treeViewDbs.SelectedItem == null) return;
			var cli = new Clipboard {Text = _treeViewDbs.SelectedItem.Text};
		}
		void _itemViewTable_Click(object sender, EventArgs e)
		{
			_host.Ui.ShowMsgInAppDialog("this function is not yet implemented");
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
                var scriptType = _shellData.ShellType;

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
