namespace Altman.Plugin.Interface
{
    /// <summary>
    /// Host程序的接口
    /// </summary>
    public interface IHost
    {
        IHostApp App { get; }
        IHostCore Core { get; }
        IHostUi Ui { get; }
        IHostDb Database { get; }
    }
}