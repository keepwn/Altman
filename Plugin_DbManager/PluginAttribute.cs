using PluginFramework;

namespace Plugin_DbManager
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Title
        {
            get { return "DbManager"; }
        }
        public string Group
        {
            get { return "webshell"; }
        }
        public string Version
        {
            get { return "1.0"; }
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
