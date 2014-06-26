using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Altman.ModelCore;

namespace PluginFramework
{
    public interface IDbInHostService
    {
        //db
        DataTable GetDataTable();
        void Insert(ShellStruct data);
        void Update(int id, ShellStruct data);
        void Delete(int id);
    }
}
