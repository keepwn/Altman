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
		public static Icon RunScripIcon
		{
			get { return Icon.FromResource(Prefix + "run_script.ico"); }
		}

		public static class Database
		{
			public static Icon DatabaseStartIcon
			{
				get { return Icon.FromResource(Prefix + "0.database_start.ico"); }
			}
			public static Icon DatabaseFailedIcon
			{
				get { return Icon.FromResource(Prefix + "1.database_failed.ico"); }
			}
			public static Icon DatabaseIcon
			{
				get { return Icon.FromResource(Prefix + "2.database.ico"); }
			}
			public static Icon TableIcon
			{
				get { return Icon.FromResource(Prefix + "3.table.ico"); }
			}
			public static Icon TableFailedIcon
			{
				get { return Icon.FromResource(Prefix + "4.table_failed.ico"); }
			}
			public static Icon ColumnIcon
			{
				get { return Icon.FromResource(Prefix + "5.column.ico"); }
			}
		}
	}
}
