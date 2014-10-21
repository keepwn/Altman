using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Altman.CustomControl;
using Eto.WinForms.Forms.Controls;
using SWF = System.Windows.Forms;
using EWF = Eto.WinForms;
using EF = Eto.Forms;

namespace Altman
{
	public class TabControlPlusHandler : EWF.WindowsContainer<TabControlPlus, EF.TabControl, EF.TabControl.ICallback>, EF.TabControl.IHandler
	{
		bool disableSelectedIndexChanged;
		public TabControlPlusHandler()
		{
			this.Control = new TabControlPlus();
			this.Control.DisplayStyle = TabStyle.Default;
			this.Control.DisplayStyleProvider.ShowTabCloser = true;
			this.Control.ImageList = new SWF.ImageList { ColorDepth = SWF.ColorDepth.Depth32Bit };
			this.Control.SelectedIndexChanged += (sender, e) =>
			{
				if (!disableSelectedIndexChanged)
					Callback.OnSelectedIndexChanged(Widget, e);
			};
		}

		public int SelectedIndex
		{
			get { return Control.SelectedIndex; }
			set { Control.SelectedIndex = value; }
		}

		public void InsertTab(int index, EF.TabPage page)
		{
			var pageHandler = (TabPageHandler)page.Handler;
			if (index == -1 || index == Control.TabPages.Count)
				Control.TabPages.Add(pageHandler.Control);
			else
				Control.TabPages.Insert(index, pageHandler.Control);
			if (Widget.Loaded && Control.TabPages.Count == 1)
				Callback.OnSelectedIndexChanged(Widget, EventArgs.Empty);
		}

		public void RemoveTab(int index, EF.TabPage page)
		{
			disableSelectedIndexChanged = true;
			try
			{
				var tab = Control.TabPages[index];
				var isSelected = Control.SelectedIndex == index;
				Control.TabPages.Remove(tab);
				if (isSelected)
					Control.SelectedIndex = Math.Min(index, Control.TabPages.Count - 1);
				if (Widget.Loaded)
					Callback.OnSelectedIndexChanged(Widget, EventArgs.Empty);
			}
			finally
			{
				disableSelectedIndexChanged = false;
			}
		}

		public void ClearTabs()
		{
			Control.TabPages.Clear();
		}
	}
}
