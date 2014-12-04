using System.Reflection;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_IPQuery
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
			get { return "IPQuery"; }
        }
        public string FileName
        {
            get { return Assembly.GetExecutingAssembly().ManifestModule.Name; }
        }
        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        public string Author
        {
            get { return "Keepwn"; }
        }
        public string Description
        {
			get { return "IP地址归属地查询工具，数据来自于17MON.CN。"; }
        }
    }
}
