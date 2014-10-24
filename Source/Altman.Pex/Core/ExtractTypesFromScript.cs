using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronPython.Hosting;
using IronPython.Runtime;
using IronPython.Runtime.Types;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Scripting.Hosting;

namespace Altman.Pex.Core
{
	public class ExtractTypesFromScript
	{
		private static ScriptEngine _engine;

		public ExtractTypesFromScript(ScriptEngine engine)
		{
			_engine = engine;
		}

		public IEnumerable<IronPythonComposablePart> GetPartsFromScript(string scriptPath, IEnumerable<Type> injectTypes = null)
		{
			var script = _engine.CreateScriptSourceFromFile(scriptPath);
			return GetParts(GetTypesFromScript(script, injectTypes));
		}

		public IEnumerable<IronPythonComposablePart> GetPartsFromScript(ScriptSource script, IEnumerable<Type> injectTypes = null)
		{
			return GetParts(GetTypesFromScript(script, injectTypes));
		}

		public IEnumerable<IronPythonTypeWrapper> GetTypesFromScript(ScriptSource script,
			IEnumerable<Type> injectTypes = null)
		{
			CompiledCode code = script.Compile();
			var scope = _engine.CreateScope();

			var types = new[] {typeof (IronPythonImportDefinition)}.ToList();
			if (injectTypes != null)
			{
				types.AddRange(injectTypes);
			}

			foreach (Type type in types)
			{
				scope.InjectType(type);
			}
			using (var libStream = GetType().Assembly.GetManifestResourceStream("Altman.Pex.lib.py"))
			using ( var libText = new StreamReader(libStream))
			{
				var libSource = _engine.CreateScriptSourceFromString(libText.ReadToEnd());
				libSource.Execute(scope);
			}


			// "force" all classes to be new style classes
			dynamic metaclass;
			if (!scope.TryGetVariable("__metaclass__", out metaclass))
			{
				scope.SetVariable("__metaclass__", _engine.GetBuiltinModule().GetVariable("type"));
			}

			scope.SetVariable("clr", _engine.GetClrModule());
			code.Execute(scope);

			var pluginClasses = scope.GetItems()
				.Where(kvp => kvp.Value is PythonType && !kvp.Key.StartsWith("__"))
				.Select(kvp => new IronPythonTypeWrapper(_engine, kvp.Key, kvp.Value, scope.GetVariableHandle(kvp.Key)))
				.Where(kvp => !types.Contains(kvp.Type));

			return pluginClasses;
		}
		public IEnumerable<IronPythonComposablePart> GetParts(IEnumerable<IronPythonTypeWrapper> types)
		{
			foreach (var definedType in types)
			{
				dynamic type = definedType.PythonType;
				IEnumerable<object> exportObjects = new List();
				IDictionary<string, object> importObjects = new Dictionary<string, object>();
				IDictionary<object, object> exportMetadatas = new PythonDictionary();
				PythonDictionary pImportObjects = null;

				try
				{
					exportObjects = ((IEnumerable<object>)type.__exports__);
				}
				catch (RuntimeBinderException)
				{
				}
				try
				{
					foreach (var callable in ((IEnumerable)_engine.Operations.GetMemberNames(type)).Cast<string>()
						.Select(m => new { name = m, member = _engine.Operations.GetMember(type, m) })
						.Where(d => d.member != null)
						.Where(d => _engine.Operations.IsCallable(d.member)))
					{
						try
						{
							if (callable.member.im_func.func_dict.has_key("imports"))
							{
								var import = callable.member.im_func.func_dict["imports"];
								importObjects.Add(callable.name, import);
							}
						}
						catch (RuntimeBinderException) { }
					}
				}
				catch (RuntimeBinderException)
				{
				}

				if (importObjects.Count + exportObjects.Count() == 0)
				{
					continue;
				}
				else
				{
					try
					{
						exportMetadatas = ((PythonDictionary)type.__metadatas__);
					}
					catch (RuntimeBinderException)
					{
					}
				}


				var exports = exportObjects.Cast<PythonType>().Select(o => (Type)o);
				var imports =
					importObjects.Keys
						.Select(key => new KeyValuePair<string, IronPythonImportDefinition>(key, (IronPythonImportDefinition)importObjects[key]));
				yield return new IronPythonComposablePart(definedType, exports, imports, exportMetadatas);
			}
		}
	}
}
