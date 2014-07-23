using PluginFramework;

namespace MyFirstPlugin
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }

        public bool IsNeedShellData
        {
            get { return true; }
        }

        public bool IsShowInRightContext
        {
            get { return true; }
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
