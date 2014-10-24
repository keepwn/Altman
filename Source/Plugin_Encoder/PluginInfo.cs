using System.Reflection;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_Encoder
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
			get { return "Encoder"; }
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
			get { return "Encode and decode something."; }
        }
    }
}
