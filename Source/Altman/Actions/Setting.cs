using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
    public class Setting : Command
    {
        public Setting()
        {
            ID = "setting";
            MenuText = "&Setting";
            ToolBarText = "Setting";
            ToolTip = "Altman Setting";
            Shortcut = Keys.S | Application.Instance.CommonModifier;
            Executed += Setting_Executed;
        }

        void Setting_Executed(object sender, EventArgs e)
        {
            new Forms.FormGlobalSetting().Show();
        }
    }
}
