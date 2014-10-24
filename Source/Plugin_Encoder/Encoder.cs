using System;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Eto.Forms;

namespace Plugin_Encoder
{
	public partial class Encoder : Panel
	{
		private IHost _host;

		private Func<string, bool, string> _encodeFunc;
		private Func<string, string> _convertFunc;
		private ListItemCollection _encodeItem = new ListItemCollection();
		private ListItemCollection _decodeItem = new ListItemCollection();
		public Encoder(IHost host, PluginParameter data)
		{
			Init();

			_host = host;

			_radioButtonEncode.Checked = true;
			LoadServices();
		}

		public void LoadServices()
		{
			var encodeNames = PluginServiceProvider.GetServiceNamesByType("Encode");
			foreach (var name in encodeNames)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetService(name)
				};
				_encodeItem.Add(item);
			}

			var convertName = PluginServiceProvider.GetServiceNamesByType("Decode");
			foreach (var name in convertName)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetService(name)
				};
				_decodeItem.Add(item);
			}
		}

		void _radioButtonEncode_CheckedChanged(object sender, EventArgs e)
		{
			_comboBoxServices.DataStore = _radioButtonEncode.Checked ? _encodeItem : _decodeItem;
		}

		void _comboBoxServices_SelectedIndexChanged(object sender, EventArgs e)
		{
			//var index = _comboBoxServices.SelectedIndex;
			//if (index >= 0)
			//{
			//	var selctedService = (_comboBoxServices.Items[index] as ListItem).Tag;
			//	if (selctedService is Func<string, bool, string>)
			//	{
			//		_radioButtonDecode.Enabled = true;
			//	}
			//	else if (selctedService is Func<string, string>)
			//	{
			//		_radioButtonEncode.Checked = true;
			//		_radioButtonDecode.Enabled = false;
			//	}
			//}
		}

		void _buttonRun_Click(object sender, EventArgs e)
		{
			var input = _textAreaInput.Text;
			var index = _comboBoxServices.SelectedIndex;

			if (string.IsNullOrEmpty(input) || index < 0) 
				return;
			
			dynamic selctedService = (_comboBoxServices.Items[index] as ListItem).Tag;

			var result = selctedService(new PluginParameter("str", input));
			_textAreaOutput.Text = result;
		}
	}
}
