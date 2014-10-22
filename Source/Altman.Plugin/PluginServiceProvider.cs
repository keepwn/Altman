using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Altman.Plugin
{
	public static class PluginServiceProvider
	{
		public struct ServiceStore
		{
			public IPlugin Provider;
			public Assembly Assembly;
			public object Service;
			public string TypeName;
		}

		private static Dictionary<string, ServiceStore> _services;
		private static Dictionary<string, Type> _serviceTypes;
		static PluginServiceProvider()
		{
			_serviceTypes = new Dictionary<string, Type>();
			_services = new Dictionary<string, ServiceStore>();
		}

		public static void RegisterService<T>(IPlugin provider, string serviceName, T func)
		{
			if (!_services.ContainsKey(serviceName))
			{
				var store = new ServiceStore
				{
					Provider = provider,
					Assembly = provider != null ? provider.GetType().Assembly : null,
					Service = func,
					TypeName = "",
				};
				_services.Add(serviceName, store);
			}
		}

		public static void RegisterService<T>(IPlugin provider, string serviceName, string serviceTypeName, T func)
		{
			if (!_services.ContainsKey(serviceName))
			{
				if (_serviceTypes.ContainsKey(serviceTypeName))
				{
					if (_serviceTypes[serviceTypeName] != typeof (T))
						throw new ArgumentException("Type of the func argument is not in conformity with the defined service type.");
				}
				else
				{
					_serviceTypes.Add(serviceTypeName, typeof(T));
				}
				var store = new ServiceStore
				{
					Provider = provider,
					Assembly = provider!=null? provider.GetType().Assembly:null,
					Service = func,
					TypeName = serviceTypeName,
				};
				_services.Add(serviceName, store);
			}
		}

		public static T GetService<T>(string serviceName)
		{
			if (_services.ContainsKey(serviceName)
				&& _services[serviceName].Service is T)
			{
				return (T) _services[serviceName].Service;
			}
			return default(T);
		}

		public static ServiceStore GetServiceStore(string serviceName)
		{
			if (_services.ContainsKey(serviceName))
			{
				return _services[serviceName];
			}
			return default(ServiceStore);
		}

		public static IEnumerable<T> GetServices<T>()
		{
			return _services.Values.Select(r=> (T)r.Service).ToList();
		}

		public static IEnumerable<T> GetServices<T>(string providerName)
		{
			return _services.Values.Where(r => r.Provider.PluginInfo.Name == providerName).Select(r => (T)r.Service).ToList();
		}

		public static IEnumerable<T> GetServices<T>(IPlugin provider)
		{
			return _services.Values.Where(r => r.Provider == provider).Select(r => (T)r.Service).ToList();
		}

		public static IEnumerable<string> GetServiceNames()
		{
			return _services.Keys;
		}

		public static IEnumerable<string> GetServiceNames(string providerName)
		{
			return _services.Where(r => r.Value.Provider.PluginInfo.Name == providerName).Select(r => r.Key).ToList();
		}

		public static IEnumerable<string> GetServiceNames(IPlugin provider)
		{
			return _services.Where(r => r.Value.Provider == provider).Select(r => r.Key).ToList();
		}

		public static IEnumerable<string> GetServiceNamesByType(string serviceTypeName)
		{
			return _services.Where(r => r.Value.TypeName == serviceTypeName).Select(r => r.Key).ToList();
		}

		public static IEnumerable<string> GetServiceNames<T>()
		{
			return _services.Where(r => r.Value.Service is T).Select(r => r.Key).ToList();
		}

		public static IEnumerable<string> GetServiceNames<T>(string providerName)
		{
			return _services.Where(r => r.Value.Service is T && r.Value.Provider.PluginInfo.Name == providerName).Select(r => r.Key).ToList();
		}

		public static string GetServiceTypeName(string serviceName)
		{
			if (_services.ContainsKey(serviceName))
			{
				return _services[serviceName].TypeName;
			}
			throw new KeyNotFoundException("The service name not found.");
		}

		public static IEnumerable<string> GetServiceTypeNames()
		{
			return _serviceTypes.Keys;
		}

		public static IPlugin GetServiceProvider(string serviceName)
		{
			if (_services.ContainsKey(serviceName))
			{
				return _services[serviceName].Provider;
			}
			throw new KeyNotFoundException("The service name not found.");
		}
	}
}