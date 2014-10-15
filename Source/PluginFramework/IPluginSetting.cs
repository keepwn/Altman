namespace PluginFramework
{
    public interface IPluginSetting
    {
        /// <summary>
        /// 是否自动加载
        /// </summary>
        bool IsAutoLoad { get; }
        /// <summary>
        /// 排列顺序的比重，越小越靠前
        /// </summary>
        int IndexInList { get; }
        /// <summary>
        /// 加载路径
        /// </summary>
        string LoadPath { get; }
    }
}
