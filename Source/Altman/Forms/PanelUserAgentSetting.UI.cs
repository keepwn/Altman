using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class PanelUserAgentSetting
	{
		void Init()
		{
			_checkBoxIsRandom = new CheckBox { Text = AltStrRes.IsUserAgentRandom };
		    _comboBoxUserAgentList = new ComboBox {Width = 300};

			var layout = new DynamicLayout { Padding = new Padding(20, 20), Spacing = new Size(10, 10) };

			layout.AddRow(_checkBoxIsRandom);
			layout.AddRow(new Label { Text = AltStrRes.UserAgentList });
			layout.AddRow(_comboBoxUserAgentList, null);
			layout.Add(null);

			this.Content = layout;
		}

		private CheckBox _checkBoxIsRandom;
		private ComboBox _comboBoxUserAgentList;
	}
}
