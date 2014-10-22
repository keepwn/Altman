using Altman.Plugin;

namespace Plugin_DeveloperTool
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
