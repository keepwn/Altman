using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_FileManager
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }

        public int IndexInList
        {
            get { return 3; }
        }
        public string LoadPath
        {
			get { return "shellmanager"; }
        }
    }
}
