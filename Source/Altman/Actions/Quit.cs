using System;
using Altman.Desktop.Resources;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
    public class Quit : Command
    {
        public Quit()
        {
	        MenuText = AltStrRes.Quit;
            Shortcut = Keys.Q | Application.Instance.CommonModifier;
            Executed += Quit_Executed;
        }

        void Quit_Executed(object sender, EventArgs e)
        {
            Application.Instance.Quit();
        }
    }
}
