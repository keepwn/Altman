using System;
using System.ComponentModel;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_FileManager.Interface
{
    public partial class FileEditerPanel : Panel
    {
        private FileManager _fileManager;

        private IHost _host;
        private Shell _shellData;

		public FileEditerPanel(IHost host, Shell data, string filePath, bool autoLoadContent)
		{
			Init();

            this._host = host;
            this._shellData = data;

            _fileManager = new FileManager(_host, _shellData);
            _fileManager.ReadFileCompletedToDo += fileManager_LoadFileContentCompletedToDo;
            _fileManager.WriteFileCompletedToDo += fileManager_SaveFileCompletedToDo;

            if (filePath != null)
            {
				Url = filePath;
                if (autoLoadContent)
                    LoadFileContent(filePath);  
            }
        }

	    string Body
	    {
			get { return _textAreaBody.Text; }
			set { _textAreaBody.Text = value; }
	    }

	    string Url
	    {
			get { return _textBoxUrl.Text; }
			set { _textBoxUrl.Text = value; }
	    }

		void _buttonSaveFile_Click(object sender, EventArgs e)
		{
			if (Url != null)
			{
				SaveFile(Url, Body);
			}
			else
			{
				MessageBox.Show("the url is null", MessageBoxType.Error);
			}
			
		}

		void _buttonReadFile_Click(object sender, EventArgs e)
		{
			if (Url != null)
			{
				LoadFileContent(Url);
			}
			else
			{
				MessageBox.Show("the url is null", MessageBoxType.Error);
			}
		}

        /// <summary>
        /// 载入完成事件
        /// </summary>
        void fileManager_LoadFileContentCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else if (e.Result is string)
            {
                var content = e.Result as string;
				Body = content;

				_host.Ui.ShowMsgInStatusBar("load file success");
            }
        }
        /// <summary>
        /// 保存文件完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fileManager_SaveFileCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else if ((bool) e.Result)
            {
				_host.Ui.ShowMsgInAppDialog("save file success");
            }
            else
            {
				_host.Ui.ShowMsgInAppDialog("save file failed");
            }
        }

        /// <summary>
        /// 载入文件
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFileContent(string filePath)
        {
			Body = "loading";
            _fileManager.ReadFile(filePath);
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileData"></param>
        public void SaveFile(string filePath, string fileData)
        {
            _fileManager.WriteFile(filePath, fileData);
        }
    }
}
