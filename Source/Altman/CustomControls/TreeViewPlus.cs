using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Forms;

namespace Altman.Desktop.CustomControls
{
	public class TreeViewPlus : TreeView
	{
		private string _pathSeparator = "/";
		public string PathSeparator
		{
			get { return _pathSeparator; }
			set { _pathSeparator = value; }
		}

		public string SelectedFullPath
		{
			get
			{
				var path = string.Empty;
				if (this.SelectedItem == null)
					return path;
				var treeItem = this.SelectedItem as ITreeItem;
				if (treeItem == null)
					return path;
				path = treeItem.Text;
				while ((treeItem = treeItem.Parent) != null && treeItem.Text != null)
				{
					path = treeItem.Text + _pathSeparator + path;
				}
				return path;
			}
		}

		public void Expand(ITreeItem treeItem)
		{
			while (true)
			{
				if (treeItem == null)
					return;
				treeItem.Expanded = true;
				treeItem = treeItem.Parent;
			}
		}
	}
}
