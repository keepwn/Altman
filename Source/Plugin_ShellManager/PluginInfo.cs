using System.Reflection;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_ShellManager
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
            get { return "ShellManager"; }
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
            get { return "Info about this shellManager"; }
        }
    }
}
