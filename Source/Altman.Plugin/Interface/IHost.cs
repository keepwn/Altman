namespace Altman.Plugin
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