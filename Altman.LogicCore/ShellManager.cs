using System;
using System.Data;
using Altman.DbCore;
using Altman.ModelCore;

namespace Altman.LogicCore
{
    /// <summary>
    /// ShellManager类,用于管理webshell
    /// </summary>
    public class ShellManager
    {
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
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Exception error = null;
            try
            {
                DbManager.Delete(id);
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
        public void Insert(ShellStruct model)
        {
            Exception error = null;
            try
            {
                DbManager.Insert(model);
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
        public void Update(int id, ShellStruct model)
        {
            Exception error = null;
            try
            {
                DbManager.Update(id,model);
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
                return DbManager.GetDataTable();
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
