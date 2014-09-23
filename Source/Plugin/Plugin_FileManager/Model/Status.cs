using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Model;
using Eto.Forms;
using PluginFramework;

namespace Plugin_FileManager.Model
{
	public class Status
	{
		public string PathSeparator;
		public string CurrentDirPath;
		public string CopyFileFullPath;
		public string CopyFileName;
		public string DirWillBeProcessed;
		public string FileWillBeProcessed;
		public GridView FileGridView;
		public FileManagerService FileManager;
		public IHost Host;
		public Shell ShellData;
	}
}
