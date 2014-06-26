using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface ICoreInHostService
    {
        //core
        byte[] SubmitCommand(ShellStruct data, string funcNameXpath, string[] param);
        string GetCustomShellTypeServerCode(string shellTypeName);
        string[] GetCustomShellTypeNameList();
        string[] GetDbNodeFuncCodeNameList(string shellTypeName);
        IEnumerable<IPlugin> GetPlugins();
    }
}
