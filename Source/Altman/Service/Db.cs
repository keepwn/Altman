using System.Collections.Generic;
using System.Data;
using System.IO;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Altman.Service
{
    public class Db : IHostDb
    {
	    public Db()
	    {
		    Util.Data.Db.Init(Path.Combine(AppEnvironment.AppPath, "data.db3"));
	    }

        public bool CheckTable(string tableName)
        {
            return Util.Data.Db.CheckTable(tableName);
        }

        public bool InitTable(string tableName, string[] definition)
        {
            return Util.Data.Db.InitTable(tableName, definition);
        }

        public DataTable GetDataTable(string tableName)
        {
            return Util.Data.Db.GetDataTable(tableName);
        }

        public bool Insert(string tableName, Dictionary<string, object> data)
        {
            return Util.Data.Db.Insert(tableName, data);
        }

        public bool Update(string tableName, Dictionary<string, object> data, KeyValuePair<string,object> where)
        {
            return Util.Data.Db.Updata(tableName, data, where);
        }

        public bool Delete(string tableName, KeyValuePair<string, object> where)
        {
            return Util.Data.Db.Delete(tableName, where);
        }
    }
}
