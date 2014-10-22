using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using IronPython.Hosting;
using Plugin_PythonEx.Core;

namespace Altman.Plugin.Mef
{
	internal class PythonMef
	{
		[ImportMany(typeof(IPlugin), AllowRecomposition = true)]
		private List<IPlugin> Plugins = null;

		public List<IPlugin> Compose<T>(string pluginPath, T exportedValue)
		{
			var success = false;
			var pluginDir = pluginPath;
			try
			{
				// load .py plugins
				var pythonFiles = new List<FileInfo>();
				foreach (var dir in Directory.EnumerateDirectories(pluginDir))
				{
					var dirInfo = new DirectoryInfo(dir);
					// add .py
					var file = dirInfo.GetFiles("*.py");
					pythonFiles.AddRange(file);
				}

				// create python
				var engine = Python.CreateEngine();
				engine.Runtime.LoadAssembly(Assembly.LoadFrom("IronPython.Stdlib.dll"));
				//var paths = engine.GetSearchPaths();
				//paths.Add(currentDir);
				//engine.SetSearchPaths(paths);

				// configure the engine with types
				var typesYouWantPythonToHaveAccessTo = new[] {typeof (IPlugin), typeof (IHost)};
				var typeExtractor = new ExtractTypesFromScript(engine);

				// add parts
				var parts = new List<ComposablePart>();
				foreach (var py in pythonFiles)
				{
					var exports = typeExtractor.GetPartsFromScript(py.FullName, typesYouWantPythonToHaveAccessTo);
					parts.AddRange(exports);
				}
				var batch = new CompositionBatch(parts, new ComposablePart[] {});
				var container = new CompositionContainer();

				container.ComposeExportedValue(exportedValue);
				container.Compose(batch);
				container.ComposeParts(this);

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
			return success ? Plugins : new List<IPlugin>();
		}
	}
}
