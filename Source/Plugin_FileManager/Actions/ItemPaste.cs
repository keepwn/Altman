using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemPaste : Command
	{
		public ItemPaste()
		{
			ID = "paste";
			MenuText = "Paste";
			Executed += ItemPaste_Executed;
		}

		void ItemPaste_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}