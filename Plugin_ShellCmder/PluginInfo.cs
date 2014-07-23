using System.Reflection;

using PluginFramework;

namespace Plugin_ShellCmder
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
            get { return "ShellCmder"; }
        }
        public string FileName
        {
            get { return Assembly.GetExecutingAssembly().ManifestModule.Name; }
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
            get { return "Info about this shellcmder"; }
        }
    }
}
