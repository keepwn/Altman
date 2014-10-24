using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_PluginManager
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }
        public int IndexInList
        {
            get { return 10000; }
        }
        public string LoadPath
        {
            get { return ""; }
        }
    }
}
