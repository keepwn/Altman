using System;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemDownloadToServer : Command
	{
		private Status _status;
		public ItemDownloadToServer(Status status)
		{
			ID = "downloadToServer";
			MenuText = StrRes.GetString("StrDownloadToServer","DownloadToServer");
			Executed += ItemDownloadToServer_Executed;

			_status = status;
		}

		void ItemDownloadToServer_Executed(object sender, EventArgs e)
		{
			var wget = new Dialogs.Wget(_status.CurrentDirPath);

			if (DialogResult.Ok == wget.ShowModal(_status.FileGridView))
			{
				string urlPath = wget.UrlPath;
				string savePath = wget.SavePath;

				_status.FileManager.Wget(urlPath, savePath);
			}

		}
	}
}