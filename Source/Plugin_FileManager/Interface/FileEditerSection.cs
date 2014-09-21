using System;
using System.ComponentModel;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_FileManager.Interface
{
    public class FileEditerSection : Panel
    {
        private FileManagerService _fileManager;

        private IHost _host;
        private Shell _shellData;

	    private TextBox urlTextBox;
	    private Button readFileButton;
	    private Button saveFileButton;
	    private TextArea bodyTextArea;

		public FileEditerSection(IHost host, Shell data, string filePath, bool autoLoadContent)
		{
			Content = CreateLayout();

            this._host = host;
            this._shellData = data;
            _fileManager = new FileManagerService(_host, _shellData);
            _fileManager.ReadFileCompletedToDo += fileManager_LoadFileContentCompletedToDo;
            _fileManager.WriteFileCompletedToDo += fileManager_SaveFileCompletedToDo;

            if (filePath != null)
            {
				Url = filePath;
                if (autoLoadContent)
                    LoadFileContent(filePath);  
            }
        }

	    Control UrlTextBox()
	    {
		    var control = urlTextBox = new TextBox();
		    return control;
	    }

	    Control ReadFileButton()
	    {
		    var control = readFileButton = new Button {Text = "Read"};
		    control.Click += delegate
		    {
				if (Url != null)
				{
					LoadFileContent(Url);
				}
				else
				{
					_host.Ui.ShowMsgInAppDialog("url is null");
				}
		    };
		    return control;
	    }

	    Control SaveFileButton()
		{
			var control = saveFileButton = new Button { Text = "Save" };
			control.Click += delegate
			{
				if (Url != null)
				{
					SaveFile(Url, Body);
				}
				else
				{
					_host.Ui.ShowMsgInAppDialog("url is null");
				}
			};
			return control;
		}

		Control BodyTextArea()
		{
			var control = bodyTextArea = new TextArea();
			return control;
		}

		Control CreateLayout()
		{
			var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.Add(UrlTextBox(),true);
			layout.Add(ReadFileButton());
			layout.Add(SaveFileButton());
			layout.EndBeginHorizontal();
			layout.EndVertical();

			layout.AddRow(BodyTextArea());

			return layout;
		}

	    string Body
	    {
			get { return bodyTextArea.Text; }
		    set { bodyTextArea.Text = value; }
	    }

	    string Url
	    {
			get { return urlTextBox.Text; }
			set { urlTextBox.Text = value; }
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
