using System;
using System.Windows.Forms;
using Altman.ModelCore;

namespace PluginFramework
{
    /// <summary>
    /// Host程序的接口
    /// </summary>
    public interface IHostService
    {
        Action<string> ShowMsgInStatusBar { get; set; }
        Action<string> ShowMsgInAppDialog { get; set; }
        Action<string,UserControl> CreateNewTabPage { get; set; }

        Func<ShellStruct, string, string[], byte[]> SubmitCommand { get; set; }
    }
}