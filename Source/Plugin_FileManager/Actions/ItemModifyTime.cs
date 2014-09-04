using System;
using Eto.Forms;

namespace Plugin_FileManager.Actions
{
	public class ItemModifyTime : Command
	{
		public ItemModifyTime()
		{
			ID = "modifyTime";
			MenuText = "ModifyTime";
			Executed += ItemModifyTime_Executed;
		}

		void ItemModifyTime_Executed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}