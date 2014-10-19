using System;
using System.Linq;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemCopy : Command
	{
		private Status _status;
		public ItemCopy(Status status)
		{
			ID = "copy";
			MenuText = StrRes.GetString("StrCopy","Copy");
			Executed += ItemCopy_Executed;

			_status = status;
		}

		void ItemCopy_Executed(object sender, EventArgs e)
		{
			FileInfoView selected = null;
			if ((selected = _status.FileGridView.SelectedItem as FileInfoView)!=null)
			{
				_status.CopyFileFullPath = selected.FullName;
				_status.CopyFileName = selected.Name;
			}
		}
	}
}