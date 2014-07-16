using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Altman.DbCore;
using Altman.ModelCore;
using Altman.Plugins;

namespace Altman
{
    public class DbService : IHostDbService
    {
        public DataTable GetDataTable()
        {
            return DbManager.GetDataTable();
        }

        public void Insert(Shell model)
        {
            DbManager.Insert(model);
        }

        public void Update(int id, Shell model)
        {
            DbManager.Update(id, model);
        }

        public void Delete(int id)
        {
            DbManager.Delete(id);
        }
    }
}
