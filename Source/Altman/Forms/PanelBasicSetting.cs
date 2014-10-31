using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Util.Setting;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelBasicSetting:Panel
	{
		public PanelBasicSetting()
		{
			Init();
		}

		public void LoadBasicSetting(Setting.BasicStruct policy)
		{
			_dropDownLang.SelectedKey = policy.Language;
		}
		public Setting.BasicStruct SaveBasicSetting()
		{
			var basic = new Setting.BasicStruct();
			basic.Language = _dropDownLang.SelectedKey;
			return basic;
		}
	}
}
