using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Altman.Plugin.Interface;

namespace Altman.Plugin.Mef
{
	internal class ImportDoNetPlugins
	{
		[ImportMany(typeof(IPlugin), AllowRecomposition = true)]
		private List<IPlugin> Plugins = null;

		public List<IPlugin> Compose<T>(string pluginPath, T exportedValue)
		{
			var success = false;
			var pluginDir = pluginPath;

			// load .dll plugins
			var catalog = new AggregateCatalog();
			//catalog.Catalogs.Add(new DirectoryCatalog(pluginDir));
			// 只扫描插件目录的所有子目录下文件
			foreach (var dir in Directory.EnumerateDirectories(pluginDir))
			{
				catalog.Catalogs.Add(new DirectoryCatalog(dir, "*.dll"));
			}
			var container = new CompositionContainer(catalog);
			try
			{
				container.ComposeExportedValue(exportedValue);
				container.ComposeParts(this);
				success = true;
			}
			catch (CompositionException compositionException)
			{
				Debug.WriteLine(compositionException.Message);
				Debug.Assert(false);
			}
			catch (ReflectionTypeLoadException ex)
			{
				foreach (var item in ex.LoaderExceptions)
				{
					Debug.WriteLine(item.Message);
				}		
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return success ? Plugins : new List<IPlugin>();
		}
	}
}
