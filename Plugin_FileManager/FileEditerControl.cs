using System;
using System.ComponentModel;
using System.Windows.Forms;

using Altman.Model;
using PluginFramework;

namespace Plugin_FileManager
{
    public partial class FileEditerControl : UserControl
    {
        private FileManagerService _fileManager;

        private IHost _host;
        private Shell _shellData;


        public FileEditerControl(IHost host, Shell data, string filePath, bool autoLoadContent)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            this._host = host;
            this._shellData = data;
            _fileManager = new FileManagerService(_host, _shellData);
            _fileManager.ReadFileCompletedToDo += fileManager_LoadFileContentCompletedToDo;
            _fileManager.WriteFileCompletedToDo += fileManager_SaveFileCompletedToDo;

            if (filePath != null)
            {
                textBox_url.Text = filePath;
                if (autoLoadContent)
                    LoadFileContent(filePath);  
            }
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
                textBox_fileContent.Text = content;
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
            if (textBox_url.Text != null)
            {
                LoadFileContent(textBox_url.Text);
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
            if (textBox_url.Text != null)
            {
                SaveFile(textBox_url.Text,textBox_fileContent.Text);
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
            textBox_fileContent.Text = "载入中...";
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
