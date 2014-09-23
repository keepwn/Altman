using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using PluginFramework;

namespace MyFirstPlugin
{
    public class PluginInfo : IPluginInfo
    {
        public string Name
        {
            get { return "MyFirstPlugin"; }
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
            get { return "Info about this my first plugin"; }
        }
    }
}
