using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Altman
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
        }
        public Setting.Setting.PolicyStruct SavePolicySetting()
        {
            Setting.Setting.PolicyStruct policy = new Setting.Setting.PolicyStruct();
            policy.IsParamRandom = checkBox_IsParamRandom.Checked;
            return policy;
        }
    }
}
