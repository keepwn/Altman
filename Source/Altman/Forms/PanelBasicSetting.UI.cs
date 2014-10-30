using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	partial class PanelBasicSetting
	{
		void Init()
		{
			_dropDownLang = new DropDown();
			_dropDownLang.Size = new Size(250, _dropDownLang.Size.Height);
			var lanList = new List<IListItem>
			{
				new ListItem {Text = "EN",Key="EN"},
				new ListItem {Text = "CN",Key="CN"}
			};
			_dropDownLang.Items.AddRange(lanList);

			var layout = new DynamicLayout {Padding = new Padding(20, 10), Spacing = new Size(10, 10)};

			layout.AddRow(
				new Label
				{
					Text = AltStrRes.GetString("StrLanguage", "Language"),
					VerticalAlign = VerticalAlign.Middle
				}, _dropDownLang, null);
			layout.Add(null);

			Content = layout;
		}

		private DropDown _dropDownLang;
	}
}
