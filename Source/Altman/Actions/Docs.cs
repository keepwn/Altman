using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Desktop.Resources;
using Eto.Forms;

namespace Altman.Desktop.Actions
{
	public class Docs : Command
	{
		public Docs()
		{
			MenuText = AltStrRes.Docs;
			Shortcut = Keys.F1;
			Executed += Docs_Executed;
		}

		void Docs_Executed(object sender, EventArgs e)
		{
			var web = "http://altman.keepwn.com/docs/info/";
			System.Diagnostics.Process.Start(web);
		}
	}
}
