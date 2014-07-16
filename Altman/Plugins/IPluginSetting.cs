namespace Altman.Plugins
{
    public interface IPluginSetting
    {
        /// <summary>
        /// 是否自动加载
        /// </summary>
        bool IsAutoLoad { get; }
        /// <summary>
        /// 是否需要shell数据
        /// </summary>
        bool IsNeedShellData { get; }
        /// <summary>
        /// 是否显示在右键菜单中
        /// </summary>
        bool IsShowInRightContext { get; }
    }
}
