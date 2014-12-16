using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Webshell.Model;
using Eto.Forms;
using Plugin_ShellManager.Data;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager.Interface
{
	public partial class PanelShellManager : Panel
	{
		public PanelShellManager(PluginParameter data)
		{
			InitUi();

			// 注册事件
			ShellManager.GetDataTableCompletedToDo += ShellManagerGetDataTableCompletedToDo;
			ShellManager.DeleteCompletedToDo += ShellManagerDeleteCompletedToDo;
			ShellManager.InsertCompletedToDo += ShellManagerInsertCompletedToDo;
			ShellManager.UpdateCompletedToDo += ShellManagerUpdateCompletedToDo;

			// 载入shell数据
			LoadWebshellData();

			// 添加插件到右键菜单
			foreach (var plugin in PluginProvider.GetPlugins())
			{
				// IsShowInRightContext
				if (plugin.PluginSetting.LoadPath.ToLower() == "shellmanager")
				{
					string title = plugin.PluginInfo.Name;

					// 添加到Tsmi_Plugins中
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
					var plugin = item.Tag as IPlugin;

					var shell = (Shell)_gridViewShell.SelectedItem;
					shell.TimeOut = 8000;

					var param = new PluginParameter();
					param.AddParameter("shell", shell);

					if (plugin is IControlPlugin)
					{
						object view = (plugin as IControlPlugin).Show(param);
						//创建新的tab标签
						//设置标题为FileManager|TargetId
						string title = plugin.PluginInfo.Name + "|" + shell.TargetId;
						ShellManager.Host.Ui.OpenTabPage(title, view);
					}
					else if (plugin is IFormPlugin)
					{
						var form = (Form)(plugin as IFormPlugin).Show(param);
						form.Show();
					}
				}
			}
		}

		/// <summary>
		/// 载入webshell数据
		/// </summary>
		public void LoadWebshellData()
		{
			DataTable dataTable = ShellManager.GetDataTable();
			if (dataTable == null)
			{
				return;
			}
			var item = new DataStoreCollection<Shell>();
			foreach (DataRow row in dataTable.Rows)
			{
				Shell shell = DataConvert.ConvertDataRowToShellStruct(row);
				item.Add(shell);
			}

			_gridViewShell.DataStore = item;
		}

		#region 重排序
		public static int SortIntAscending(string x, string y, bool isAscending)
		{
			if (string.IsNullOrWhiteSpace(x))
			{
				if (string.IsNullOrWhiteSpace(y))
					return 0;
				return isAscending ? - 1 : 1;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(y))
				{
					return isAscending ? 1 : -1;
				}
				try
				{
					var a = Int32.Parse(x);
					var b = Int32.Parse(y);
					return isAscending ? (a - b) : -(a - b);
				}
				catch
				{
					return 0;
				}
			}
		}
		public static int SortIntAscending(int x, int y, bool isAscending)
		{
			return isAscending ? (x - y) : -(x - y);
		}
		public static int SortStringAscending(string x, string y, bool isAscending)
		{
			if (isAscending)
				return String.Compare(x, y, StringComparison.Ordinal);
			return -String.Compare(x, y, StringComparison.Ordinal);
		}
		public static int SortTimeAscending(string x, string y, bool isAscending)
		{
			try
			{
				var a = DateTime.Parse(x);
				var b = DateTime.Parse(y);
				return isAscending ? DateTime.Compare(a, b) : -DateTime.Compare(a, b);
			}
			catch
			{
				return 0;
			}
		}

		private bool _idIsAscending;
		private bool _nameIsAscending;
		private bool _levelIsAscending;
		private bool _statusIsAscending;
		private bool _typeIsAscending;
		private bool _addTimeIsAscending;
		void _gridViewShell_ColumnHeaderClick(object sender, GridColumnEventArgs e)
		{
			var items = _gridViewShell.DataStore as DataStoreCollection<Shell>;
			if (items == null) return;
			switch (e.Column.HeaderText)
			{
				case "Id":
					_idIsAscending = !_idIsAscending;
					items.Sort((a, b) => SortIntAscending(a.Id, b.Id, _idIsAscending));
					break;
				case "Name":
					_nameIsAscending = !_nameIsAscending;
					items.Sort((a, b) => SortStringAscending(a.TargetId, b.TargetId, _nameIsAscending));
					break;
				case "Level":
					_levelIsAscending = !_levelIsAscending;
					items.Sort((a, b) => SortStringAscending(a.TargetLevel, b.TargetLevel, _levelIsAscending));
					break;
				case "Status":
					_statusIsAscending = !_statusIsAscending;
					items.Sort((a, b) => SortIntAscending(a.Status, b.Status, _statusIsAscending));
					break;
				case "Type":
					_typeIsAscending = !_typeIsAscending;
					items.Sort((a, b) => SortStringAscending(a.ShellType, b.ShellType, _typeIsAscending));
					break;
				case "AddTime":
					_addTimeIsAscending = !_addTimeIsAscending;
					items.Sort((a, b) => SortTimeAscending(a.AddTime, b.AddTime, _addTimeIsAscending));
					break;
				default:
					break;
			}
		}
		#endregion

		#region 数据获取/插入/删除/更新事件
		private void ShellManagerUpdateCompletedToDo(object sender, ShellManager.CompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
		}
		private void ShellManagerInsertCompletedToDo(object sender, ShellManager.CompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
		}
		private void ShellManagerDeleteCompletedToDo(object sender, ShellManager.CompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
		}
		private void ShellManagerGetDataTableCompletedToDo(object sender, ShellManager.CompletedEventArgs e)
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
			var editwebshell = new FormEditWebshell();
			editwebshell.WebshellWatchEvent += OnWebshellChange;
			editwebshell.Show();
		}
		void _itemEdit_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				// only get first row
				Shell shell = (Shell)_gridViewShell.SelectedItem;

				FormEditWebshell editwebshell = new FormEditWebshell(shell);
				editwebshell.WebshellWatchEvent += OnWebshellChange;
				editwebshell.Show();
			}
		}
		void _itemDelete_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				// can multi-row
				foreach (var item in _gridViewShell.SelectedItems.OfType<Shell>())
				{
					int id = int.Parse(item.Id);
					ShellManager.Delete(id);
				}
				LoadWebshellData();
			}
		}
		void _itemCopyServerCode_Click(object sender, EventArgs e)
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				// only get first row
				var shell = _gridViewShell.SelectedItem as Shell;
				string code = Altman.Webshell.Service.GetCustomShellTypeServerCode(shell.ShellType);

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
			RefreshStatusOfSelectedRows();
		}
		private void RefreshStatusOfSelectedRows()
		{
			if (_gridViewShell.SelectedItems.Any())
			{
				// can multi-row
				foreach (var item in _gridViewShell.SelectedItems.OfType<Shell>())
				{
					Thread thread = new Thread(RefreshStatus);
					thread.Start(item);
				}
			}
			else
			{
				MessageBox.Show(_gridViewShell, "please select at least one shell");
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
			// update status to database
			item.Status = status;
			ShellManager.Update(int.Parse(item.Id), item);
			// refresh
			Application.Instance.Invoke(LoadWebshellData);
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
