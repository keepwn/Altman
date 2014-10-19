using System;
using System.Linq;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemRename : Command
	{
		private Status _status;
		public ItemRename(Status status)
		{
			ID = "rename";
			MenuText = StrRes.GetString("StrRename","Rename");
			Executed += ItemRename_Executed;

			_status = status;
		}

		void ItemRename_Executed(object sender, EventArgs e)
		{
			if (_status.FileGridView.SelectedRows.Any())
			{
				var row = _status.FileGridView.SelectedRows.First();
				_status.FileGridView.BeginEdit(row, 1);
			}
		}
	}
}