using System;
using System.Collections.Generic;
using System.Linq;

namespace Altman.Plugin
{
	public class PluginParameter
	{
		public Dictionary<string, dynamic> Parameters;

		public PluginParameter()
		{
			Parameters = new Dictionary<string, dynamic>();
		}

		public PluginParameter(string key, dynamic value)
		{
			Parameters = new Dictionary<string, dynamic> {{key, value}};
		}

		public dynamic this[int index]
		{
			get
			{
				if (index < Parameters.Values.Count)
					return Parameters.Values.ToArray()[index];
				throw new IndexOutOfRangeException("index out of parameters's count");
			}
		}

		public void AddParameter(string key, dynamic value)
		{
			if (!Parameters.ContainsKey(key))
			{
				Parameters.Add(key, value);
			}
		}

		public dynamic GetParameter(string key)
		{
			if (Parameters.ContainsKey(key))
			{
				return Parameters[key];
			}
			throw new ArgumentException("Not Found Key In Parameters");
		}
	}
}