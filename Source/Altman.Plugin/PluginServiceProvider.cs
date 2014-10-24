using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Altman.Plugin.Interface;

namespace Altman.Plugin
{
	public static class PluginServiceProvider
	{
		public struct ServiceStore
		{
			public IPlugin Provider;
			public string TypeName;
			public Func<PluginParameter, dynamic> Service;
		}
		private static Dictionary<string, ServiceStore> _services;

		static PluginServiceProvider()
		{
			_services = new Dictionary<string, ServiceStore>();
		}

		public static void RegisterService(IPlugin provider, string serviceName, string serviceTypeName, Func<PluginParameter, dynamic> func)
		{
			if (!_services.ContainsKey(serviceName))
			{
				var store = new ServiceStore
				{
					Provider = provider,
					Service = func,
					TypeName = serviceTypeName,
				};
				_services.Add(serviceName, store);
			}
		}

		public static void RegisterService(string serviceName, string serviceTypeName, Func<PluginParameter, dynamic> func)
		{
			if (!_services.ContainsKey(serviceName))
			{
				var store = new ServiceStore
				{
					Provider = null,
					Service = func,
					TypeName = serviceTypeName,
				};
				_services.Add(serviceName, store);
			}
		}

		public static Func<PluginParameter, dynamic> GetService(string serviceName)
		{
			if (_services.ContainsKey(serviceName))
			{
				return _services[serviceName].Service;
			}
			return null;
		}

		public static ServiceStore GetServiceStore(string serviceName)
		{
			if (_services.ContainsKey(serviceName))
			{
				return _services[serviceName];
			}
			return default(ServiceStore);
		}

		public static IEnumerable<Func<PluginParameter, dynamic>> GetServices(string providerName)
		{
			return _services.Values.Where(r => r.Provider.PluginInfo.Name == providerName).Select(r => r.Service).ToList();
		}

		public static IEnumerable<Func<PluginParameter, dynamic>> GetServices(IPlugin provider)
		{
			return _services.Values.Where(r => r.Provider == provider).Select(r => r.Service).ToList();
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

		public static string GetServiceTypeName(string serviceName)
		{
			if (_services.ContainsKey(serviceName))
			{
				return _services[serviceName].TypeName;
			}
			throw new KeyNotFoundException("The service name not found.");
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