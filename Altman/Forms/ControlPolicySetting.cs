using System.Windows.Forms;

namespace Altman.Forms
{
    public partial class ControlPolicySetting : UserControl
    {
        public ControlPolicySetting()
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void LoadPolicySetting(Setting.Setting.PolicyStruct policy)
        {
            checkBox_IsParamRandom.Checked = policy.IsParamRandom;
            checkBox_IsShowDisclaimer.Checked = policy.IsShowDisclaimer;
        }
        public Setting.Setting.PolicyStruct SavePolicySetting()
        {
            Setting.Setting.PolicyStruct policy = new Setting.Setting.PolicyStruct();
            policy.IsParamRandom = checkBox_IsParamRandom.Checked;
            policy.IsShowDisclaimer = checkBox_IsShowDisclaimer.Checked;
            return policy;
        }
    }
}
