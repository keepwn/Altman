using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;

namespace AltmanMef
{
	public static class ScriptScopeExtensionMethods
	{
		public static void InjectType(this ScriptScope scope, Type t)
		{
			string name = t.Name;
			if (t.IsGenericTypeDefinition)
				name = name.Substring(0, name.IndexOf('`'));
			scope.SetVariable(name, ClrModule.GetPythonType(t));
		}
	}
}
