using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Altman.Plugin.Interface;
using Altman.Plugin.Mef;

namespace Altman.Plugin
{
	public static class PluginProvider
	{
		private static IHost _host;
		public static IHost Host
		{
			get { return _host; }
			set { _host = value; }
		}

		private static List<IPlugin> _plugins;
		public static List<IPlugin> Plugins
		{
			get { return _plugins ?? new List<IPlugin>(); }
		}
		private static List<IService> _services;
		public static List<IService> Services
		{
			get { return _services ?? new List<IService>(); }
		}

		public static void Compose(string pluginPath, string servicesPath, bool isLoadPy=false)
		{
			List<IPlugin> donetPlugins;
			List<IPlugin> pyPlugins;
			donetPlugins = new ImportDoNetPlugins().Compose(pluginPath, _host);
			if (isLoadPy)
			{
				var py = new ImportPythonPlugins();
				py.Compose(pluginPath, servicesPath, _host);
				if (py.Plugins != null)
					donetPlugins.AddRange(py.Plugins);
				_services = py.Services ?? new List<IService>();
			}
			_plugins = donetPlugins;
		}

		public static IEnumerable<IPlugin> GetPlugins()
		{
			return Plugins.OrderBy(p => p.PluginSetting.IndexInList).ThenBy(p => p.PluginInfo.Name);
		}

		public static IEnumerable<IPlugin> GetChildPlugins(IPlugin plugin)
		{
			return Plugins.Where(r => String.Equals(r.PluginSetting.LoadPath, plugin.PluginInfo.Name, StringComparison.CurrentCultureIgnoreCase));
		}

		public static IEnumerable<IPlugin> GetRootPlugins()
		{
			return Plugins.Where(r => r.PluginSetting.LoadPath == "");
		}

		public static IPlugin GetParentPlugin(IPlugin plugin)
		{
			return Plugins.FirstOrDefault(r => r.PluginInfo.Name == plugin.PluginSetting.LoadPath);
		}

		public static void AddPlugins(IEnumerable<IPlugin> plugins)
		{
			if (Plugins != null)
			{
				Plugins.AddRange(plugins);
			}
		}
	}
}
