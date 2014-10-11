using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_FileManager.Interface
{
	partial class FileEditerPanel
	{
		void Init()
		{
			//_textBoxUrl
			_textBoxUrl = new TextBox();

			//_buttonReadFile
			_buttonReadFile = new Button { Text = "Read" };
			_buttonReadFile.Click += _buttonReadFile_Click;

			//_buttonSaveFile
			_buttonSaveFile = new Button {Text = "Save"};
			_buttonSaveFile.Click += _buttonSaveFile_Click;

			//_textAreaBody
			_textAreaBody = new TextArea();

			var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.Add(_textBoxUrl, true);
			layout.Add(_buttonReadFile);
			layout.Add(_buttonSaveFile);
			layout.EndBeginHorizontal();
			layout.EndVertical();

			layout.AddRow(_textAreaBody);

			Content = layout;
		}

		private TextBox _textBoxUrl;
		private Button _buttonReadFile;
		private Button _buttonSaveFile;
		private TextArea _textAreaBody;
	}
}
