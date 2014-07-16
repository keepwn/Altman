using System.Reflection;

using Altman.Plugins;

namespace Plugin_PluginManager
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Name
        {
            get { return "PluginManager"; }
        }
        public string FileName
        {
            get { return Assembly.GetExecutingAssembly().ManifestModule.Name; }
        }
        public string Group
        {
            get { return "webshell"; }
        }
        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        public string Author
        {
            get { return "Keepwn"; }
        }
        public string Description
        {
            get { return "Manage plugins"; }
        }
    }
}
