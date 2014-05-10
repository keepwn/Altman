using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginFramework;

namespace MyFirstPlugin
{
    public class PluginAttribute : IPluginAttribute
    {
        public string Title
        {
            get { return "MyFirstPlugin"; }
        }
        public string Group
        {
            get { return "webshell"; }
        }
        public string Version
        {
            get { return "1.0"; }
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
