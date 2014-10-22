using System;
using Altman.Dialogs;
using Altman.Resources;
using Eto.Forms;

namespace Altman.Actions
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
