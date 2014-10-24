using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_DbManager
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }

        public int IndexInList
        {
            get { return 4; }
        }
        public string LoadPath
        {
			get { return "shellmanager"; }
        }
    }
}
