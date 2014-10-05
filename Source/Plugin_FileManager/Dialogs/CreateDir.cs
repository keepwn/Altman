using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_FileManager.Dialogs
{
	public class CreateDir : Dialog<string>
	{
		private TextBox _dirNameTextBox;
		private Button _okButton;
		public CreateDir(string defaultName)
        {
			Title = "New Dir";
            Resizable = false;

            var layout = new DynamicLayout { Padding = new Padding(20, 5), Spacing = new Size(10, 10) };

            layout.AddRow(_dirNameTextBox = new TextBox { Text = defaultName }, OkButton());

            Content = layout;
        }

		Button OkButton()
		{
			var control = _okButton = new Button
			{
				Text = "Create"
			};
			control.Click += delegate
			{
				Result = _dirNameTextBox.Text;
				Close();
			};
			return control;
		}
	}
}
