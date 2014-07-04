using System;
using System.Data;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_ShellManager
{
    /// <summary>
    /// ShellManagerService类,用于管理webshell
    /// </summary>
    public class ShellManagerService
    {
        private IHostService _host;

        public class CompletedEventArgs :EventArgs
        {
            private Exception _error;
            public CompletedEventArgs(Exception error)
            {
                _error = error;
            }
            public Exception Error
            {
                get { return _error; }
            }
        }

        public event EventHandler<CompletedEventArgs> DeleteCompletedToDo;
        public event EventHandler<CompletedEventArgs> InsertCompletedToDo;
        public event EventHandler<CompletedEventArgs> UpdateCompletedToDo;
        public event EventHandler<CompletedEventArgs> GetDataTableCompletedToDo;
        public event EventHandler<CompletedEventArgs> RefreshShellStatusCompletedToDo;


        public ShellManagerService(IHostService host)
        {
            this._host = host;
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Exception error = null;
            try
            {
                _host.Db.Delete(id);
            }
            catch (Exception ex)
            {
                error = ex;
                if (DeleteCompletedToDo != null)
                {
                    DeleteCompletedToDo(null,new CompletedEventArgs(error));
                }
            }
            
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        public void Insert(Shell model)
        {
            Exception error = null;
            try
            {
                _host.Db.Insert(model);
            }
            catch (Exception ex)
            {
                error = ex;
                if (InsertCompletedToDo != null)
                {
                    InsertCompletedToDo(null, new CompletedEventArgs(error));
                }
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void Update(int id, Shell model)
        {
            Exception error = null;
            try
            {
                _host.Db.Update(id, model);
            }
            catch (Exception ex)
            {
                error = ex;
                if (UpdateCompletedToDo != null)
                {
                    UpdateCompletedToDo(null, new CompletedEventArgs(error));
                }
            }
        }
        /// <summary>
        /// 获取数据库表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {           
            Exception error = null;
            try
            {
                return _host.Db.GetDataTable();
            }
            catch (Exception ex)
            {
                error = ex;
                if (GetDataTableCompletedToDo != null)
                {
                    GetDataTableCompletedToDo(null, new CompletedEventArgs(error));
                }
                return null;
            }
        }
    }
}
