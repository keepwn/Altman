using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Desktop.Dialogs;
using Altman.Desktop.Resources;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
    public class Setting : Command
    {
        public Setting()
        {
			MenuText = AltStrRes.Setting;
            Shortcut = Keys.S | Application.Instance.CommonModifier;
            Executed += Setting_Executed;
        }

        void Setting_Executed(object sender, EventArgs e)
        {
	        new DialogSetting().ShowModal(Application.Instance.MainForm);
        }
    }
}
