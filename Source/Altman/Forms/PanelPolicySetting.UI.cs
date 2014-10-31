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
			_checkBoxIsShowDisclaimer = new CheckBox() { Text = AltStrRes.IsShowDisclaimer };

			var layout = new DynamicLayout { Padding = new Padding(20, 10), Spacing = new Size(10, 10) };

			layout.AddRow(_checkBoxIsParamRandom);
			layout.AddRow(_checkBoxIsShowDisclaimer);
			layout.Add(null);

			this.Content = layout;
		}

		private CheckBox _checkBoxIsParamRandom;
		private CheckBox _checkBoxIsShowDisclaimer;
	}
}
