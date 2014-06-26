using System.Reflection;
using System.Windows.Forms;
using PluginFramework;

namespace Plugin_ShellManager
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Name
        {
            get { return "ShellManager"; }
        }
        public string FileName
        {
            get { return Assembly.GetExecutingAssembly().ManifestModule.Name; }
        }
        public string Group
        {
            get { return "altman"; }
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
