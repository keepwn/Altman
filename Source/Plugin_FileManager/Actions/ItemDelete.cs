using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemDelete : Command
	{
		public ItemDelete()
		{
			ID = "delete";
			MenuText = "Delete";
			Executed += ItemDelete_Executed;
		}

		void ItemDelete_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}