using System;
using System.Collections.Generic;
using System.Linq;
using Eto;
using Eto.Forms;
using Plugin_FileManager;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemCreateDir : Command
	{
		private Status _status;
		public ItemCreateDir(Status status)
		{
			ID = "createDir";
			MenuText = StrRes.GetString("StrCreateDir","CreateDir");
			Executed += ItemCreateDir_Executed;

			_status = status;
		}

		void ItemCreateDir_Executed(object sender, EventArgs e)
		{
			string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var newItem = new FileInfoView("NewFolder", "NewFolder", true, time, "0", "0777");
			newItem.IsCreateing = true;

			if (!Platform.Instance.IsWinForms)
			{
				var items = _status.FileGridView.DataStore as DataStoreCollection<FileInfoView>;
				items.Add(newItem);

				var row = items.IndexOf(newItem);
				_status.FileGridView.BeginEdit(row, 1);
			}
			else
			{
				var oldText = "NewFolder";
				var dir = new Dialogs.CreateDir(oldText);
				string result = dir.ShowModal(_status.FileGridView);

				if (!string.IsNullOrEmpty(result))
				{
					var newText = result;
					if (newText == oldText)
					{
						return;
					}
					if (GetOldFiles().FirstOrDefault(r => r == newText) != null)
					{
						MessageBox.Show("This name already exists, please rename");
						return;
					}
					//创建文件夹
					var currentDir = _status.CurrentDirPath;
					string dirName = newText;
					string dirFullPath = currentDir + _status.PathSeparator + dirName;
					_status.FileManager.CreateDir(dirFullPath);
				}
			}
		}

		private string[] GetOldFiles()
		{
			return _status.FileGridView.DataStore.Select(r => (r as FileInfoView).Name).ToArray();
		}
	}
}