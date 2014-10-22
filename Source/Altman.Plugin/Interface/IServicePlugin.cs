namespace Altman.Plugin
{
	public interface IServicePlugin : IPlugin
	{
		/// <summary>
		/// 加载服务
		/// </summary>
		/// <returns>成功或失败</returns>
		bool LoadService();
	}
}
