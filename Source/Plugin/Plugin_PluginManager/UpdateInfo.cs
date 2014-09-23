using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin_PluginManager
{
    public class UpdateInfo
    {
        public struct VersionInfo
        {
            public Version Version;
            public Version RequiredVersion;
            public string Md5;

            public VersionInfo(string version, string required, string md5)
            {
                Version = new Version(version);
                RequiredVersion = new Version(required);
                Md5 = md5;
            }
        }

        public struct InstallInfo
        {
            public KeyValuePair<string, string>[] CopyFiles;
        }

        public struct UninstallInfo
        {
            public string[] DeleteFiles;
        }

        public string Name;
        public string Author;
        public string Description;
        public VersionInfo Version;
        public string DownloadUrl;
        public InstallInfo Install;
        public UninstallInfo Uninstall;
        public bool CanUpdate;
    }
}
