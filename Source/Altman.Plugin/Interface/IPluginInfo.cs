namespace Altman.Plugin
{
    public interface IPluginInfo
    {
        string Name { get; }
        string FileName { get; }
        string Version { get; }
        string Author { get; }
        string Description { get; }
    }
}
