using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace Altman.Pex.Core
{
	public class IronPythonComposablePart : ComposablePart
	{
		private readonly dynamic _instance;
		private readonly Dictionary<string, ImportDefinition> _imports;
		private readonly IList<ExportDefinition> _exports;
		private readonly IronPythonTypeWrapper _typeWrapper;

		public IronPythonComposablePart(IronPythonTypeWrapper typeWrapper, IEnumerable<Type> exports, IEnumerable<KeyValuePair<string, IronPythonImportDefinition>> imports, IDictionary<object, object> exportMetadatas)
		{
			_typeWrapper = typeWrapper;
			_instance = typeWrapper.Activator();
			_exports = new List<ExportDefinition>(exports.Count());
			_imports = new Dictionary<string, ImportDefinition>(imports.Count());
			foreach (var export in exports)
			{
				var metadata = new Dictionary<string, object>()
                                   {
                                       {"ExportTypeIdentity", AttributedModelServices.GetTypeIdentity(export)}
                                   };

				if (exportMetadatas != null)
				{
					foreach (var data in exportMetadatas)
					{
						metadata.Add(data.Key.ToString(), data.Value);
					}
				}

				var contractName = AttributedModelServices.GetContractName(export);
				_exports.Add(new ExportDefinition(contractName, metadata));
			}
			foreach (var import in imports)
			{
				var contractName = AttributedModelServices.GetContractName(import.Value.Type);
				var metadata = new Dictionary<string, Type>();

				_imports[import.Key] = new IronPythonContractBasedImportDefinition(
					import.Key,
					contractName,
					AttributedModelServices.GetTypeIdentity(import.Value.Type),
					metadata.ToList(),
					import.Value.Cardinality, import.Value.IsRecomposable, import.Value.IsPrerequisite,
					CreationPolicy.Any);
			}
		}

		public dynamic Instance
		{
			get { return _instance; }
		}

		public override object GetExportedValue(ExportDefinition definition)
		{
			return _instance;
		}

		public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
		{
			var importDefinition = definition as IronPythonContractBasedImportDefinition;
			if (importDefinition == null)
				throw new InvalidOperationException("ImportDefinition should have been an IronPythonContractBasedImportDefinition");

			_typeWrapper.InvokeMethodWithArgument(importDefinition.MethodName,
												  importDefinition.Cardinality == ImportCardinality.ExactlyOne
													  ? exports.Select(e => e.Value).SingleOrDefault()
													  : exports.Select(e => e.Value).ToList());
		}

		public override IEnumerable<ExportDefinition> ExportDefinitions
		{
			get { return _exports; }
		}

		public override IEnumerable<ImportDefinition> ImportDefinitions
		{
			get { return _imports.Values; }
		}
	}
}
