using System;
using System.ComponentModel;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Webshell.Model;
using Eto.Drawing;
using Eto.Forms;

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

        #region Event

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

        void findCommand_Executed(object sender, EventArgs e)
        {
            var findForm = new FileEditerFindForm();
            findForm.FindNextClick += findForm_FindNextClick;
            findForm.Show();
        }

        void findForm_FindNextClick(object sender, FindContentEventArgs e)
        {
            FindText(_textAreaBody, e.FindContent, _textAreaBody.CaretIndex, e.CaseSensitive,
                e.FindDirection == FindContentEventArgs.Direction.Up);
        }

        /// <summary>
        /// 载入完成事件
        /// </summary>
        void fileManager_LoadFileContentCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            _host.Ui.ShowMsgInStatusBar("", false);
            if (e.Error != null)
            {
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else
            {
                string msg;
                if (e.Result is string)
                {
                    msg = "Load file success";

                    var content = e.Result as string;
                    Body = content;
                    _textAreaBody.Focus();                  
                }
                else
                {
                    msg = "Load file success";
                }

				_host.Ui.ShowMsgInStatusBar(msg);
            }
        }

        /// <summary>
        /// 保存文件完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fileManager_SaveFileCompletedToDo(object sender, RunWorkerCompletedEventArgs e)
        {
            _host.Ui.ShowMsgInStatusBar("", false);
            if (e.Error != null)
            {
				_host.Ui.ShowMsgInStatusBar(e.Error.Message);
            }
            else
            {
                string msg;
                if ((bool) e.Result)
                {
                    msg = "Save file success";
                }
                else
                {
                    msg = "Save file failed";
                }
                
				_host.Ui.ShowMsgInAppDialog(msg);
                _host.Ui.ShowMsgInStatusBar(msg);
            }
        }

        #endregion

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

        /// <summary>
        /// 查找字符串
        /// </summary>
        /// <param name="textArea"></param>
        /// <param name="findContent"></param>
        /// <param name="curIndex"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="isUp"></param>
        public void FindText(TextArea textArea, string findContent, int curIndex, bool caseSensitive, bool isUp)
        {
            string searchText;
            
            // pre
            if (isUp)
            {
                searchText = textArea.Text.Substring(0, curIndex);
            }
            else
            {
                curIndex += textArea.Selection.Length();
                searchText = textArea.Text.Substring(curIndex);
            }
           
            if (!caseSensitive)
            {
                searchText = searchText.ToLower();
                findContent = findContent.ToLower();
            }

            // find
            var index = isUp
                ? searchText.LastIndexOf(findContent, StringComparison.Ordinal)
                : searchText.IndexOf(findContent, StringComparison.Ordinal);
            if (index != -1)
            {
                var selectionStart = isUp ? index : index + curIndex;
                textArea.Selection = new Range<int>(selectionStart, selectionStart + findContent.Length - 1);
                textArea.Focus();
            }
            else
            {
                MessageBox.Show(string.Format("Not Found \"{0}\" ", findContent));
            }
        }
    }
}
