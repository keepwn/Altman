using System;
using Eto.Forms;
using Plugin_FileManager.Interface;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemEdit : Command
	{
		private Status _status;
		public ItemEdit(Status status)
		{
			ID = "edit";
			MenuText = StrRes.GetString("StrEdit","Edit");
			Executed += ItemEdit_Executed;

			_status = status;
		}

		void ItemEdit_Executed(object sender, EventArgs e)
		{
			var selectFile =  _status.FileGridView.SelectedItem as FileInfoView;
			if (selectFile != null && !selectFile.IsDir)
			{
				var webFile = selectFile.FullName;
				var fileEditer = new FileEditerPanel(_status.Host,_status.ShellData, webFile, true);
				_status.Host.Ui.OpenTabPage("FileEdit", fileEditer);
			}
		}
	}
}