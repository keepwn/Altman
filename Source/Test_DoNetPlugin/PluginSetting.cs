using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Test_DoNetPlugin
{
    public class PluginSetting : IPluginSetting
    {
        public bool IsAutoLoad
        {
            get { return false; }
        }

        public int IndexInList
        {
            get { return 1; }
        }

        public string LoadPath
        {
			get { return "shellmanager"; }
        }
    }
}
