using Altman.Resources;
using Altman.Util.Setting;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public partial class PanelPolicySetting : Panel
    {

        public PanelPolicySetting()
        {
            Init();
        }
      
        public void LoadPolicySetting(Setting.PolicyStruct policy)
        {
            _checkBoxIsParamRandom.Checked = policy.IsParamRandom;
            _checkBoxIsShowDisclaimer.Checked = policy.IsShowDisclaimer;
        }
        public Setting.PolicyStruct SavePolicySetting()
        {
            var policy = new Setting.PolicyStruct();
            policy.IsParamRandom = _checkBoxIsParamRandom.Checked==true;
            policy.IsShowDisclaimer = _checkBoxIsShowDisclaimer.Checked==true;
            return policy;
        }
    }
}
