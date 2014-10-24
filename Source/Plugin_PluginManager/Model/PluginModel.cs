using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_PluginManager.Model
{
	public class PluginModel
	{
		public bool Checked { get; set; }
		public string Name { get; set; }
		public string FileName { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		public string InstalledVersion { get; set; }
		public string AvailableVersion { get; set; }
		public string RequiredVersion { get; set; }
		public string CanInstall { get; set; }
		public string CanUpdate { get; set; }
		public IPlugin Plugin;
		public object Tag;

		public PluginModel(IPlugin plugin)
		{
			Name = plugin.PluginInfo.Name;
			FileName = plugin.PluginInfo.FileName;
			Author = plugin.PluginInfo.Author;
			InstalledVersion = plugin.PluginInfo.Version;

			Plugin = plugin;
		}
		public PluginModel(string name)
		{
			Name = name;
		}
	}
}
