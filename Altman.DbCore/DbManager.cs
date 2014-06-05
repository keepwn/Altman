using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Altman.ModelCore;

namespace Altman.DbCore
{
    /// <summary>
    /// 数据库管理类
    /// </summary>
    public static class DbManager
    {
        private const string DatabasePath = "data.db3";
        private static SqliteHelper _sqliteHelper = null;
        static DbManager()
        {
            //初始化数据库
            InitDb(DatabasePath);
        }
        /// <summary>
        /// 检查数据库文件
        /// </summary>
        /// <param name="dbPath">数据库路径</param>
        private static void InitDb(string dbPath)
        {
            //如果不存在数据库文件，则创建该数据库文件
            if (!File.Exists(dbPath))
            {
                //创建数据库
                SqliteHelper.CreateDb(dbPath);
            }
            //sqlite帮助类实例化
            _sqliteHelper = new SqliteHelper(DatabasePath);

            //判断数据库是否含有shell表
            DataTable dt = _sqliteHelper.GetSchema();
            bool isAvailableDb = false;
            foreach (DataRow row in dt.Rows)
            {
                if ((string)row["TABLE_NAME"] == "shell" && (string)row["TABLE_TYPE"] == "table")
                {
                    //修复之前旧版数据库缺少（status）的问题
                    DataTable old = GetDataTable();
                    if (!old.Columns.Contains("status"))
                    {
                        //添加status列
                        _sqliteHelper.ExecuteNonQuery("alter table shell add column status TEXT;", null);
                    }
                    isAvailableDb = true;
                    break;
                }
            }
            if (!isAvailableDb)
            {
                //创建shell表
                CreateTable_shell();
            }
        }
        /// <summary>
        /// 创建shell表
        /// </summary>
        private static void CreateTable_shell()
        {
            SqliteHelper sqliteHelper = _sqliteHelper;
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

            sqliteHelper.CreateTable("shell", definition);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">指定id号</param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return _sqliteHelper.Delete("shell", string.Format("id={0}", id));
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model">shell的参数</param>
        /// <returns></returns>
        public static bool Insert(ShellStruct model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                //{"id", null},//主键自增长字段，不需要设置
                {"target_id", model.TargetId},
                {"target_level", model.TargetLevel},
                {"status", model.Status},
                {"shell_url", model.ShellUrl},
                {"shell_pwd", model.ShellPwd},
                {"shell_type", model.ShellType},
                {"shell_extra_setting", model.ShellExtraSetting},
                {"server_coding", model.ServerCoding},
                {"web_coding", model.WebCoding},
                {"area", model.Area},
                {"remark", model.Remark},
                {"add_time", model.AddTime}
            };

            return _sqliteHelper.Insert("shell", dic);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id">指定id号</param>
        /// <param name="model">shell参数</param>
        /// <returns></returns>
        public static bool Update(int id, ShellStruct model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"id", id.ToString()},
                {"target_id", model.TargetId},
                {"target_level", model.TargetLevel},
                {"status", model.Status},
                {"shell_url", model.ShellUrl},
                {"shell_pwd", model.ShellPwd},
                {"shell_type", model.ShellType},
                {"shell_extra_setting", model.ShellExtraSetting},
                {"server_coding", model.ServerCoding},
                {"web_coding", model.WebCoding},
                {"area", model.Area},
                {"remark", model.Remark},
                {"add_time", model.AddTime}
            };

            return _sqliteHelper.Update("shell", dic, string.Format("id={0}", id));
        }
        /// <summary>
        /// 获取数据库表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from shell;";
            return _sqliteHelper.GetDataTable(sql);
        }

    }
}
