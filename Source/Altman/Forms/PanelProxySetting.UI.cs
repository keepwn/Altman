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
            // radioButtonList with proxy
		    _radioButtonList = new RadioButtonList {Orientation = Orientation.Vertical};
		    _radioButtonList.Items.Add(new ListItem { Text = AltStrRes.NotUseProxy });
            _radioButtonList.Items.Add(new ListItem { Text = AltStrRes.UseSystemProxySetting });
            _radioButtonList.Items.Add(new ListItem { Text = AltStrRes.UseCustomProxySetting });
		    _radioButtonList.SelectedIndex = 0;
		    _radioButtonList.SelectedKeyChanged += delegate
		    {
		        _groupBoxSetting.Enabled = _radioButtonList.SelectedIndex == 2;
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
		    layout.AddRow(_radioButtonList);
            layout.AddRow(_groupBoxSetting);
			layout.AddRow(null);

			this.Content = layout;
		}

	    private RadioButtonList _radioButtonList;
		private TextBox _textBoxProxyAddr;
		private TextBox _textBoxProxyPort;
		private TextBox _textBoxProxyUser;
		private TextBox _textBoxProxyPasswd;
		private TextBox _textBoxProxyDomain;

		private GroupBox _groupBoxSetting;
	}
}
