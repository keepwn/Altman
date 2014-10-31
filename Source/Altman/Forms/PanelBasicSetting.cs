using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Dialogs;
using Altman.Util.Setting;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelBasicSetting : Panel, IOptions
	{
		public PanelBasicSetting()
		{
			Init();
		}

		public void LoadSetting(Setting setting)
		{
			var basic = setting.BasicSetting;
			_dropDownLang.SelectedKey = basic.Language;
		}
		public Setting SaveSetting()
		{
			var basic = new Setting.BasicStruct
			{
				Language = _dropDownLang.SelectedKey
			};

			var setting = new Setting {BasicSetting = basic};
			return setting;
		}
	}
}
