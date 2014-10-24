using System.Reflection;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Test_DoNetPlugin
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
			get { return "DoNetPluginTest"; }
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
			get { return "this is a .net plugin demo."; }
        }
    }
}
