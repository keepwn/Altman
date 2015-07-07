using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Interface
{
    partial class FileEditerFindForm
    {
        void Init()
		{
            // _textBoxFindText
            var labelFindText = new Label { Text = StrRes.GetString("StrFindContent", "FindContent") };	
			_textBoxFindText = new TextBox();

			// _buttonFindNext
			_buttonFindNext = new Button { Text = StrRes.GetString("StrFindNext", "FindNext") };
            _buttonFindNext.Click += _buttonFindNext_Click;

			// _buttonCancel
			_buttonCancel = new Button { Text = StrRes.GetString("StrCancel","Cancel") };
            _buttonCancel.Click += _buttonCancel_Click;

            // _checkBoxCaseSensitive
            _checkBoxCaseSensitive = new CheckBox { Text = StrRes.GetString("StrCaseSensitive", "Case Sensitive") };

            // _radio
            _radioButtonUp = new RadioButton { Text = StrRes.GetString("StrUp", "Up") };
            _radioButtonDown = new RadioButton(_radioButtonUp) { Text = StrRes.GetString("StrDown", "Down") };
            _radioButtonDown.Checked = true;

            var groupLayout = new DynamicLayout {Padding = new Padding(5, 5), Spacing = new Size(5, 5)};
            groupLayout.AddRow(_radioButtonUp, _radioButtonDown);

            var group = new GroupBox { Text = StrRes.GetString("StrDirection", "Direction") };
            group.Content = groupLayout;

			var layout = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };

            layout.BeginHorizontal();
            // left start --->
            layout.BeginVertical(xscale: true);
            layout.BeginHorizontal();
            layout.Add(labelFindText);
            layout.Add(_textBoxFindText);
            layout.EndHorizontal();

            layout.Add(null);

            layout.BeginHorizontal();
            layout.Add(_checkBoxCaseSensitive);
            layout.Add(group);
            layout.EndHorizontal();
            layout.EndVertical();
            // <--- left end

            // right start --->
            layout.BeginVertical(xscale: false, yscale: false);
            layout.Add(_buttonFindNext);         
            layout.Add(null);
            layout.Add(_buttonCancel);
            layout.EndVertical();
            // <--- right end
            layout.EndHorizontal();

			Content = layout;
            Title = StrRes.GetString("StrFind", "Find");
            Minimizable = false;
            Maximizable = false;
            Topmost = true;
            ClientSize = new Size(350, 90);
            Resizable = false;     
		}

		private TextBox _textBoxFindText;
		private Button _buttonFindNext;
		private Button _buttonCancel;
        private CheckBox _checkBoxCaseSensitive;
        private RadioButton _radioButtonUp;
        private RadioButton _radioButtonDown;
    }
}
