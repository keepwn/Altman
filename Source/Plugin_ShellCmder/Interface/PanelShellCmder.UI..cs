using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellCmder.Controls;

namespace Plugin_ShellCmder.Interface
{
	partial class PanelShellCmder
	{
		void Init()
		{
			_consoleBoxCmder = new ConsoleBox()
			{
				IsWin = true,
				Prompt = ">>>",
				ShellTextBackColor = Colors.Black,
				ShellTextForeColor = Color.FromArgb(192, 192, 192),
				ShellTextFont = new Font(FontFamilies.Monospace, 10, style: FontStyle.Bold)
			};

			var layout = new DynamicLayout { Padding = new Padding(0) };
			layout.Add(_consoleBoxCmder);

			Content = layout;
		}

		private ConsoleBox _consoleBoxCmder;
	}
}
