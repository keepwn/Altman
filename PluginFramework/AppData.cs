using System.Collections.Generic;
using System.Data;
using Altman.DbCore;

namespace PluginFramework
{
    public class AppData : IAppData
    {
        public bool CheckTable(string tableName)
        {
            return Db.CheckTable(tableName);
        }

        public bool CreateTable(string tableName, List<string> definition)
        {
            return Db.CreateTable(tableName, definition);
        }

        public bool Insert(string tableName, Dictionary<string, string> dic)
        {
            return Db.Insert(tableName, dic);
        }

        public bool Updata(string tableName, Dictionary<string, string> dic, string where)
        {
            return Db.Updata(tableName, dic, where);
        }

        public bool Delete(string tableName, string where)
        {
            return Db.Delete(tableName, where);
        }

        public DataTable GetDataTable(string tableName)
        {
            return Db.GetDataTable(tableName);
        }
    }
}
