using System;
using Eto.Forms;
using Plugin_FileManager.Model;

namespace Plugin_FileManager.Actions
{
	public class ItemCreateDir : Command
	{
		private Model.Status _status;
		public ItemCreateDir(Model.Status status)
		{
			ID = "createDir";
			MenuText = "CreateDir";
			Executed += ItemCreateDir_Executed;

			_status = status;
		}

		void ItemCreateDir_Executed(object sender, EventArgs e)
		{
			string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[] subItems = new string[] { "NewFolder", time, "0", "0777" };
			//ListViewItem item = new ListViewItem(subItems, 1);
			//listView_File.Items.Add(item);
			//listView_File.EditSubItem(item, 0, "createDir", true);

			var items = _status.FileGridView.DataStore as DataStoreCollection;

			var newItem = new FileInfo("NewDir", "NewDir", true, "", "", "");
			items.Add(newItem);

			var index = items.IndexOf(newItem);
			MessageBox.Show(_status.FileGridView.Columns[2].HeaderText);
		}
	}
}