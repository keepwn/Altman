using System;
using System.Linq;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemModifyTime : Command
	{
		private Status _status;
		public ItemModifyTime(Status status)
		{
			ID = "modifyTime";
			MenuText = StrRes.GetString("StrModifyTime","ModifyTime");
			Executed += ItemModifyTime_Executed;

			_status = status;
		}

		void ItemModifyTime_Executed(object sender, EventArgs e)
		{
			if (_status.FileGridView.SelectedRows.Any())
			{
				var row = _status.FileGridView.SelectedRows.First();
				_status.FileGridView.BeginEdit(row, 2);
			}
		}
	}
}