/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

namespace Altman.Controls.TabControl.TabStyleProviders
{
    [System.ComponentModel.ToolboxItem(false)]
	public class TabStyleNoneProvider : TabStyleProvider
	{
		public TabStyleNoneProvider(CustomTabControl tabControl) : base(tabControl){
		}
		
		public override void AddTabBorder(System.Drawing.Drawing2D.GraphicsPath path, System.Drawing.Rectangle tabBounds){
		}
	}
}
