using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemCreateFile : Command
	{
		public ItemCreateFile()
		{
			ID = "createFile";
			MenuText = "CreateFile";
			Executed += ItemCreateFile_Executed;
		}

		void ItemCreateFile_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}