using System;
using Altman.Desktop.Resources;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
    public class About : Command
    {
        public About()
        {
            MenuText = AltStrRes.AboutAltman;
            Executed += About_Executed;
        }

        void About_Executed(object sender, EventArgs e)
        {
            var about = new Dialogs.About();
            about.ShowModal(Application.Instance.MainForm);
        }
    }
}
