using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Interface
{
	partial class FileEditerPanel
	{
		void Init()
		{
			//_textBoxUrl
			_textBoxUrl = new TextBox();

			//_buttonReadFile
			_buttonReadFile = new Button { Text = StrRes.GetString("StrLoad", "Load") };
			_buttonReadFile.Click += _buttonReadFile_Click;

			//_buttonSaveFile
			_buttonSaveFile = new Button {Text = StrRes.GetString("StrSave","Save")};
			_buttonSaveFile.Click += _buttonSaveFile_Click;

			//_textAreaBody
			_textAreaBody = new TextArea();

			var layout = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.AddCentered(_textBoxUrl, xscale: true, horizontalCenter: false);
			layout.AddCentered(_buttonReadFile, horizontalCenter: false);
			layout.AddCentered(_buttonSaveFile, horizontalCenter: false);
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
