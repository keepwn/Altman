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
	    private TextArea bodyTextBox;

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

		    };
		    return control;
	    }

	    Control SaveFileButton()
		{
			var control = saveFileButton = new Button { Text = "Save" };
			control.Click += delegate
			{

			};
			return control;
		}

		Control BodyTextBox()
		{
			var control = bodyTextBox = new TextArea();
			return control;
		}

		Control CreateLayout()
		{
			var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };
			layout.AddRow(ReadFileButton(), UrlTextBox(), SaveFileButton());
			layout.AddRow(BodyTextBox());

			return layout;
		}

	    string Body
	    {
			get { return bodyTextBox.Text; }
		    set { bodyTextBox.Text = value; }
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
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Result is string)
            {
                string content = e.Result as string;
				Body = content;
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
                MessageBox.Show(e.Error.Message);
            }
            else if ((bool) e.Result)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
        /// <summary>
        /// 读取文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_readFile_Click(object sender, EventArgs e)
        {
			if (Url != null)
            {
				LoadFileContent(Url);
            }
            else
            {
                MessageBox.Show("地址栏为空");
            }
        }
        /// <summary>
        /// 保存文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_saveFile_Click(object sender, EventArgs e)
        {
			if (Url != null)
            {
				SaveFile(Url, Body);
            }
            else
            {
                MessageBox.Show("地址栏为空");
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
