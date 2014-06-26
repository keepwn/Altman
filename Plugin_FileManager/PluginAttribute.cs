using System.Reflection;
using System.Windows.Forms;
using PluginFramework;

namespace Plugin_FileManager
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Name
        {
            get { return "FileManager"; }
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
            get { return "fileManager provides server's file management function"; }
        }
    }
}
