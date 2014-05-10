using System.Windows.Forms;
using PluginFramework;

namespace Plugin_ShellCmder
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Title
        {
            get { return "ShellCmder"; }
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
            get { return "Info about this shellcmder"; }
        }
    }
}
