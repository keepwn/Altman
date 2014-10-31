using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class PanelPolicySetting
	{
		void Init()
		{
			_checkBoxIsParamRandom = new CheckBox() { Text = AltStrRes.IsParamRandom };

			var layout = new DynamicLayout { Padding = new Padding(20, 20), Spacing = new Size(10, 10) };
			layout.AddRow(_checkBoxIsParamRandom);
			layout.Add(null);

			Content = layout;
		}

		private CheckBox _checkBoxIsParamRandom;
	}
}
