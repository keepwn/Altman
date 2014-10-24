using System.Reflection;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_DbManager
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
            get { return "DbManager"; }
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
            get { return "Manager database in server"; }
        }
    }
}
