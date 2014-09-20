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

		public static Bitmap AltmanPng
		{
			get { return Bitmap.FromResource(Prefix + "altman.png"); }
		}

		public static Bitmap AltmanBigPng
		{
			get { return Bitmap.FromResource(Prefix + "altman_big.png"); }
		}
	}
}
