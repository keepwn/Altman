using System.Windows.Forms;
using PluginFramework;

namespace Plugin_FileManager
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Title
        {
            get { return "FileManager"; }
        }
        public string Group
        {
            get { return "webshell"; }
        }
        public string Version
        {
            get { return Application.ProductVersion; }
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
