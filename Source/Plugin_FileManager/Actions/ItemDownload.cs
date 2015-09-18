using System;
using System.ComponentModel;
using System.IO;
using Altman.Util.Common.AltEventArgs;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemDownload : Command
	{
		private Status _status;
		public ItemDownload(Status status)
		{
			MenuText = StrRes.GetString("StrDownload","Download");
			Executed += ItemDownload_Executed;

			_status = status;
		}

		void ItemDownload_Executed(object sender, EventArgs e)
		{
			var selectFile = _status.FileGridView.SelectedItem as FileInfoView;
			if (selectFile != null)
			{
				var webFile = selectFile.FullName;
				var name = Path.GetFileName(webFile);
				var saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Save File As";
				saveFileDialog.FileName = name;
				if (DialogResult.Ok == saveFileDialog.ShowDialog(_status.FileGridView))
				{
					DownloadFile(webFile, saveFileDialog.FileName);
				}
			}
		}

		/// <summary>
		/// 下载文件
		/// </summary>
		private void DownloadFile(string sourceFilePath, string targetFilePath)
		{
			try
			{
				var download = new FileUploadOrDownload(_status.Host, _status.ShellData, sourceFilePath, targetFilePath);
				download.DownloadFileProgressChangedToDo += download_DownloadFileProgressChangedToDo;
				download.DownloadFileCompletedToDo += download_DownloadFileCompletedToDo;
				download.StartToDownloadFile();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		private void download_DownloadFileProgressChangedToDo(object sender, AltProgressChangedEventArgs e)
		{
			//ShowPercentageInProgressBar(e);
		}
		private void download_DownloadFileCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
             _status.Host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				//ShowResultInProgressBar(false, e);
                _status.Host.Ui.ShowMsgInStatusBar(e.Error.Message);
				MessageBox.Show(e.Error.Message);
			}
			else
			{
				//ShowResultInProgressBar(true,e);
				string msg = "Download file succeed";
				_status.Host.Ui.ShowMsgInStatusBar(msg);
			}
		}
	}
}