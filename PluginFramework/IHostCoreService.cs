using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface IHostCoreService
    {
        //core
        byte[] SubmitCommand(Shell data, string funcNameXpath, string[] param);
        string GetCustomShellTypeServerCode(string shellTypeName);
        string[] GetCustomShellTypeNameList();
        string[] GetDbNodeFuncCodeNameList(string shellTypeName);
        IEnumerable<IPlugin> GetPlugins();
        XmlNode GetShellHttpHeader(Shell data);
        XmlNode GetShellSqlConnection(Shell data);
    }
}
