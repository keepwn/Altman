using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager.Interface
{
	public partial class FormEditWebshell
	{
		void InitUi()
		{
			// input
            _textBoxName = new TextBox { PlaceholderText = "*Name", Width = 200};
		    _comboBoxLevel = new ComboBox {Width = 100};
		    _dropDownScritpType = new DropDown {Width = 120};
		    _textBoxShellPath = new TextBox {PlaceholderText = "*Shell Url", Width = 300};
			_textBoxShellPass = new TextBox { PlaceholderText = "*Pass" };
			_textBoxRemark = new TextBox { PlaceholderText = "Remark" };

			// _buttonAdd
			_buttonAdd = new Button { Text = StrRes.GetString("StrAdd", "Add") };
			_buttonAdd.Click += buttonAdd_Click;

			// _buttonAlter
			_buttonAlter = new Button { Text = StrRes.GetString("StrAlter", "Alter") };
			_buttonAlter.Click += _buttonAlter_Click;

			// _buttonAdvanced
			_buttonAdvanced = new Button { Text = StrRes.GetString("StrAdvanced","Advanced") };
			_buttonAdvanced.Click += _buttonAdvanced_Click;

			var codeList1 = new List<IListItem>
			{
				new ListItem {Text = "UTF-8"},
				new ListItem {Text = "GB2312"}
			};
            var codeList2 = new List<IListItem>
			{
				new ListItem {Text = "UTF-8"},
				new ListItem {Text = "GB2312"},
                new ListItem {Text = "Big5"},
                new ListItem {Text = "Shift-JIS"},
                new ListItem {Text = "EUC-JP"},
                new ListItem {Text = "EUC-KR"},
                new ListItem {Text = "ISO-8859-1"}
			};
			_dropDownServerCoding = new DropDown();
			_dropDownServerCoding.Items.AddRange(codeList1);
			_dropDownServerCoding.SelectedIndex = 0;
			_dropDownWebCoding = new DropDown();
			_dropDownWebCoding.Items.AddRange(codeList2);
			_dropDownWebCoding.SelectedIndex = 0;

			// _buttonDefault
			_buttonDefault = new Button { Text = "Default" };
			_buttonDefault.Click += _buttonDefault_Click;

			// _richTextBoxSetting
			_richTextBoxSetting = new TextArea {Wrap = false};

			// _panelAdvanced
			_panelAdvanced = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			_panelAdvanced.BeginVertical();
			_panelAdvanced.BeginHorizontal();
			_panelAdvanced.AddAutoSized(new Label
			{
				Text = StrRes.GetString("StrServerCoding", "ServerCoding"),
				VerticalAlign = VerticalAlign.Middle
			}, centered: true);
			_panelAdvanced.AddAutoSized(_dropDownServerCoding, centered: true);
			_panelAdvanced.AddAutoSized(new Label
			{
				Text = StrRes.GetString("StrWebCoding", "WebCoding"),
				VerticalAlign = VerticalAlign.Middle
			}, centered: true);
			_panelAdvanced.AddAutoSized(_dropDownWebCoding, centered: true);
			_panelAdvanced.Add(null);
			_panelAdvanced.AddAutoSized(_buttonDefault, centered: true);
			_panelAdvanced.EndHorizontal();
			_panelAdvanced.EndVertical();
			//_panelAdvanced.AddSeparateRow(new Label { Text = StrRes.GetString("StrServerCoding", "ServerCoding"), VerticalAlign = VerticalAlign.Middle }, _dropDownServerCoding);
			//_panelAdvanced.AddSeparateRow(new Label { Text = StrRes.GetString("StrWebCoding", "WebCoding"), VerticalAlign = VerticalAlign.Middle }, _dropDownWebCoding);
			_panelAdvanced.AddSeparateRow(_richTextBoxSetting);

			var panel1 = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			//line 1
			panel1.BeginVertical();
			panel1.BeginHorizontal();
			panel1.Add(_textBoxName, xscale: true);
			panel1.Add(_comboBoxLevel);
			panel1.Add(_dropDownScritpType);
			panel1.EndHorizontal();
			panel1.EndVertical();
			//line 2
			panel1.BeginVertical();
			panel1.BeginHorizontal();
			panel1.Add(_textBoxShellPath, true);
			panel1.Add(_textBoxShellPass);
			panel1.EndHorizontal();
			panel1.EndVertical();
			//line 3
			panel1.AddRow(_textBoxRemark);
			//line 4
			panel1.BeginVertical();
			panel1.BeginHorizontal();
			panel1.Add(_buttonAdvanced);
			panel1.Add(null, true);
			panel1.Add(_buttonAdd);
			panel1.Add(_buttonAlter);
			panel1.EndHorizontal();
			panel1.EndVertical();
			//line 5
			panel1.Add(_panelAdvanced, false, true);
			_panelAdvanced.Visible = false;

			//_p12 = new Splitter
			//{
			//	Panel1 = panel1,
			//	Panel2 = _panelAdvanced,
			//	Orientation = SplitterOrientation.Vertical,
			//	Position = 130,
			//};
			//_p12.FixedPanel = SplitterFixedPanel.Panel1;
			//_p12.Panel2.Visible = false;

			Content = panel1;
			ClientSize = new Size(500, 130);
			Title = "Edit Shell";
			Icon = Application.Instance.MainForm.Icon;
		}

		private TextBox _textBoxName;
		private ComboBox _comboBoxLevel;
		private DropDown _dropDownScritpType;
		private TextBox _textBoxShellPath;
		private TextBox _textBoxShellPass;
		private Button _buttonAdvanced;
		private Button _buttonAdd;
		private Button _buttonAlter;		

		private TextBox _textBoxRemark;
		private DropDown _dropDownServerCoding;
		private DropDown _dropDownWebCoding;
		private Button _buttonDefault;
		private TextArea _richTextBoxSetting;
		private DynamicLayout _panelAdvanced;
		private Splitter _p12;
	}
}
