using System.Collections.Generic;
using System.Windows.Forms;
using Altman.Forms;

namespace Altman
{
    public class TabCore
    {
        private static TabControl _tabControl = null;
        private static FormMain _formMain = null;

        public static void Init(FormMain formMain, TabControl tabControl)
        {
            _formMain = formMain;
            _tabControl = tabControl;
        }

        public static void CreateNewTabPage(string name, UserControl userControl)
        {
            //create new tabpage
            TabPage newTabpage = new TabPage(name);
            newTabpage.Name = name;
            newTabpage.Controls.Add(userControl);
            _tabControl.TabPages.Add(newTabpage);
            _tabControl.SelectTab(newTabpage);
        }
    }
}
