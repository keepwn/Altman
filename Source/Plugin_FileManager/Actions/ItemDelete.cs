using System;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemDelete : Command
	{
		private Status _status;
		public ItemDelete(Status status)
		{
			ID = "delete";
			MenuText = StrRes.GetString("StrDelete","Delete");
			Executed += ItemDelete_Executed;

			_status = status;
		}

		void ItemDelete_Executed(object sender, EventArgs e)
		{
			var selectFile = _status.FileGridView.SelectedItem as FileInfoView;
			if (selectFile != null)
			{
			    var msg = string.Format("Will delete {0}, to continue?", selectFile.Name);
                var result = MessageBox.Show(_status.FileGridView, msg, MessageBoxButtons.YesNo, MessageBoxType.Question);
                if (result == DialogResult.No)
                    return;

				string webDir = selectFile.FullName;
				//提前删除文件夹节点，如果删除失败可能会影响(待解决)
				//RemoveDirInDirTree(treeView_Dirs, webDir, _isWin);
				_status.FileManager.DeleteFileOrDir(webDir);

				//var item = _status.FileGridView.DataStore as DataStoreCollection;
				//item.RemoveAt(1);
			}
		}
	}
}