using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Forms;

namespace Test_DoNetPlugin
{
	public class PanelOption : Dialog
	{
		public PanelOption()
		{
			var layout = new DynamicLayout {Padding = new Eto.Drawing.Padding(10, 10)};
			layout.AddRow(new Label {Text = "This a test of show option"});

			Content = layout;
			Title = "Option";
		}
	}
}
