using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Webshell.Model;
using Eto.Forms;
using Plugin_DbManager.Resources;

namespace Plugin_DbManager.Interface
{
    public partial class PanelDbManager : Panel
    {
        private IHost _host;
        private Shell _shellData;

	    private struct ShellSqlConnection
	    {
		    public string type;
		    public string conn;
	    }

	    private ShellSqlConnection _shellSqlConn;

        private DbManager _dbManager;

        private DataTable _dataTableResult;
		
        public PanelDbManager(IHost host, PluginParameter data)
        {
			_host = host;
			_shellData = (Shell)data[0];
			_shellSqlConn = GetShellSqlConn();

			// init StrRes to translate string
			StrRes.SetHost(_host);
            Init();			

            //绑定事件
			_dbManager = new DbManager(_host, _shellData, _shellSqlConn.type);
			_dbManager.ConnectDbCompletedToDo += DbManagerConnectDbCompletedToDo;
			_dbManager.GetDbNameCompletedToDo += DbManagerGetDbNameCompletedToDo;
			_dbManager.GetDbTableNameCompletedToDo += DbManagerGetTableNameCompletedToDo;
			_dbManager.GetColumnTypeCompletedToDo += DbManagerGetColumnTypeCompletedToDo;
			_dbManager.ExecuteReaderCompletedToDo += DbManagerExecuteReaderCompletedToDo;
			_dbManager.ExecuteNonQueryCompletedToDo += DbManagerExecuteNonQueryCompletedToDo;

			RefreshServerStatus(false);


	        if (string.IsNullOrEmpty(_shellSqlConn.type) || string.IsNullOrEmpty(_shellSqlConn.conn))
	        {
		        MessageBox.Show("shell's sqlConnection is null or space");
	        }
	        else
	        {
				//连接数据库
				_dbManager.ConnectDb(_shellSqlConn.conn);
	        }
        }

        #region ServiceCompletedToDo
        private void DbManagerExecuteNonQueryCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
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
        private void DbManagerExecuteReaderCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
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
        private void DbManagerGetColumnTypeCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
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
        private void DbManagerGetTableNameCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
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
        private void DbManagerGetDbNameCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
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
        private void DbManagerConnectDbCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
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

	        _dataTableResult = dataTable;
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
			_dropDownDbs.Items.Clear();
			_dropDownDbs.Items.AddRange(dbs.Select(db => new ListItem { Text = db, Key = db }));
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
					_dbManager.GetDbName(_shellSqlConn.conn);
				}
				else if (type == "db")
				{
					_dbManager.GetTableName(_shellSqlConn.conn, name);
				}
				else if (type == "table")
				{
					var dbname = e.Item.Parent.Text;

					_dbManager.GetColumnType(_shellSqlConn.conn, dbname, name);
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
				_dropDownDbs.SelectedKey = select.Key;
			}
		}
        #endregion

        #region Button Event
        /// <summary>
        /// 连接数据库
        /// </summary>
		void ButtonConnect_Click(object sender, EventArgs e)
		{
			_dbManager.ConnectDb(_shellSqlConn.conn);
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
			else if (_dropDownDbs.SelectedIndex == -1)
			{
				MessageBox.Show("please select one database");
			}
			else
			{
				//执行前先清空之前结果
				_gridViewResult.DataStore = null;
				_gridViewResult.Columns.Clear();

				var dbName = _dropDownDbs.SelectedKey;
				if (sql.ToLower().StartsWith("select"))
				{
					_dbManager.ExecuteReader(_shellSqlConn.conn, dbName, sql);
				}
				else
				{
					_dbManager.ExecuteNonQuery(_shellSqlConn.conn, dbName, sql);
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
        private void SaveAs(DataTable dt, string fileName, string separator=",")
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
                    data += separator;
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
                        data += separator;
                    }
                }
                sw.WriteLine(data);
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("Save Ok!");
        }
		void _itemSaveAs_Click(object sender, EventArgs e)
		{
		    var data = _dataTableResult;
			if (data != null)
			{
				var saveFileDialog = new SaveFileDialog
				{
					Filters =
					{
					    new FileDialogFilter("CSV(Comma Separated Values)|*.csv"),
					    new FileDialogFilter("TSV(Tab Separated Values)|*.txt")
					}
				};
				if (DialogResult.Ok == saveFileDialog.ShowDialog(_gridViewResult))
				{
					var fileName = saveFileDialog.FileName;
				    switch (saveFileDialog.CurrentFilterIndex)
				    {
				        case 0:
				            SaveAs(data, fileName);
				            break;
				        case 1:
				            SaveAs(data, fileName, "\t");
				            break;
				    }
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

		private ShellSqlConnection GetShellSqlConn()
	    {
			var res = Altman.Webshell.Service.GetShellSqlConnection(_shellData);
			return new ShellSqlConnection {type = res[0]??"", conn = res[1]??""};
	    }

        private void ShowMsgInStatusBar(string msg)
        {
            _host.Ui.ShowMsgInStatusBar(msg);
        }
    }
}
