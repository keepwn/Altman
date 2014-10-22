using System.Collections.Generic;
using Eto.Forms;

namespace Plugin_FileManager.Controls
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
				var treeItem = this.SelectedItem;
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
			var root = treeItem;
			var catchTreeItem = new List<ITreeItem>();
			while (true)
			{
				if (root == null)
					break;
				catchTreeItem.Insert(0, root);
				root = root.Parent;
			}
			foreach (var item in catchTreeItem)
			{
				(item as TreeItem).Expanded = true;
			}
		}
	}
}
