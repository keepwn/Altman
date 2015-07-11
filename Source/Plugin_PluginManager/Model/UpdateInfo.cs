using System;
using System.Collections.Generic;

namespace Plugin_PluginManager.Model
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

        public string Name;
        public string Author;
        public string Description;
        public VersionInfo Version;
        public string DownloadUrl;
        public bool CanUpdate;
    }
}
