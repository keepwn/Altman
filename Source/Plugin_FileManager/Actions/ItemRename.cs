using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemRename : Command
	{
		public ItemRename()
		{
			ID = "rename";
			MenuText = "Rename";
			Executed += ItemRename_Executed;
		}

		void ItemRename_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}