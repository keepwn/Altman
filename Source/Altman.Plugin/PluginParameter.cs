using System;
using System.Collections.Generic;
using System.Linq;

namespace Altman.Plugin
{
	public class PluginParameter
	{
		public Dictionary<string, object> Parameters;

		public PluginParameter()
		{
			Parameters = new Dictionary<string, object>();
		}

		public object this[int index]
		{
			get
			{
				if (index < Parameters.Values.Count)
					return Parameters.Values.ToArray()[index];
				throw new IndexOutOfRangeException("index out of parameters's count");
			}
		}

		public void AddParameter(string key,object value)
		{
			if (!Parameters.ContainsKey(key))
			{
				Parameters.Add(key, value);
			}
		}

		public object GetParameter(string key)
		{
			if (Parameters.ContainsKey(key))
			{
				return Parameters[key];
			}
			throw new ArgumentException("Not Found Key In Parameters");
		}
	}
}