using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.ModelCore;

namespace PluginFramework
{
    public class HostService:IHostService
    {
        public Action<string> ShowMsgInStatusBar { get; set; }
        public Action<string> ShowMsgInAppDialog { get; set; }

        public Action<string,UserControl> CreateNewTabPage { get; set; }

        /// <summary>
        /// 发送命令(ShellBasicData,funcName,params,result)
        /// </summary>
        public Func<ShellStruct,string, string[], byte[]> SubmitCommand { get; set; }

    }
}
