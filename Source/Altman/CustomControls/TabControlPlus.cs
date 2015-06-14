using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Eto.Forms
{
	public class TabControlCancelEventArgs : EventArgs
	{
		public int TabPageIndex { get; set; }
		public bool Cancel { get; set; }

		public TabControlCancelEventArgs(int tabPageIndex, bool cancel)
		{
			this.TabPageIndex = tabPageIndex;
			this.Cancel = cancel;
		}
	}

	public class TabControlEventArgs : EventArgs
	{
		public int TabPageIndex { get; set; }

		public TabControlEventArgs(int tabPageIndex)
		{
			this.TabPageIndex = tabPageIndex;
		}
	}

	[ContentProperty("Pages")]
	[Handler(typeof(TabControlPlus.IHandler))]
	public class TabControlPlus : Container
	{
		TabPageCollection pages;
		new IHandler Handler { get { return (IHandler)base.Handler; } }

		public override IEnumerable<Control> Controls
		{
			get { return pages; }
		}

		public event EventHandler<EventArgs> SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}

		public event EventHandler<TabControlCancelEventArgs> TabClosing;
		protected virtual void OnTabClosing(TabControlCancelEventArgs e)
		{
			if (TabClosing != null)
				TabClosing(this, e);
		}


		public event EventHandler<TabControlEventArgs> TabClosed;
		protected virtual void OnTabClosed(TabControlEventArgs e)
		{
			if (TabClosed != null)
				TabClosed(this, e);
		}


		public TabControlPlus()
		{
		}

		protected TabControlPlus(IHandler handler)
			: base(handler)
		{
		}

		public int SelectedIndex
		{
			get { return Handler.SelectedIndex; }
			set { Handler.SelectedIndex = value; }
		}

		public TabPage SelectedPage
		{
			get { return SelectedIndex < 0 ? null : Pages[SelectedIndex]; }
			set { SelectedIndex = pages.IndexOf(value); }
		}

		public Collection<TabPage> Pages
		{
			get { return pages ?? (pages = new TabPageCollection(this)); }
		}

		public override void Remove(Control child)
		{
			var page = child as TabPage;
			if (page != null)
			{
				Pages.Remove(page);
			}
		}

		public ControlBinding<TabControlPlus, int> SelectedIndexBinding
		{
			get
			{
				return new ControlBinding<TabControlPlus, int>(
					this,
					c => c.SelectedIndex,
					(c, v) => c.SelectedIndex = v,
					(c, h) => c.SelectedIndexChanged += h,
					(c, h) => c.SelectedIndexChanged -= h
				);
			}
		}
		class TabPageCollection : Collection<TabPage>
		{
			readonly TabControlPlus control;

			internal TabPageCollection(TabControlPlus control)
			{
				this.control = control;
			}

			protected override void InsertItem(int index, TabPage item)
			{
				base.InsertItem(index, item);
				control.SetParent(item, () => control.Handler.InsertTab(index, item));
			}

			protected override void ClearItems()
			{
				var pages = this.ToArray();
				for (int i = 0; i < pages.Length; i++)
				{
					control.Handler.RemoveTab(i, pages[i]);
					control.RemoveParent(pages[i]);
				}
				base.ClearItems();
			}

			protected override void RemoveItem(int index)
			{
				var page = this[index];
				control.Handler.RemoveTab(index, page);
				control.RemoveParent(page);
				base.RemoveItem(index);
			}
		}

		#region Callback

		static readonly object callback = new Callback();

		protected override object GetCallback() { return callback; }

		public new interface ICallback : Control.ICallback
		{
			/// <summary>
			/// Raises the selected index changed event.
			/// </summary>
			void OnSelectedIndexChanged(TabControlPlus widget, EventArgs e);

			/// <summary>
			/// Raises the tab closing event.
			/// </summary>
			void OnTabClosing(TabControlPlus widget, TabControlCancelEventArgs e);

			/// <summary>
			/// Raises the tab closed event.
			/// </summary>
			void OnTabClosed(TabControlPlus widget, TabControlEventArgs e);
		}

		protected new class Callback : Control.Callback, ICallback
		{
			public void OnSelectedIndexChanged(TabControlPlus widget, EventArgs e)
			{
				widget.Platform.Invoke(() => widget.OnSelectedIndexChanged(e));
			}

			public void OnTabClosing(TabControlPlus widget, TabControlCancelEventArgs e)
			{
				widget.Platform.Invoke(() => widget.OnTabClosing(e));
			}

			public void OnTabClosed(TabControlPlus widget, TabControlEventArgs e)
			{
				widget.Platform.Invoke(() => widget.OnTabClosed(e));
			}
		}

		#endregion

		#region Handler
		public new interface IHandler : Container.IHandler
		{
			int SelectedIndex { get; set; }

			void InsertTab(int index, TabPage page);

			void ClearTabs();

			void RemoveTab(int index, TabPage page);
		}

		#endregion
	}
}
