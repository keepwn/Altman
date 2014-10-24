using System;
using System.Collections.Generic;
using Altman.Resources;
using Eto;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.CustomControls
{
	public class UpdateForm
	{
		private static Font _font = Fonts.Serif(20, FontStyle.Italic);

		public static void UpdateFont(object obj)
		{
			try
			{
				if (obj is Window)
				{
					var form = obj as Window;
					if (form.Controls != null)
					{
						UpdateControls(form.Controls);
					}
				}
				//if (obj is Panel)
				//{
				//	var panel = obj as Panel;
				//	if (panel.ContextMenu != null)
				//	{
				//		//UpdateMenu(panel.ContextMenu.Items, Table);
				//	}
				//	if (panel.Controls != null)
				//	{
				//		UpdateControls(panel.Controls);
				//	}
				//}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private static void UpdateControls(IEnumerable<Control> controls)
		{
			if (controls == null) return;
			foreach (var item in controls)
			{
				UpdateControls(item);
			}
		}

		private static void UpdateControls(Control obj)
		{
			if (obj == null) return;
			if (obj is CommonControl)
			{
				(obj as CommonControl).Font = _font;
			}
			else if (obj is Container)
			{
				if (obj is Panel)
				{
					//Panel.ContextMenu
					if ((obj as Panel).ContextMenu != null)
					{
						//UpdateMenu((item as Panel).ContextMenu.Items, table);
					}
					//GroupBox
					if (obj is GroupBox)
					{
						UpdateControls(obj as GroupBox);
					}
				}
				//Container.Controls
				if ((obj as Container).Controls != null)
				{
					UpdateControls((obj as Container).Controls);
				}
			}
			else if (obj is GridView)
			{
				if ((obj as GridView).ContextMenu != null)
				{
					//UpdateMenu((item as GridView).ContextMenu.Items, table);
				}
			}
		}
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
			//AltLangRes.UpdateLanguage(this);
			UpdateForm.UpdateFont(this);
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
