using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Altman.ModelCore;

namespace PluginFramework
{
    /// <summary>
    /// Host程序的接口
    /// </summary>
    public interface IHostService
    {
        IHostCoreService Core { get; }
        IHostGuiService Gui { get; }
        IHostDbService Db { get; }
    }
}