using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_FileManager.Dialogs
{
	public class Wget : Dialog<DialogResult>
	{
		private string _saveDir;

		private TextBox textBox_url;
		private TextBox textBox_save;
		private Button button_wget;
		public Wget(string saveDir)
		{
			_saveDir = saveDir;
			Init();			
		}

		void Init()
		{
			textBox_url = new TextBox();
			textBox_url.Text = "http://";
			textBox_save = new TextBox();
			textBox_save.Text = _saveDir;

			textBox_url.TextChanged += textBox_url_TextChanged;		
			
			button_wget = new Button
			{
				Text = "Wget"
			};
			button_wget.Click += button_wget_Click;

			var layout = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(5, 5) };

			var layout_left = new DynamicLayout();
			layout_left.AddColumn(textBox_url, textBox_save);

			layout.BeginHorizontal();
			layout.Add(layout_left,true);
			layout.Add(button_wget);
			layout.EndBeginHorizontal();

			Content = layout;

			Title = "Wget";
			Size = new Size(300, 100);
		}


		private void textBox_url_TextChanged(object sender, EventArgs e)
		{
			string name = textBox_url.Text.Substring(textBox_url.Text.LastIndexOf("/", StringComparison.Ordinal) + 1);
			textBox_save.Text = _saveDir + name;
		}
		private void button_wget_Click(object sender, EventArgs e)
		{
			if (textBox_url.Text == "")
			{
				MessageBox.Show(this, "the url couldn't be empty");
				return;
			}
			if (!textBox_url.Text.StartsWith("http://") && !textBox_url.Text.StartsWith("https://"))
			{
				MessageBox.Show(this, "the url should beginning with http[s]://");
				return;
			}
			if (textBox_save.Text == "")
			{
				MessageBox.Show(this, "the save url is empty");
				return;
			}
			if (textBox_save.Text.EndsWith("/"))
			{
				MessageBox.Show(this, "the save url couldn't be folder");
				return;
			}

			Result = DialogResult.Ok;
			Close();
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
