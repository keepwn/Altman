using System;
using System.Windows.Forms;

namespace Plugin_FileManager
{
    public partial class FormWget : Form
    {
        private string _saveDir;
        public FormWget(string saveDir)
        {
            InitializeComponent();
            this.textBox_url.TextChanged += textBox_url_TextChanged;
            _saveDir = saveDir;
            textBox_url.Text = "http://";
            textBox_save.Text = _saveDir;
        }

        private void textBox_url_TextChanged(object sender, EventArgs e)
        {
            string name=textBox_url.Text.Substring(textBox_url.Text.LastIndexOf("/")+1);
            textBox_save.Text = _saveDir + name;
        }
        private void button_wget_Click(object sender, EventArgs e)
        {
            if (textBox_url.Text == "")
            {
                MessageBox.Show("远程地址不应该为空");
                return;
            }
            if (!textBox_url.Text.StartsWith("http://") && !textBox_url.Text.StartsWith("https://"))
            {
                MessageBox.Show("url地址应以http(s)://开头");
                return;
            }
            if (textBox_save.Text=="")
            {
                MessageBox.Show("保存地址不应该为空");
                return;
            }
            if (textBox_save.Text.EndsWith("/"))
            {
                MessageBox.Show("保存地址不可以为文件夹");
                return;
            }
            this.DialogResult = DialogResult.Yes;
        }


        public string UrlPath
        {
            get { return textBox_url.Text; }
        }
        public string SavePath
        {
            get { return textBox_save.Text; }
        }
    }
}
