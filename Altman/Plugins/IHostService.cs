namespace Altman.Plugins
{
    /// <summary>
    /// Host程序的接口
    /// </summary>
    public interface IHostService
    {
        IHostCoreService Core { get; }
        IHostGuiService Gui { get; }
        IHostDbService Db { get; }
    }
}