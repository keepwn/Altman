using PluginFramework;

namespace Plugin_PluginManager
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }
        public bool IsNeedShellData
        {
            get { return false; }
        }
        public bool IsShowInRightContext
        {
            get { return false; }
        }
        public int IndexInList
        {
            get { return 100; }
        }
        public string LoadPath
        {
            get { return "system"; }
        }
    }
}
