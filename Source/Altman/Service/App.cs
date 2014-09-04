using System;
using System.Reflection;
using PluginFramework;

namespace Altman.Desktop.Service
{
    public class App:IHostApp
    {
        public Version AppVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public string AppCurrentDir
        {
            get { return Environment.CurrentDirectory; }
        }

        public string AppPluginDir
        {
            get { return "Plugins"; }
        }

        public string AppPluginConfigDir
        {
            get { return "Plugins/Config"; }
        }
        public string AppBinDir
        {
            get { return "Bin"; }
        }

        public string AppLanguageDir
        {
            get { return "Languages"; }
        }
    }
}
