using System;
using Altman.Resources;
using Eto.Forms;

namespace Altman.Actions
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
