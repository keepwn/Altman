namespace PluginFramework
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
