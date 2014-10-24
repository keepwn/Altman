using System;

namespace Altman.Plugin.Interface
{
	public interface IPlugin : IService, IDisposable
	{
		IPluginInfo PluginInfo { get; }

		IPluginSetting PluginSetting { get; }
	}
}
