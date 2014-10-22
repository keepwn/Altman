using System;
using Altman.Resources;
using Eto.Forms;

namespace Altman.Actions
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
