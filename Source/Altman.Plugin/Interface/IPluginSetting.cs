namespace Altman.Plugin.Interface
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
        /// 插件装载点，默认为空，如果需要作为子插件，则可以填写父插件名。
        /// </summary>
        string LoadPath { get; }
    }
}
