using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;

namespace IPythonMef
{
	public class IronPythonContractBasedImportDefinition : ContractBasedImportDefinition
	{
		public string MethodName { get; private set; }

		public IronPythonContractBasedImportDefinition(string methodName, string contractName,
													   string requiredTypeIdentity,
													   IEnumerable<KeyValuePair<string, Type>> requiredMetadata,
													   ImportCardinality cardinality, bool isRecomposable,
													   bool isPrerequisite, CreationPolicy requiredCreationPolicy)
			: base(
				contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite,
				requiredCreationPolicy)
		{
			MethodName = methodName;
		}
	}
}
