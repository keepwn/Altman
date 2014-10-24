using System;
using System.Runtime.Remoting;
using IronPython.Runtime.Types;
using Microsoft.Scripting.Hosting;

namespace Altman.Pex.Core
{
	public class IronPythonTypeWrapper
	{
		private readonly ScriptEngine _engine;
		private readonly ObjectHandle _classHandle;
		private ObjectHandle _instanceHandle;
		private object _instance;

		public IronPythonTypeWrapper(ScriptEngine engine, string name, PythonType pythonType, ObjectHandle classHandle)
		{
			Name = name;
			PythonType = pythonType;
			_engine = engine;
			_classHandle = classHandle;
		}

		public string Name { get; private set; }
		public PythonType PythonType { get; private set; }
		public Type Type { get { return PythonType; } }
		public object Activator()
		{
			_instanceHandle = _engine.Operations.Invoke(_classHandle, new object[] { });
			_instance = _engine.Operations.Unwrap<object>(_instanceHandle);
			return _instance;
		}

		public void InvokeMethodWithArgument(string methodName, object argument)
		{
			_engine.Operations.InvokeMember(_instance, methodName, argument);
		}
	}
}
