using System;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
    public class Quit : Command
    {
        public Quit()
        {
			ID = "commandQuit";
            MenuText = "Quit";
            ToolTip = "Close altman";
            Shortcut = Keys.Q | Application.Instance.CommonModifier;
            Executed += Quit_Executed;
        }

        void Quit_Executed(object sender, EventArgs e)
        {
            Application.Instance.Quit();
        }
    }
}
