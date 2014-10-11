using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PluginFramework
{
	public static class PluginProvider
	{
		private class PluginsImport
		{
			[ImportMany(typeof(IPlugin), AllowRecomposition = true)]
			public IEnumerable<IPlugin> Plugins = null;
		}
		private static PluginsImport _import;
		private static IHost _host;

		static PluginProvider()
		{
		}

		public static IHost Host
		{
			get { return _host; }
			set { _host = value; }
		}

		public static IEnumerable<IPlugin> Plugins
		{
			get { return _import.Plugins; }
		}

		public static bool Compose(string pluginPath)
		{
			_import = new PluginsImport();

			var success = false;
			var pluginDir = pluginPath;

			// load .dll plugins
			var catalog = new AggregateCatalog();
			//catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			//catalog.Catalogs.Add(new DirectoryCatalog(pluginDir));
			//只扫描插件目录的所有子目录下文件
			foreach (var dir in Directory.EnumerateDirectories(pluginDir))
			{
				catalog.Catalogs.Add(new DirectoryCatalog(dir, "*.dll"));
			}
			var container = new CompositionContainer(catalog);
			try
			{
				container.ComposeExportedValue(_host);
				container.ComposeParts(_import);
				success = true;
			}
			catch (CompositionException compositionException)
			{
				Debug.WriteLine(compositionException.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return success;
		}

		public static IEnumerable<IPlugin> GetPlugins()
		{
			return Plugins;
		}

		public static IEnumerable<IPlugin> GetChildPlugins(IPlugin plugin)
		{
			return Plugins.Where(r => r.PluginSetting.LoadPath == plugin.PluginInfo.Name);
		}

		public static IPlugin GetParentPlugin(IPlugin plugin)
		{
			return Plugins.FirstOrDefault(r => r.PluginInfo.Name == plugin.PluginSetting.LoadPath);
		}
	}

	public static class PluginServiceProvider
	{
		private static Dictionary<string, object> _services;
		static PluginServiceProvider()
		{
			_services = new Dictionary<string, object>();
		}

		public static void RegisterService<T>(string name, T func)
		{
			if (!_services.ContainsKey(name))
			{
				_services.Add(name, func);
			}
		}

		public static T GetService<T>(string name)
		{
			if (_services.ContainsKey(name) && _services[name] is T)
			{
				return (T)_services[name];
			}
			return default(T);
		}
	}
}
