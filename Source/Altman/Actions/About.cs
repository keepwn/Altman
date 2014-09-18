using System;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
    public class About : Command
    {
        public About()
        {
			ID = "commandAbout";
            MenuText = "About Altman";
            Shortcut = Keys.F11;
            Executed += About_Executed;
        }

        void About_Executed(object sender, EventArgs e)
        {
            var about = new Dialogs.About();
            about.ShowModal(Application.Instance.MainForm);
        }
    }
}
