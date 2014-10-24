using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_ShellManager
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return true; }
        }

        public int IndexInList
        {
            get { return 1; }
        }

        public string LoadPath
        {
            get { return ""; }
        }
    }
}
