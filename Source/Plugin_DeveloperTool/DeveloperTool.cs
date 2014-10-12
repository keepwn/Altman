using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_DeveloperTool
{
	public class DeveloperTool : Form
	{
		private IHost _host;
		public DeveloperTool(IHost host, PluginParameter data)
		{
			Init();

			_host = host;

			_radioButtonEncode.Checked = true;

			LoadServices();
		}

		private ComboBox _comboBoxServices;
		private RadioButton _radioButtonEncode;
		private RadioButton _radioButtonDecode;
		private Button _buttonRun;
		private TextBox _textBoxA;
		private TextBox _textBoxB;

		private TextArea _textAreaResult;
		void Init()
		{
			_comboBoxServices = new ComboBox();

			_radioButtonEncode = new RadioButton();
			_radioButtonDecode = new RadioButton(_radioButtonEncode);

			_buttonRun = new Button { Text = "Run" };
			_buttonRun.Click += _buttonRun_Click;

			_textBoxA = new TextBox();
			_textBoxB = new TextBox();

			_textAreaResult = new TextArea();

			var layout = new DynamicLayout {Padding = new Padding(10, 10)};
			layout.AddSeparateRow(_comboBoxServices, _radioButtonEncode, _radioButtonDecode, _buttonRun);
			layout.AddSeparateRow(_textBoxA, _textBoxB);
			layout.AddSeparateRow(_textAreaResult);

			Content = layout;
			Title = "Developer Tool";
		}

		void _buttonRun_Click(object sender, EventArgs e)
		{
			var index = _comboBoxServices.SelectedIndex;
			_function = (_comboBoxServices.Items[index] as ListItem).Tag as Func<string, bool, string>;

			var par1 = _textBoxA.Text.Trim();
			var par2 = _radioButtonEncode.Checked;
			var result = _function(par1,par2);

			_textAreaResult.Text = result;
		}

		private Func<string, bool, string> _function;
		public void LoadServices()
		{
			var names = PluginServiceProvider.GetServiceNames<Func<string, bool, string>>();
			foreach (var name in names)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetService<Func<string, bool, string>>(name)
				};
				_comboBoxServices.Items.Add(item);
			}
		}
	}
}
