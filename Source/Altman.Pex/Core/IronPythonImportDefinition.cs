using System;
using System.ComponentModel.Composition.Primitives;

namespace Altman.Pex.Core
{
	public class IronPythonImportDefinition
	{
		private readonly ImportCardinality _cardinality;
		public string MethodName { get; set; }
		public Type Type { get; set; }
		public ImportCardinality Cardinality { get { return _cardinality; } }
		public bool IsRecomposable { get; set; }
		public bool IsPrerequisite { get; set; }

		public IronPythonImportDefinition(string methodName, Type type, string cardinality, bool isRecomposable, bool isPrerequisite)
		{
			MethodName = methodName;
			Type = type;
			_cardinality = Cardinality;
			if (!Enum.TryParse(cardinality, true, out _cardinality))
				throw new InvalidOperationException("Cardinality not convertable : {0}");
			IsRecomposable = isRecomposable;
			IsPrerequisite = isPrerequisite;
		}
	}
}
