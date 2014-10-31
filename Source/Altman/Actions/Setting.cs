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
			MenuText = AltStrRes.Options;
            Shortcut = Keys.S | Application.Instance.CommonModifier;
            Executed += Setting_Executed;
        }

        void Setting_Executed(object sender, EventArgs e)
        {
	        new Options().ShowModal(Application.Instance.MainForm);
        }
    }
}
