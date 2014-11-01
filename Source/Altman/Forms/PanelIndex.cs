using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Plugin;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Forms
{
	public class PanelIndex:Panel
	{
		public PanelIndex()
		{
			Init();
		}

		void Init()
		{
			var label1 = new Label
			{
				Text = "Welcome to",
				Font = new Font(FontFamilies.Monospace, 30)
			};
			var labelTitle = new Label
			{
				Text = "Altman3",
				Font = new Font(FontFamilies.Monospace, 60)
			};

			var layout = new PixelLayout();
			layout.Add(label1, new Point(70, 50));
			layout.Add(labelTitle, new Point(100, 120));

			var logo = PluginServiceProvider.GetService("ToFingerBinary");
			if (logo != null)
			{
				var rnd = new Random();
				var par = new PluginParameter();
				par.AddParameter("str", rnd.Next(1, 1023));
				var ret = logo(par);
				var tmp = new Label
				{
					Text = ret,
					Font = new Font(FontFamilies.Monospace, 10)
				};
				layout.Add(tmp, new Point(300, 220));
			}

			Content = layout;
		}
	}
}
