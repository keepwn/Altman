using Eto.Drawing;
using Eto.Forms;

namespace Plugin_Encoder.Interface
{
	partial class PanelEncoder
	{
		void Init()
		{
			_textAreaInput = new TextArea();
			_textAreaInput.Font = new Font(FontFamilies.Monospace, 10);

		    _dropDownServices = new DropDown {Width = 200};
			_dropDownServices.SelectedIndexChanged += DropDownServicesSelectedIndexChanged;

			_radioButtonEncode = new RadioButton {Text = "Encode"};
			_radioButtonEncode.CheckedChanged += _radioButtonEncode_CheckedChanged;
			_radioButtonDecode = new RadioButton(_radioButtonEncode) {Text = "Decode"};

			_buttonRun = new Button { Text = "Run" };
			_buttonRun.Click += _buttonRun_Click;

			_textAreaOutput = new TextArea();
			_textAreaOutput.Font = new Font(FontFamilies.Monospace, 10);

			var inputLayout = new DynamicLayout {Padding = new Padding(5, 5, 5, 0), Spacing = new Size(5, 5)};
			inputLayout.AddSeparateRow(_textAreaInput);

			var outputLayout = new DynamicLayout {Padding = new Padding(5, 5), Spacing = new Size(5, 5)};
			outputLayout.BeginVertical();
			outputLayout.BeginHorizontal();
			outputLayout.AddCentered(_radioButtonEncode, horizontalCenter: false);
			outputLayout.AddCentered(_radioButtonDecode, horizontalCenter: false);
			outputLayout.Add(null);
			outputLayout.AddCentered(_dropDownServices, horizontalCenter: false);
			outputLayout.AddCentered(_buttonRun, horizontalCenter: false);
			outputLayout.EndHorizontal();
			outputLayout.EndVertical();
			outputLayout.AddSeparateRow(_textAreaOutput);

			var layout = new Splitter
			{
				Panel1 = inputLayout,
				Panel2 = outputLayout,
				Orientation = SplitterOrientation.Vertical,
				Position = 130
			};

			Content = layout;
		}
	
		private TextArea _textAreaInput;
		private TextArea _textAreaOutput;

		private DropDown _dropDownServices;
		private RadioButton _radioButtonEncode;
		private RadioButton _radioButtonDecode;
		private Button _buttonRun;

	}
}
