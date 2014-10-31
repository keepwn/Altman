using Eto.Drawing;

namespace Plugin_FileManager.Resources
{
	public static class Icons
	{
		public static string Prefix = "Plugin_FileManager.Resources.";
		public static class TreeType
		{
			public static Icon DriveIcon
			{
				get { return Icon.FromResource(Prefix + "drive.ico"); }
			}
			public static Icon FloderIcon
			{
				get { return Icon.FromResource(Prefix + "floder.ico"); }
			}
		}

		public static class FileType
		{
			public static Icon GetIcon(string name)
			{
				try
				{
					return Icon.FromResource(Prefix + name);
				}
				catch
				{
					return Icon.FromResource(Prefix + "0_unknow_32.ico");
				}
			}
		}
	}
}
