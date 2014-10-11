using System;

namespace PluginFramework
{
    public interface IControlPlugin : IPlugin
    {
		/// <summary>
		/// 加载
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
	    object Load(PluginParameter param);
    }
}