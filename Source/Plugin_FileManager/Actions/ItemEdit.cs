using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemEdit : Command
	{
		public ItemEdit()
		{
			ID = "edit";
			MenuText = "Edit";
			Executed += ItemEdit_Executed;
		}

		void ItemEdit_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}