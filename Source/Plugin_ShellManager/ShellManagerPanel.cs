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
	public partial class ShellManagerPanel : Panel
	{
		private IHost _host;
		private Shell _shellData;
		private ShellManagerService _shellManagerService = null;

		public ShellManagerPanel(IHost host, Shell data)
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
					/*
					var pluginCommand = new Command();
					pluginCommand.ID = title;
					pluginCommand.MenuText = title;
					pluginCommand.Tag = plugin;
					pluginCommand.Executed += pluginCommand_Executed;
					*/
					var pluginItem = new ButtonMenuItem();
					pluginItem.ID = title;
					pluginItem.Text = title;
					pluginItem.Click += pluginItem_Click;
					pluginItem.Tag = plugin;

					_rightMenuWebshell.Items.Add(pluginItem);
				}
			}
		}

		void pluginItem_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				var item = sender as MenuItem;
				if (item != null)
				{
					IPlugin plugin = item.Tag as IPlugin;

					Shell shell = (Shell)_gridViewShell.SelectedItem;
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

			_gridViewShell.DataStore = item;
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
		void _itemAdd_Click(object sender, EventArgs e)
		{
			var editwebshell = new FormEditWebshell(_host);
			editwebshell.WebshellWatchEvent += OnWebshellChange;
			editwebshell.Show();
		}
		void _itemEdit_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				Shell shell = (Shell)_gridViewShell.SelectedItem;

				FormEditWebshell editwebshell = new FormEditWebshell(_host, shell);
				editwebshell.WebshellWatchEvent += OnWebshellChange;
				editwebshell.Show();
			}
		}
		void _itemDelete_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				int id = int.Parse(((Shell)_gridViewShell.SelectedItem).Id);
				_shellManagerService.Delete(id);
				LoadWebshellData();
			}
		}
		void _itemCopyServerCode_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				var shell = _gridViewShell.SelectedItem as Shell;
				string code = _host.Core.GetCustomShellTypeServerCode(shell.ShellType);

				if (string.IsNullOrWhiteSpace(code))
				{
					MessageBox.Show("ServerCode is NULL!");
				}
				new Clipboard().Text = code;
			}
		}
		#endregion

		#region 批量检测shell状态
		void _itemRefreshStatus_Click(object sender, EventArgs e)
		{
			RefreshAllStatus();
		}
		private void RefreshAllStatus()
		{
			if (_gridViewShell.DataStore == null) return;
			foreach (var item in _gridViewShell.DataStore as DataStoreCollection)
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
				RefreshShellStatusInListView(shell, "-1");
			}
			else
			{
				RefreshShellStatusInListView(shell, Convert.ToInt32(myResponse.StatusCode).ToString());
			}
		}

		private void RefreshShellStatusInListView(Shell item, string status)
		{
			item.Status = status;
			_shellManagerService.Update(int.Parse(item.Id), item);
		}
		#endregion

		void _gridViewShell_CellDoubleClick(object sender, GridViewCellEventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				//如果存在FileManager插件，则双击进入FileManager
				var item = _rightMenuWebshell.Items.FirstOrDefault(i => i.ID == "FileManager");
				if (item != null)
				{
					pluginItem_Click(item, null);
				}
			}
		}
	}
}
