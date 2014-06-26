using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Altman.DbCore;
using Altman.ModelCore;
using PluginFramework;

namespace Altman
{
    public class DbService : IDbInHostService
    {
        public DataTable GetDataTable()
        {
            return DbManager.GetDataTable();
        }

        public void Insert(ShellStruct model)
        {
            DbManager.Insert(model);
        }

        public void Update(int id, ShellStruct model)
        {
            DbManager.Update(id, model);
        }

        public void Delete(int id)
        {
            DbManager.Delete(id);
        }
    }
}
