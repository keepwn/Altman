using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Altman.Pex.Core;
using Altman.Plugin.Interface;
using IronPython.Hosting;

namespace Altman.Plugin.Mef
{
	internal class ImportPythonPlugins
	{
		private class Imports
		{
			[ImportMany(typeof(IPlugin), AllowRecomposition = true)]
			public List<IPlugin> Plugins = null;

			[ImportMany(typeof(IService), AllowRecomposition = true)]
			public List<IService> Services = null;
		}

		private Imports _imports;

		public List<IPlugin> Plugins
		{
			get { return _imports.Plugins; }
		}

		public List<IService> Services
		{
			get { return _imports.Services; }
		}

		public void Compose<T>(string pluginPath,string servicePath, T exportedValue)
		{
			_imports = new Imports();
			var success = false;
			var pluginDir = pluginPath;
			try
			{
				// load .py
				var pythonFiles = new List<FileInfo>();
				// service
				pythonFiles.AddRange(new DirectoryInfo(servicePath).GetFiles("*.py"));
				// plugins
				foreach (var dir in Directory.EnumerateDirectories(pluginDir))
				{
					var dirInfo = new DirectoryInfo(dir);
					var file = dirInfo.GetFiles("*.py");
					pythonFiles.AddRange(file);
				}

				// create python
				var engine = Python.CreateEngine();
				var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
				var dlla = Path.Combine(path, "IronPython.Stdlib.dll");
				var dllb = Path.Combine(path, "Bin", "IronPython.Stdlib.dll");
				if (File.Exists(dlla))
				{
					engine.Runtime.LoadAssembly(Assembly.LoadFrom(dlla));
				}
				else if (File.Exists(dllb))
				{
					engine.Runtime.LoadAssembly(Assembly.LoadFrom(dllb));
				}
				//var paths = engine.GetSearchPaths();
				//paths.Add(currentDir);
				//engine.SetSearchPaths(paths);

				// configure the engine with types
				var typesInjectTo = new[] {typeof (IPlugin), typeof (IHost), typeof (IService)};
				var typeExtractor = new ExtractTypesFromScript(engine);

				// add parts
				var parts = new List<ComposablePart>();
				foreach (var py in pythonFiles)
				{
					var exports = typeExtractor.GetPartsFromScript(py.FullName, typesInjectTo);
					parts.AddRange(exports);
				}
				var batch = new CompositionBatch(parts, new ComposablePart[] {});
				var container = new CompositionContainer();

				container.ComposeExportedValue(exportedValue);
				container.Compose(batch);
				container.ComposeParts(_imports);
				success = true;
			}
			catch (CompositionException compositionException)
			{
				Debug.WriteLine(compositionException.Message);
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
		}
	}
}
