using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;

namespace Altman.Desktop
{
	public static class Icons
	{
		public static string Prefix = "Altman.Desktop.";
		public static Icon AltmanIcon
		{
			get { return Icon.FromResource(Prefix + "altman.ico"); }
		}
	}
}
