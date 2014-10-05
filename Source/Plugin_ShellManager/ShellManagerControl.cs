using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_ShellManager
{
	public class ShellManagerControl : Panel
	{
		private IHost _host;
		private Shell _shellData;
		private ShellManagerService _shellManagerService = null;


		private ContextMenu rightMenu_Webshell;
		private GridView lv_shell;
		/*
		private ToolStripMenuItem item_refreshStatus;
		private ToolStripMenuItem item_copyServerCode;
		*/
		public ShellManagerControl(IHost host, Shell data)
		{
			Init();

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
				if (plugin.PluginSetting.LoadPath == "webshell" && plugin.PluginSetting.IsShowInRightContext)
				{
					string title = plugin.PluginInfo.Name;

					//添加到Tsmi_Plugins中
					var pluginCommand = new Command();
					pluginCommand.ID = title;
					pluginCommand.MenuText = title;
					pluginCommand.Tag = plugin;
					pluginCommand.Executed += pluginCommand_Executed;
					/*
					var pluginItem = new ButtonMenuItem();
					pluginItem.ID = title;
					pluginItem.Text = title;
					pluginItem.Click += pluginItem_Click;
					pluginItem. = plugin;
					 */
					rightMenu_Webshell.Items.Add(pluginCommand);
				}
			}
		}

		void pluginCommand_Executed(object sender, EventArgs e)
		{
			if (lv_shell.SelectedItems.Any())
			{
				var item = sender as Command;
				if (item != null)
				{
					IPlugin plugin = item.Tag as IPlugin;

					Shell shell = (Shell)lv_shell.SelectedItem;
					shell.TimeOut = 8000;

					if (plugin is IControlPlugin)
					{
						object view = (plugin as IControlPlugin).GetUi(shell);
						//创建新的tab标签
						//设置标题为FileManager|TargetId
						string title = plugin.PluginInfo.Name + "|" + shell.TargetId;
						_host.Ui.CreateNewTabPage(title, view);
					}
					else if (plugin is IFormPlugin)
					{
						Form form = (Form)(plugin as IFormPlugin).GetUi(shell);
						form.Show();
					}
				}
			}
		}
		void Init()
		{
			//rightMenu_Webshell
			rightMenu_Webshell = new ContextMenu();

			var item_add = new ButtonMenuItem {ID="menuitemAdd", Text = "Add" };
			item_add.Click += item_add_Click;
			var item_edit = new ButtonMenuItem { Text = "Edit" };
			item_edit.Click += item_edit_Click;
			var item_delete = new ButtonMenuItem { Text = "Delete" };
			item_delete.Click += item_delete_Click;
			rightMenu_Webshell.Items.Add(item_add);
			rightMenu_Webshell.Items.Add(item_edit);
			rightMenu_Webshell.Items.Add(item_delete);
			rightMenu_Webshell.Items.Add(new SeparatorMenuItem());

			//lv_shell
			lv_shell = CreateListView();
			lv_shell.ContextMenu = rightMenu_Webshell;
			lv_shell.MouseUp += (sender, e) =>
			{
				if (e.Buttons == MouseButtons.Alternate)
				{
					lv_shell.ContextMenu.Show(lv_shell);
				}
			};

			var layout = new DynamicLayout { Padding = new Padding(0), Spacing = new Size(10, 10) };
			layout.Add(lv_shell);

			//this.ContextMenu = rightMenu_Webshell;
			this.Content = layout;
		}

		GridView CreateListView()
		{
			//_gridViewHeader
			var gridView = new GridView()
			{
				AllowMultipleSelection = false,
				BackgroundColor = Colors.White,
				ShowCellBorders = false,
			};
			//Id
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Id",
				DataCell = new TextBoxCell("Id"),
				Editable = false,
				Sortable = true,
				AutoSize = true
			});
			//Name
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Name",
				DataCell = new TextBoxCell("TargetId"),
				Editable = true,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			//Level
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Level",
				DataCell = new TextBoxCell("TargetLevel"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 50
			});
			//Status
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Status",
				DataCell = new TextBoxCell("Status"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 50
			});
			//ShellUrl
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "ShellUrl",
				DataCell = new TextBoxCell("ShellUrl"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 250
			});
			//Type
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Type",
				DataCell = new TextBoxCell("ShellType"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			//Remark
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "Remark",
				DataCell = new TextBoxCell("Remark"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			//AddTime
			gridView.Columns.Add(new GridColumn
			{
				HeaderText = "AddTime",
				DataCell = new TextBoxCell("AddTime"),
				Editable = false,
				Sortable = true,
				AutoSize = false,
				Width = 100
			});
			return gridView;
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


		public class ShellDataView : Shell
		{
			//public string ID;//用于定位listview
			public ShellDataView(Shell shell)
			{
				this.Id = shell.Id;
				this.TargetId = shell.TargetId;
				this.TargetLevel = shell.TargetLevel;
				this.Status = shell.Status;

				this.ShellUrl = shell.ShellUrl;
				this.ShellPwd = shell.ShellPwd;
				this.ShellType = shell.ShellType;
				this.ShellExtraString = shell.ShellExtraString;

				this.ServerCoding = shell.ServerCoding;
				this.WebCoding = shell.WebCoding;

				this.TimeOut = shell.TimeOut;

				this.Area = shell.Area;
				this.Remark = shell.Remark;
				this.AddTime = shell.AddTime;
			}
		}


		/// <summary>
		/// 载入webshell数据
		/// </summary>
		public void LoadWebshellData()
		{
			DataTable dataTable = _shellManagerService.GetDataTable();
			if (dataTable == null)
			{
				return;
			}
			var item = new DataStoreCollection();
			foreach (DataRow row in dataTable.Rows)
			{
				Shell shell = ConvertDataRowToShellStruct(row);
				item.Add(shell);
			}

			lv_shell.DataStore = item;
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
		private void item_edit_Click(object sender, EventArgs e)
		{
			if (lv_shell.SelectedItems.Any())
			{
				Shell shell = (Shell)lv_shell.SelectedItem;

				FormEditWebshell editwebshell = new FormEditWebshell(_host, shell);
				editwebshell.WebshellWatchEvent += OnWebshellChange;
				editwebshell.Show();
			}
		}
		private void item_delete_Click(object sender, EventArgs e)
		{
			if (lv_shell.SelectedItems.Any())
			{
				int id = int.Parse(((Shell)lv_shell.SelectedItem).Id);
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
			if (lv_shell.DataStore == null)
				return;
			foreach (var item in lv_shell.DataStore as DataStoreCollection)
			{
				Thread thread = new Thread(RefreshStatus);
				thread.Start(item);
			}
		}
		private void RefreshStatus(object shellData)
		{
			var shell = shellData as Shell;
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
				//RefreshShellStatusInListView(item, "-1");
			}
			else
			{
				//RefreshShellStatusInListView(item, Convert.ToInt32(myResponse.StatusCode).ToString());
			}
		}

		//public delegate void RefreshShellStatusInvoke(ListViewItem item, string status);
		//private void RefreshShellStatusInListView(ListViewItem item, string status)
		//{
		//    //等待异步
		//    if (this.InvokeRequired)
		//    {
		//        RefreshShellStatusInvoke invoke = new RefreshShellStatusInvoke(RefreshShellStatusInListView);
		//        this.Invoke(invoke, new object[] { item, status });
		//    }
		//    else
		//    {
		//        item.SubItems[4].Text = status;

		//        Shell shell = (Shell)item.Tag;
		//        shell.Status = status;
		//        _shellManagerService.Update(int.Parse(shell.Id), shell);
		//    }
		//}
		#endregion

		private void lv_shell_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (lv_shell.SelectedItems.Any())
			{
				//如果存在FileManager插件，则双击进入FileManager
				if (rightMenu_Webshell.Items.Any(item => item.Text == "FileManager"))
				{
					//ToolStripItem item = rightMenu_Webshell.Items["FileManager"];
					//pluginItem_Click(item, null);
				}
			}
		}

		private void item_copyServerCode_Click(object sender, EventArgs e)
		{
			if (lv_shell.SelectedItems.Any())
			{
				var shell = lv_shell.SelectedItem as Shell;
				string code = _host.Core.GetCustomShellTypeServerCode(shell.ShellType);

				if (string.IsNullOrWhiteSpace(code))
				{
					MessageBox.Show("ServerCode is NUll!");
				}
				//Clipboard.SetDataObject(code);
				new Clipboard().Text = code;
			}
		}
	}
}
