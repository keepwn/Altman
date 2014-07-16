using Altman.Plugins;

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
    }
}
