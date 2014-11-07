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
		    _dropDownLang = new DropDown {Width = 200};
			var lanList = new List<IListItem>
			{
				new ListItem {Text = "EN",Key="EN"},
				new ListItem {Text = "CN",Key="CN"}
			};
			_dropDownLang.Items.AddRange(lanList);

			_checkBoxIsShowDisclaimer = new CheckBox { Text = AltStrRes.IsShowDisclaimer };
			_checkBoxIsOpenIPythonSupport = new CheckBox {Text = AltStrRes.IsOpenIPythonSupport};

			var layout = new DynamicLayout {Padding = new Padding(20, 20), Spacing = new Size(10, 10)};
			layout.AddSeparateRow(
				new Label {Text = AltStrRes.Language, VerticalAlign = VerticalAlign.Middle},
				_dropDownLang, null);
			layout.AddRow(_checkBoxIsShowDisclaimer, null);
			layout.AddRow(_checkBoxIsOpenIPythonSupport, null);
			layout.Add(null);

			Content = layout;
		}

		private DropDown _dropDownLang;
		private CheckBox _checkBoxIsShowDisclaimer;
		private CheckBox _checkBoxIsOpenIPythonSupport;
	}
}
