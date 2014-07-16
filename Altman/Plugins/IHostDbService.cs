using System.Data;
using Altman.ModelCore;

namespace Altman.Plugins
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
