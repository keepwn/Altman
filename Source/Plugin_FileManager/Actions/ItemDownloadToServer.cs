using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemDownloadToServer : Command
	{
		public ItemDownloadToServer()
		{
			ID = "downloadToServer";
			MenuText = "DownloadToServer";
			Executed += ItemDownloadToServer_Executed;
		}

		void ItemDownloadToServer_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}