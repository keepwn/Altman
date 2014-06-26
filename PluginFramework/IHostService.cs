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
        ICoreInHostService Core { get; }
        IGuiInHostService Gui { get; }
        IDbInHostService Db { get; }
    }
}