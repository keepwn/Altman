using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Altman.Util.Common.AltEventArgs;
using Eto.Forms;
using Plugin_FileManager.Model;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Actions
{
	public class ItemUpload : Command
	{
		private Status _status;
		public ItemUpload(Status status)
		{
			ID = "upload";
			MenuText = StrRes.GetString("StrUpload","Upload");
			Executed += ItemUpload_Executed;

			_status = status;
		}
		void ItemUpload_Executed(object sender, EventArgs e)
		{
			var openFileDialog = new OpenFileDialog
			{
				Title = "Select File To Upload",
				Filters =
				{
                    new FileDialogFilter("All Files", ".*"),
					new FileDialogFilter("Script Files", ".asp", ".aspx", ".php", ".jsp"),
					new FileDialogFilter("Program", ".exe", ".bat")		
				}
			};
			if (openFileDialog.ShowDialog(_status.FileGridView) == DialogResult.Ok)
			{
				string srcfile = openFileDialog.FileName;
				string currentDirPath = _status.CurrentDirPath;
				string fileName = Path.GetFileName(srcfile);
				string targetFilePath = Path.Combine(currentDirPath, fileName);
				UploadFile(srcfile, targetFilePath);
			}
		}

		private void UploadFile(string sourceFilePath, string targetFilePath)
		{
			try
			{
				//判断窗体是否已经被关闭
				//if (trafficManager == null || trafficManager.IsDisposed)
				//{
				//    trafficManager = new FormTrafficManager();
				//}
				//ControlProgressBar progressBar = ShowProgressBar(true, sourceFilePath, targetFilePath);
				//FileUploadOrDownload upload = new FileUploadOrDownload(_shellData, sourceFilePath, targetFilePath);
				//upload.UploadFileProgressChangedToDo += upload_UploadFileProgressChangedToDo;
				//upload.UploadFileCompletedToDo += upload_UploadFileCompletedToDo;
				//upload.StartToUploadFile();

				var upload = new FileUploadOrDownload(_status.Host, _status.ShellData, sourceFilePath, targetFilePath);
				upload.UploadFileProgressChangedToDo += upload_UploadFileProgressChangedToDo;
				upload.UploadFileCompletedToDo += upload_UploadFileCompletedToDo;
				upload.StartToUploadFile();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		private void upload_UploadFileProgressChangedToDo(object sender, AltProgressChangedEventArgs e)
		{
			//ControlProgressBar progressBar = e.UserState as ControlProgressBar;
			//progressBar.Value = e.ProgressPercentage;
		}
		private void upload_UploadFileCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
		{
            _status.Host.Ui.ShowMsgInStatusBar("", false);
			if (e.Error != null)
			{
				//ShowResultInProgressBar(false, e);
				_status.Host.Ui.ShowMsgInStatusBar(e.Error.Message);
			}
			else if (e.Result is bool)
			{
				string msg = string.Empty;
				if (!(bool)e.Result)
				{
					msg = "Failed to upload file";
				}
				else
				{
					msg = "Upload file succeed";
				}
				_status.Host.Ui.ShowMsgInStatusBar(msg);
				_status.Host.Ui.ShowMsgInAppDialog(msg);

				//ShowResultInProgressBar(true, e);
				//RefreshAllFiles(GetCurrentDirPath());
				_status.FileManager.GetFileTree(_status.CurrentDirPath);
			}
		}
	}
}