using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Altman.DbCore
{
    public static class Db
    {
        private const string DatabasePath = "data.db3";
        private static SqliteHelper _sqliteHelper = null;
        static Db()
        {
            //初始化数据库
            CheckDb(DatabasePath);
        }

        /// <summary>
        /// 检查数据库文件
        /// </summary>
        private static void CheckDb(string dbPath)
        {
            //如果不存在数据库文件，则创建该数据库文件
            if (!File.Exists(dbPath))
            {
                //创建数据库
                SqliteHelper.CreateDb(dbPath);
            }
            //sqlite帮助类实例化
            _sqliteHelper = new SqliteHelper(dbPath);
        }
        /// <summary>
        /// 检查数据库表
        /// </summary>
        public static bool CheckTable(string tableName)
        {
            //判断数据库是否含有指定表
            DataTable dt = _sqliteHelper.GetSchema();
            bool isAvailableDb = false;
            foreach (DataRow row in dt.Rows)
            {
                if ((string)row["TABLE_NAME"] == tableName && (string)row["TABLE_TYPE"] == "table")
                {
                    isAvailableDb = true;
                    break;
                }
            }
            return isAvailableDb;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        public static bool CreateTable(string tableName, List<string> definition)
        {
            if (!CheckTable(tableName))
            {
                return _sqliteHelper.CreateTable(tableName, definition);
            }
            return true;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public static bool Delete(string tableName, string where)
        {
            return _sqliteHelper.Delete(tableName, where);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        public static bool Insert(string tableName, Dictionary<string, string> dic)
        {
            return _sqliteHelper.Insert(tableName, dic);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public static bool Updata(string tableName, Dictionary<string, string> dic, string where)
        {
            return _sqliteHelper.Update(tableName, dic, where);
        }
        /// <summary>
        /// 获取数据库表
        /// </summary>
        public static DataTable GetDataTable(string tableName)
        {
            string sql = string.Format("select * from {0};", tableName);
            return _sqliteHelper.GetDataTable(sql);
        }
    }
}
