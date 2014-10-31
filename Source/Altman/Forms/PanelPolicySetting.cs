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
        }
        public Setting SaveSetting()
        {
            var policy = new Setting.PolicyStruct
            {
	            IsParamRandom = _checkBoxIsParamRandom.Checked == true
            };

			var setting = new Setting {PolicySetting = policy};
	        return setting;
        }
    }
}
