using System;
using System.Linq;
using Eto.Forms;
using Plugin_FileManager.Model;

namespace Plugin_FileManager.Actions
{
	public class ItemCopy : Command
	{
		private Status _status;
		public ItemCopy(Status status)
		{
			ID = "copy";
			MenuText = "Copy";
			Executed += ItemCopy_Executed;

			_status = status;
		}

		void ItemCopy_Executed(object sender, EventArgs e)
		{
			FileInfo selected = null;
			if ((selected = _status.FileGridView.SelectedItem as FileInfo)!=null)
			{
				_status.CopyFileFullPath = selected.FullName;
				_status.CopyFileName = selected.Name;
			}
		}
	}
}