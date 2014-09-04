using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemRefresh : Command
	{
		private Model.Status _status;
		public ItemRefresh(Model.Status status)
		{
			ID = "refresh";
			MenuText = "Refresh";
			Executed += ItemRefresh_Executed;

			_status = status;
		}

		void ItemRefresh_Executed(object sender, EventArgs e)
		{
			_status.FileManager.GetFileTree(_status.CurrentDirPath);
		}
	}
}
