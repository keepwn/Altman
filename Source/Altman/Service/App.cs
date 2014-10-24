using System;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Altman.Service
{
    public class App:IHostApp
    {
        public Version AppVersion
        {
			get { return AppEnvironment.AppVersion; }
        }

        public string AppCurrentDir
        {
			get { return AppEnvironment.AppPath; }
        }

        public string AppPluginDir
        {
			get { return AppEnvironment.AppPluginPath; }
        }

        public string AppPluginConfigDir
        {
			get { return AppEnvironment.AppPluginConfigPath; }
        }
        public string AppBinDir
        {
			get { return AppEnvironment.AppBinPath; }
        }

        public string AppLanguageDir
        {
			get { return AppEnvironment.AppLanguagePath; }
        }
    }
}
