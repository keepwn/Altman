namespace Altman.Plugins
{
    public interface IPluginAttribute
    {
        string Name { get; }
        string FileName { get; }
        string Group { get; }
        string Version { get; }
        string Author { get; }
        string Description { get; }
    }
}
