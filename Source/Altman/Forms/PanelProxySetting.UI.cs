using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class PanelProxySetting
	{
		void Init()
		{
			// radioButton_noProxy
			_radioButtonNoProxy = new RadioButton() { Text = AltStrRes.NotUseProxy };
			_radioButtonNoProxy.CheckedChanged += delegate
			{
				if (_radioButtonNoProxy.Checked)
				{
					_groupBoxSetting.Enabled = false;
				}
			};

			// radioButton_ieProxy
			_radioButtonIeProxy = new RadioButton(_radioButtonNoProxy) { Text = AltStrRes.UseSystemProxySetting };
			_radioButtonIeProxy.CheckedChanged += delegate
			{
				if (_radioButtonIeProxy.Checked)
				{
					_groupBoxSetting.Enabled = false;
				}
			};

			// radioButton_customProxy
			_radioButtonCustomProxy = new RadioButton(_radioButtonNoProxy) { Text = AltStrRes.UseCustomProxySetting };
			_radioButtonCustomProxy.CheckedChanged += delegate
			{
				if (_radioButtonCustomProxy.Checked)
				{
					_groupBoxSetting.Enabled = true;
				}
			};

			// textBox
			_textBoxProxyAddr = new TextBox();
			_textBoxProxyPort = new TextBox();
			_textBoxProxyUser = new TextBox();
			_textBoxProxyPasswd = new TextBox();
			_textBoxProxyDomain = new TextBox();

			var proxysetting = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(5, 5) };
			proxysetting.AddRow(new Label { Text = "Server: ", VerticalAlign = VerticalAlign.Middle}, TableLayout.AutoSized(_textBoxProxyAddr));
			proxysetting.AddRow(new Label { Text = "Port: ", VerticalAlign = VerticalAlign.Middle }, TableLayout.AutoSized(_textBoxProxyPort));
			proxysetting.AddRow(new Label { Text = "User: ", VerticalAlign = VerticalAlign.Middle }, TableLayout.AutoSized(_textBoxProxyUser));
			proxysetting.AddRow(new Label { Text = "Passwd: ", VerticalAlign = VerticalAlign.Middle }, TableLayout.AutoSized(_textBoxProxyPasswd));
			proxysetting.AddRow(new Label { Text = "Domain: ", VerticalAlign = VerticalAlign.Middle }, TableLayout.AutoSized(_textBoxProxyDomain));

			_groupBoxSetting = new GroupBox {Text = AltStrRes.Setting, Content = proxysetting};

			var layout = new DynamicLayout { Padding = new Padding(20, 20), Spacing = new Size(10, 10) };
			layout.AddRow(_radioButtonNoProxy);
			layout.AddRow(_radioButtonIeProxy);
			layout.AddRow(_radioButtonCustomProxy);
			layout.AddRow(_groupBoxSetting);
			layout.AddRow(null);

			this.Content = layout;
		}

		private RadioButton _radioButtonNoProxy;
		private RadioButton _radioButtonIeProxy;
		private RadioButton _radioButtonCustomProxy;
		private TextBox _textBoxProxyAddr;
		private TextBox _textBoxProxyPort;
		private TextBox _textBoxProxyUser;
		private TextBox _textBoxProxyPasswd;
		private TextBox _textBoxProxyDomain;

		private GroupBox _groupBoxSetting;
	}
}
