using System;
using System.Collections.Generic;
using System.Text;
using Altman.Common.AltData;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_ShellCmder
{
    public class ShellCmder
    {
        private HostService _host;
        private ShellStruct _shellData;

        #region 属性
        public string ShellUrl
        {
            get { return this._shellData.ShellUrl; }
            set { this._shellData.ShellUrl = value; }
        }
        public string ShellPwd
        {
            get { return this._shellData.ShellPwd; }
            set { this._shellData.ShellPwd = value; }
        }
        public string ShellType
        {
            get { return this._shellData.ShellType; }
            set { this._shellData.ShellType = value; }
        }
        public string ShellCoding
        {
            get { return this._shellData.WebCoding; }
            set { this._shellData.WebCoding = value; }
        }
        public int ShellTimeOut
        {
            get { return this._shellData.TimeOut; }
            set { this._shellData.TimeOut = value; }
        }
        #endregion

        public ShellCmder(HostService host,ShellStruct data)
        {
            this._host = host;
            this._shellData = data;
        }

        public OsInfo GetSysInfo()
        {
            if (_host.SubmitCommand != null)
            {
                byte[] resultBytes = _host.SubmitCommand(_shellData, "SysInfoCode", null);
                return ResultMatch.MatchResultToOsInfo(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
            }
            else
            {
                _host.ShowMsgInAppDialog("SubmitCommand未赋值");
            }

            return new OsInfo();
        }

        public CmdResult ExecuteCmd(string cmdPath, string command,string currentDir,bool isWin)
        {
            //若cmdPath未设置，则采用默认值
            if (string.IsNullOrEmpty(cmdPath))
            {
                cmdPath = isWin ? "cmd" : "/bin/sh";
            }
            //组合cmd命令
            string combineCommand = string.Format(isWin ? "cd /d \"{0}\"&{1}&echo [S]&cd&echo [E]" : "cd \"{0}\";{1};echo [S];pwd;echo [E]", currentDir, command);

            if (_host.SubmitCommand != null)
            {
                byte[] resultBytes = _host.SubmitCommand(_shellData,"ExecuteCommandCode", new string[] {cmdPath, combineCommand});
                return ResultMatch.MatchResultToCmdResult(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
            }
            else
            {
                _host.ShowMsgInAppDialog("SubmitCommand未赋值");
            }
            return new CmdResult();
        }
    }
}
