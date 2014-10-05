using System;
using System.Collections.Generic;
using System.Data;
using Altman.Model;
using PluginFramework;

namespace Plugin_ShellManager
{
    /// <summary>
    /// ShellManagerService类,用于管理webshell
    /// </summary>
    public class ShellManagerService
    {
        private IHost _host;
        private const string Tablename = "shell";

        public class CompletedEventArgs : EventArgs
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


        public ShellManagerService(IHost host)
        {
            this._host = host;
            Init();
        }

        private void Init()
        {
            List<string> definition = new List<string>
            {
                "id INTEGER PRIMARY KEY",
                "target_id TEXT NOT NULL",
                "target_level TEXT NOT NULL",
                "status TEXT",
                "shell_url TEXT NOT NULL",
                "shell_pwd TEXT NOT NULL",
                "shell_type TEXT NOT NULL",
                "shell_extra_setting TEXT",
                "server_coding TEXT NOT NULL",
                "web_coding TEXT NOT NULL",
                "area TEXT",
                "remark TEXT",
                "add_time TEXT NOT NULL"
            };
            _host.Database.InitTable(Tablename, definition.ToArray());
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
                var where = new KeyValuePair<string, object>("id", id);
                _host.Database.Delete(Tablename, where);
            }
            catch (Exception ex)
            {
                error = ex;
                if (DeleteCompletedToDo != null)
                {
                    DeleteCompletedToDo(null, new CompletedEventArgs(error));
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
                var dic = new Dictionary<string, object>
                {
                    //{"id", null},//主键自增长字段，不需要设置
                    {"target_id", model.TargetId},
                    {"target_level", model.TargetLevel},
                    {"status", model.Status},
                    {"shell_url", model.ShellUrl},
                    {"shell_pwd", model.ShellPwd},
                    {"shell_type", model.ShellType},
                    {"shell_extra_setting", model.ShellExtraString},
                    {"server_coding", model.ServerCoding},
                    {"web_coding", model.WebCoding},
                    {"area", model.Area},
                    {"remark", model.Remark},
                    {"add_time", model.AddTime}
                };
                _host.Database.Insert(Tablename, dic);
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
                var dic = new Dictionary<string, object>
                {
                    {"id", id},
                    {"target_id", model.TargetId},
                    {"target_level", model.TargetLevel},
                    {"status", model.Status},
                    {"shell_url", model.ShellUrl},
                    {"shell_pwd", model.ShellPwd},
                    {"shell_type", model.ShellType},
                    {"shell_extra_setting", model.ShellExtraString},
                    {"server_coding", model.ServerCoding},
                    {"web_coding", model.WebCoding},
                    {"area", model.Area},
                    {"remark", model.Remark},
                    {"add_time", model.AddTime}
                };
                var where = new KeyValuePair<string, object>("id",id);
                _host.Database.Update(Tablename,dic,where);
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
                return _host.Database.GetDataTable(Tablename);
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
