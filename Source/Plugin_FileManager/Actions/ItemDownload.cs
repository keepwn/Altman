using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemDownload : Command
	{
		public ItemDownload()
		{
			ID = "download";
			MenuText = "Download";
			Executed += ItemDownload_Executed;
		}

		void ItemDownload_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}