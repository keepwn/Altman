using System;
using Eto.Forms;
using Plugin_FileManager.Model;

namespace Plugin_FileManager.Actions
{
	public class ItemPaste : Command
	{
		private Status _status;
		public ItemPaste(Status status)
		{
			ID = "paste";
			MenuText = "Paste";
			Executed += ItemPaste_Executed;

			_status = status;
		}

		void ItemPaste_Executed(object sender, EventArgs e)
		{
			FileInfo selected = null;
			if ((selected = _status.FileGridView.SelectedItem as FileInfo) != null)
			{
				var sourceFullName = _status.CopyFileFullPath;
				var targetDir = _status.CurrentDirPath;
				var targetFullName = targetDir + _status.PathSeparator + _status.CopyFileName;
				if (targetFullName != sourceFullName)
				{
					_status.FileManager.CopyFileOrDir(sourceFullName, targetFullName);
					//clear
					_status.CopyFileFullPath = string.Empty;
					_status.CopyFileName = string.Empty;
				}
			}


		}
	}
}