namespace Altman.Common.AltData
{
    public struct FileArgv
    {
        public string SourceFilePath;
        public string TargetFilePath;

        public FileArgv(string sourceFilePath, string targetFilePath)
        {
            this.SourceFilePath = sourceFilePath;
            this.TargetFilePath = targetFilePath;
        }
    }

    public struct OsFile
    {
        public string FileName;
        public string FileMTime;
        public string FileSize;
        public string FilePerms;

        public OsFile(string name, string mtime, string size, string perms)
        {
            this.FileName = name;
            this.FileMTime = mtime;
            this.FileSize = size;
            this.FilePerms = perms;
        }
    }

    public struct OsInfo
    {
        public string ShellDir;
        public string Platform;
        public string CurrentUser;
        public string DirSeparators;
    }

    public struct OsDisk
    {
        public string ShellDir;
        public string AvailableDisk;
    }

    public struct CmdResult
    {
        public string Result;
        public string CurrentDir;
    }
}
