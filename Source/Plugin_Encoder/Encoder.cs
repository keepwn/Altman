using System;
using Eto.Forms;
using PluginFramework;

namespace Plugin_Encoder
{
	public partial class Encoder : Panel
	{
		private IHost _host;

		private Func<string, bool, string> _encodeFunc;
		private Func<string, string> _convertFunc;
		public Encoder(IHost host, PluginParameter data)
		{
			Init();

			_host = host;

			_radioButtonEncode.Checked = true;
			LoadServices();
		}

		void _comboBoxServices_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = _comboBoxServices.SelectedIndex;
			if (index >= 0)
			{
				var selctedService = (_comboBoxServices.Items[index] as ListItem).Tag;
				if (selctedService is Func<string, bool, string>)
				{
					_radioButtonDecode.Enabled = true;
				}
				else if (selctedService is Func<string, string>)
				{
					_radioButtonEncode.Checked = true;
					_radioButtonDecode.Enabled = false;
				}
			}
		}

		void _buttonRun_Click(object sender, EventArgs e)
		{
			var input = _textAreaInput.Text;
			var encode = _radioButtonEncode.Checked;
			var index = _comboBoxServices.SelectedIndex;

			if (string.IsNullOrEmpty(input) || index < 0) 
				return;

			var result = "";
			var selctedService = (_comboBoxServices.Items[index] as ListItem).Tag;
			if (selctedService is Func<string, bool, string>)
			{
				_encodeFunc = (Func<string, bool, string>) selctedService;
				result = _encodeFunc(input, encode);
			}
			else if (selctedService is Func<string, string>)
			{
				_convertFunc = (Func<string, string>)selctedService;
				result = _convertFunc(input);
			}

			_textAreaOutput.Text = result;
		}

		public void LoadServices()
		{
			var encodeNames = PluginServiceProvider.GetServiceNamesByType("Encode");
			foreach (var name in encodeNames)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetService<Func<string, bool, string>>(name)
				};
				_comboBoxServices.Items.Add(item);
			}

			var convertName = PluginServiceProvider.GetServiceNamesByType("Convert");
			foreach (var name in convertName)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetService<Func<string, string>>(name)
				};
				_comboBoxServices.Items.Add(item);
			}
		}
	}
}
