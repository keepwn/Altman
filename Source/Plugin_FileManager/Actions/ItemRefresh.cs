using System;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemRefresh : Command
	{
		private Status _status;
		public ItemRefresh(Status status)
		{
			ID = "refresh";
			MenuText = StrRes.GetString("StrRefresh","Refresh");
			Executed += ItemRefresh_Executed;

			_status = status;
		}

		void ItemRefresh_Executed(object sender, EventArgs e)
		{
			_status.FileManager.GetFileTree(_status.CurrentDirPath);
		}
	}
}
