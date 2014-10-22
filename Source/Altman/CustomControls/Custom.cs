using System;
using Altman.Resources;
using Eto.Forms;

namespace Altman.CustomControls
{
	public class Custom
	{

	}

	public class DialogPlus : Dialog
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			AltLangRes.UpdateLanguage(this);
		}
	}

	public class FormPlus : Form
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			AltLangRes.UpdateLanguage(this);
		}
	}

	public class PanelPlus : Panel
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			AltLangRes.UpdateLanguage(this);
		}
	}

}
