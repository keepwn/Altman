using Eto.Drawing;

namespace Plugin_DbManager
{
	public static class Icons
	{
		public static string Prefix = "Plugin_DbManager.Resources.";
		public static Icon ConnectIcon
		{
			get { return Icon.FromResource(Prefix + "server_connect.ico"); }
		}
		public static Icon DisconnectIcon
		{
			get { return Icon.FromResource(Prefix + "server_disconnect.ico"); }
		}
	}
}
