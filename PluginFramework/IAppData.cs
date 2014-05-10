using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PluginFramework
{
    public interface IAppData
    {
        bool CheckTable(string tableName);

        bool CreateTable(string tableName, List<string> definition);

        bool Insert(string tableName, Dictionary<string, string> dic);

        bool Updata(string tableName, Dictionary<string, string> dic, string where);

        bool Delete(string tableName, string where);

        DataTable GetDataTable(string tableName);
    }
}
