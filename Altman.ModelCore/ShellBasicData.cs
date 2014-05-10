using System;
using System.Text;

namespace Altman.ModelCore
{
    public struct ShellBasicData
    {
        public string ShellUrl;
        public string ShellPwd;
        public string ShellType;
        public string ShellCoding;
        public int ShellTimeOut;

        public Encoding Encoding
        {
            get { return Encoding.GetEncoding(ShellCoding); }
        }

        public ShellBasicData(string url, string pwd, string type, string coding, int timeOut)
        {
            this.ShellUrl = url;
            this.ShellPwd = pwd;
            this.ShellType = type;
            this.ShellCoding = coding;
            this.ShellTimeOut = timeOut;
        }
    }
}
