using System;
using Eto.Forms;
using Plugin_FileManager.Model;

namespace Plugin_FileManager.Actions
{
	public class ItemCreateDir : Command
	{
		private Status _status;
		public ItemCreateDir(Status status)
		{
			ID = "createDir";
			MenuText = "CreateDir";
			Executed += ItemCreateDir_Executed;

			_status = status;
		}

		void ItemCreateDir_Executed(object sender, EventArgs e)
		{
			string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var newItem = new FileInfo("NewFolder", "NewFolder", true, time, "0", "0777");

			var items = _status.FileGridView.DataStore as DataStoreCollection;			
			items.Add(newItem);

			var row = items.IndexOf(newItem);
			_status.FileGridView.BeginEdit(row, 1);
		}
	}
}