using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface IHostDbService
    {
        //db
        DataTable GetDataTable();
        void Insert(Shell data);
        void Update(int id, Shell data);
        void Delete(int id);
    }
}
