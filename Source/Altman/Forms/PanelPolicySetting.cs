using Altman.Dialogs;
using Altman.Resources;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelPolicySetting : Panel,IOptions
    {

        public PanelPolicySetting()
        {
            Init();
        }
      
        public void LoadSetting(Setting setting)
        {
			var policy = setting.PolicySetting;

            _checkBoxIsParamRandom.Checked = policy.IsParamRandom;
            _checkBoxIsShowDisclaimer.Checked = policy.IsShowDisclaimer;
        }
        public Setting SaveSetting()
        {
            var policy = new Setting.PolicyStruct
            {
	            IsParamRandom = _checkBoxIsParamRandom.Checked == true,
	            IsShowDisclaimer = _checkBoxIsShowDisclaimer.Checked == true
            };

			var setting = new Setting {PolicySetting = policy};
	        return setting;
        }
    }
}
