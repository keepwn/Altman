using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Altman.Webshell.Model;
using Eto.Forms;

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
		public FileManager FileManager;
		public IHost Host;
		public Shell ShellData;
	}
}
