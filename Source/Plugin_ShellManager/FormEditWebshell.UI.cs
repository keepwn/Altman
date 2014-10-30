using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager
{
	public partial class FormEditWebshell
	{
		void InitUi()
		{
			// input
			_textBoxName = new TextBox {PlaceholderText = "*Name", Size = new Size(200, -1)};
			_comboBoxLevel = new ComboBox(true) {Size = new Size(100, -1)};
			_dropDownScritpType = new DropDown {Size = new Size(120, -1)};
			_textBoxShellPath = new TextBox { PlaceholderText = "*Shell Url", Size = new Size(300, -1) };
			_textBoxShellPass = new TextBox { PlaceholderText = "*Pass" };
			_textBoxRemark = new TextBox { PlaceholderText = "Remark" };

			//_buttonAdd
			_buttonAdd = new Button { Text = StrRes.GetString("StrAdd", "Add") };
			_buttonAdd.Click += buttonAdd_Click;

			//_buttonAlter
			_buttonAlter = new Button { Text = StrRes.GetString("StrAlter", "Alter") };
			_buttonAlter.Click += _buttonAlter_Click;

			//_buttonAdvanced
			_buttonAdvanced = new Button { Text = StrRes.GetString("StrAdvanced","Advanced") };
			_buttonAdvanced.Click += _buttonAdvanced_Click;

			//buttonTest
			var buttonTest1 = new Button {Text = "Test1"};
			buttonTest1.Click += buttonTest1_Click;
			//buttonTest
			var buttonTest2 = new Button { Text = "Test2" };
			buttonTest2.Click += buttonTest2_Click;

			var codeList = new List<IListItem>
			{
				new ListItem {Text = "UTF-8"},
				new ListItem {Text = "GB2312"}
			};
			_dropDownServerCoding = new DropDown();
			_dropDownServerCoding.Items.AddRange(codeList);
			_dropDownServerCoding.SelectedIndex = 0;
			_dropDownWebCoding = new DropDown();
			_dropDownWebCoding.Items.AddRange(codeList);
			_dropDownWebCoding.SelectedIndex = 0;
			_richTextBoxSetting = new TextArea();

			//_panelAdvanced
			_panelAdvanced = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			_panelAdvanced.AddSeparateRow(new Label { Text = StrRes.GetString("StrServerCoding", "ServerCoding"), VerticalAlign = VerticalAlign.Middle }, _dropDownServerCoding);
			_panelAdvanced.AddSeparateRow(new Label { Text = StrRes.GetString("StrWebCoding", "WebCoding"), VerticalAlign = VerticalAlign.Middle }, _dropDownWebCoding);
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
			panel1.Add(buttonTest1);
			panel1.Add(buttonTest2);
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

		private void buttonTest1_Click(object sender, EventArgs e)
		{
			var conns = Altman.Webshell.Service.GetDbNodeInfoList(_dropDownScritpType.SelectedKey);
			_richTextBoxSetting.Text = ShellExtraStringHandle.CreateDefaultIniString(conns);
		}

		void buttonTest2_Click(object sender, EventArgs e)
		{
			var str = _richTextBoxSetting.Text;
		}

		private Button _buttonAdvanced;
		private Button _buttonReturn;
		private Button _buttonAdd;
		private Button _buttonAlter;
		private TextBox _textBoxName;
		private ComboBox _comboBoxLevel;
		private TextBox _textBoxShellPath;
		private TextBox _textBoxShellPass;
		private TextArea _richTextBoxSetting;
		private TextBox _textBoxRemark;
		private DropDown _dropDownScritpType;
		private DropDown _dropDownServerCoding;
		private DropDown _dropDownWebCoding;
		private Splitter _p12;
		private DynamicLayout _panelAdvanced;
	}
}
