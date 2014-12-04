using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_IPQuery
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }

        public int IndexInList
        {
            get { return 200; }
        }
        public string LoadPath
        {
			get { return ""; }
        }
    }
}
