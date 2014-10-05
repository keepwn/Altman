using System;
using Eto.Forms;
using Plugin_FileManager.Interface;
using Plugin_FileManager.Model;

namespace Plugin_FileManager.Actions
{
	public class ItemEdit : Command
	{
		private Status _status;
		public ItemEdit(Status status)
		{
			ID = "edit";
			MenuText = "Edit";
			Executed += ItemEdit_Executed;

			_status = status;
		}

		void ItemEdit_Executed(object sender, EventArgs e)
		{
			var selectFile =  _status.FileGridView.SelectedItem as FileInfo;
			if (selectFile != null && !selectFile.IsDir)
			{
				var webFile = selectFile.FullName;
				var fileEditer = new FileEditerSection(_status.Host,_status.ShellData, webFile, true);
				_status.Host.Ui.CreateNewTabPage("FileEdit", fileEditer);
			}
		}
	}
}