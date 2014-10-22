namespace Altman.Plugin
{
    public interface IFormPlugin : IPlugin
    {
		/// <summary>
		/// 加载界面
		/// </summary>
		/// <param name="param"></param>
		/// <returns>返回为Form类型的对象</returns>
		object LoadGui(PluginParameter param);
    }
}
