using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemCopy : Command
	{
		public ItemCopy()
		{
			ID = "copy";
			MenuText = "Copy";
			Executed += ItemCopy_Executed;
		}

		void ItemCopy_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}