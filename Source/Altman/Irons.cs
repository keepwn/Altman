using Eto.Drawing;

namespace Altman
{
	public static class Icons
	{
		public static string Prefix = "Altman.";
		public static Icon AltmanIcon
		{
			get { return Icon.FromResource(Prefix + "altman.ico"); }
		}

		public static Bitmap AltmanPng
		{
			get { return Bitmap.FromResource(Prefix + "altman.png"); }
		}

		public static Bitmap AltmanAboutPng
		{
			get { return Bitmap.FromResource(Prefix + "altman_about.png"); }
		}
	}
}
