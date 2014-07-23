using PluginFramework;

namespace Plugin_ShellManager
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return true; }
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
            get { return 1; }
        }
        public string LoadPath
        {
            get { return "webshell"; }
        }
    }
}
