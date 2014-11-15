namespace Altman.Plugin.Interface
{
    public interface IPluginInfo
    {
		/// <summary>
		/// 插件名
		/// </summary>
        string Name { get; }

		/// <summary>
		/// 插件文件名
		/// </summary>
        string FileName { get; }

		/// <summary>
		/// 版本号
		/// </summary>
        string Version { get; }

		/// <summary>
		/// 作者
		/// </summary>
        string Author { get; }

		/// <summary>
		/// 描述
		/// </summary>
        string Description { get; }
    }
}
