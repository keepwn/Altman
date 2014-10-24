using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_Encoder
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }

        public int IndexInList
        {
            get { return 100; }
        }
        public string LoadPath
        {
			get { return ""; }
        }
    }
}
