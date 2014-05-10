using System;
using System.Collections.Generic;
using Altman.Common.AltData;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_ShellCmder
{
    public class ShellCmder
    {
        private HostService _host;
        private ShellBasicData _shellBasicData;

        #region 属性
        public string ShellUrl
        {
            get { return this._shellBasicData.ShellUrl; }
            set { this._shellBasicData.ShellUrl = value; }
        }
        public string ShellPwd
        {
            get { return this._shellBasicData.ShellPwd; }
            set { this._shellBasicData.ShellPwd = value; }
        }
        public string ShellType
        {
            get { return this._shellBasicData.ShellType; }
            set { this._shellBasicData.ShellType = value; }
        }
        public string ShellCoding
        {
            get { return this._shellBasicData.ShellCoding; }
            set { this._shellBasicData.ShellCoding = value; }
        }
        public int ShellTimeOut
        {
            get { return this._shellBasicData.ShellTimeOut; }
            set { this._shellBasicData.ShellTimeOut = value; }
        }
        #endregion

        public ShellCmder(HostService host,ShellBasicData data)
        {
            this._host = host;
            this._shellBasicData = data;
        }

        public OsInfo GetSysInfo()
        {
            if (_host.SubmitCommand != null)
            {
                byte[] resultBytes = _host.SubmitCommand(_shellBasicData, "SysInfoCode", null);
                return ResultMatch.MatchResultToOsInfo(resultBytes, _shellBasicData.Encoding);
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
                byte[] resultBytes = _host.SubmitCommand(_shellBasicData,"ExecuteCommandCode", new string[] {cmdPath, combineCommand});
                return ResultMatch.MatchResultToCmdResult(resultBytes, _shellBasicData.Encoding);
            }
            else
            {
                _host.ShowMsgInAppDialog("SubmitCommand未赋值");
            }
            return new CmdResult();
        }
    }
}
