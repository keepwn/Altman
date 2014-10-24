using System;
using Cairo;
using Eto.Forms;
using Eto.GtkSharp;
using Gdk;
using Gtk;
using Button = Gtk.Button;
using Color = Eto.Drawing.Color;
using Image = Gtk.Image;
using Label = Gtk.Label;

namespace Altman
{
	public class MultiTab : Box
	{
		public Label Caption;
		private Image img = new Image(Stock.Close, IconSize.Menu);
		public Button Close;
		public Notebook _parent;

		public MultiTab(string name)
		{
			CreateUi(name);
		}

		public MultiTab(string name, Notebook parent)
		{
			_parent = parent;
			CreateUi(name);
			CreateHandlers();
		}

		private void CreateUi(string name)
		{
			Caption = new Label(name+" ");
			Close = new Button
			{
				Image = new Image(Stock.Close, IconSize.Menu),
				TooltipText = "Close Tab",
				Relief = ReliefStyle.None
			};

			var rcStyle = new RcStyle
			{
				Xthickness = 0, 
				Ythickness = 0,
			};
			Close.ModifyStyle(rcStyle);
			PackStart(Caption);
			PackStart(Close);
			ShowAll();
			//Close.Hide();
		}

		private void CreateHandlers()
		{
			Close.Clicked += delegate
			{
				//_parent.RemovePage(_parent.CurrentPage);
				OnTabClosing(new TabControlCancelEventArgs(_parent.CurrentPage, false));
			};
		}

		public void OnTabClosing(TabControlCancelEventArgs e)
		{
			if (TabClosing != null)
				TabClosing(this, e);
		}

		public event EventHandler<TabControlCancelEventArgs> TabClosing;
		public bool Active;
	}

	public class TabControlPlusHandler : GtkContainer<Notebook, TabControl, TabControl.ICallback>, TabControl.IHandler
	{
		private MultiTab multiTab;
		public TabControlPlusHandler()
		{
			Control = new Notebook();
		}

		protected override void Initialize()
		{
			base.Initialize();
			Control.SwitchPage += Connector.HandleSwitchPage;
		}

		protected override bool IsTransparentControl
		{
			get { return false; }
		}

		protected override Color DefaultBackgroundColor
		{
			get { return ContainerContentControl.Style.Base(StateType.Normal).ToEto(); }
		}

		protected new TabControlConnector Connector
		{
			get { return (TabControlConnector) base.Connector; }
		}

		protected override WeakConnector CreateConnector()
		{
			return new TabControlConnector();
		}

		protected class TabControlConnector : GtkControlConnector
		{
			public new TabControlPlusHandler Handler
			{
				get { return (TabControlPlusHandler) base.Handler; }
			}

			public void HandleSwitchPage(object o, SwitchPageArgs args)
			{
				var handler = Handler;
				if (handler != null && handler.Widget.Loaded)
					handler.Callback.OnSelectedIndexChanged(handler.Widget, EventArgs.Empty);
			}
		}

		public int SelectedIndex
		{
			get { return Control.CurrentPage; }
			set { Control.CurrentPage = value; }
		}

		public void InsertTab(int index, TabPage page)
		{
			var pageHandler = (TabPageHandler) page.Handler;
			if (Widget.Loaded)
			{
				pageHandler.ContainerControl.ShowAll();
				pageHandler.LabelControl.ShowAll();
			}
			var tab = new MultiTab(pageHandler.Text, Control);
			tab.TabClosing += (sender, e) =>
			{
				Callback.OnTabClosing(Widget, new TabControlCancelEventArgs(e.TabPageIndex, e.Cancel));
			};

			if (index == -1)
				//Control.AppendPage(pageHandler.ContainerControl, pageHandler.LabelControl);
				Control.AppendPage(pageHandler.ContainerControl, tab);
			else
				//Control.InsertPage(pageHandler.ContainerControl, pageHandler.LabelControl, index);
				Control.AppendPage(pageHandler.ContainerControl, tab);
		}

		public void ClearTabs()
		{
			while (Control.NPages > 0)
				Control.RemovePage(0);
		}

		public void RemoveTab(int index, TabPage page)
		{
			Control.RemovePage(index);
			if (Widget.Loaded && Control.NPages == 0)
				Callback.OnSelectedIndexChanged(Widget, EventArgs.Empty);
		}
	}
}