using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

namespace Altman.DbCore
{
    /// <summary>
    /// sqlite数据库帮助类
    /// </summary>
    internal class SqliteHelper
    {
        /// <summary>
        /// 数据库连接配置
        /// </summary>
        private string dbConnectionString;

        public SqliteHelper(string dbPath)
        {
            dbConnectionString = string.Format("Data Source={0}", dbPath);
        }
        public SqliteHelper(Dictionary<string, string> connectionOpts)
        {
            List<string> list = new List<string>();
            foreach (var row in connectionOpts)
            {
                list.Add(string.Format("{0}={1}", row.Key, row.Value));
            }
            string str = string.Join(";", list.ToArray());
            dbConnectionString = str;
        }

        /// <summary>
        /// 获取所有数据类型信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchema()
        {
            using (SqliteConnection connection = new SqliteConnection(dbConnectionString))
            {
                connection.Open();
                DataTable dt = connection.GetSchema("TABLES");
                return dt;
            }
        }
        /// <summary>
        /// 执行查询语句，获取包含查询结果的datatable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            using (SqliteConnection connection = new SqliteConnection(dbConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    SqliteDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                    //用下面的这种方法，会有一个异常
                    //SqliteDataAdapter adapter = new SqliteDataAdapter(command);                
                    //adapter.Fill(dt);             
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqliteDataReader实例
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public SqliteDataReader ExecuteReader(string sql)
        {
            using (SqliteConnection connection = new SqliteConnection(dbConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回查询结果的第一行第一列
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">查询语句所需要的参数</param>
        /// <returns></returns>
        public string ExecuteScalar(string sql)
        {
            using (SqliteConnection connection = new SqliteConnection(dbConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    object value = command.ExecuteScalar();
                    return value != null ? value.ToString() : "";
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">查询语句所需要的参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, SqliteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SqliteConnection connection = new SqliteConnection(dbConnectionString))
            {
                connection.Open();
                using (SqliteTransaction transaction = connection.BeginTransaction())
                {
                    using (SqliteCommand command = new SqliteCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }


        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">需要更新的键值对（列名，列值）</param>
        /// <param name="where">判断表达式</param>
        /// <returns></returns>
        public bool Update(string tableName, Dictionary<string, string> data, string where)
        {
            string vals = "";
            bool returnCode = true;
            if (data.Count >= 1)
            {
                List<string> list = new List<string>();
                foreach (var row in data)
                {
                    list.Add(string.Format(" {0} = '{1}'", row.Key, row.Value));
                }
                vals = string.Join(",", list.ToArray());
            }
            try
            {
                string sql = string.Format("update {0} set {1} where {2};", tableName, vals, where);
                ExecuteNonQuery(sql, null);
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">判断表达式</param>
        /// <returns></returns>
        public bool Delete(string tableName, string where)
        {
            bool returnCode = true;
            try
            {
                string sql = string.Format("delete from {0} where {1};", tableName, where);
                ExecuteNonQuery(sql, null);
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">需要插入的键值对（列名，列值）</param>
        /// <returns></returns>
        public bool Insert(string tableName, Dictionary<string, string> data)
        {
            bool returnCode = true;

            List<string> columnsList = new List<string>();
            List<string> valuesList = new List<string>();
            foreach (var val in data)
            {
                columnsList.Add(string.Format(" {0}", val.Key));
                if (val.Value == null)
                {
                    valuesList.Add(" NULL");
                }
                else
                {
                    valuesList.Add(String.Format(" '{0}'", val.Value));
                }
            }
            string columns = string.Join(",", columnsList.ToArray());
            string values = string.Join(",", valuesList.ToArray());

            try
            {
                string sql = string.Format("insert into {0}({1}) values({2});", tableName, columns, values);
                ExecuteNonQuery(sql, null);
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbName">数据名</param>
        /// <returns></returns>
        public static bool CreateDb(string dbName)
        {
            bool returnCode = true;
            try
            {
                SqliteConnection.CreateFile(dbName);
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">表的定义</param>
        /// <returns></returns>
        public bool CreateTable(string tableName, List<string> data)
        {
            bool returnCode = true;
            try
            {
                string values = string.Join(",", data.ToArray());
                string sql = string.Format("create table {0}({1});", tableName, values);
                ExecuteNonQuery(sql, null);
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        /// 清空表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public bool ClearTable(string tableName)
        {
            bool returnCode = true;
            try
            {
                string sql = string.Format("delete from {0};", tableName);
                ExecuteNonQuery(sql, null);
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        /// 清空数据库
        /// </summary>
        /// <returns></returns>
        public bool ClearDb()
        {
            bool returnCode = true;
            try
            {
                string sql = "select NAME from SQLITE_MASTER where type='table' order by NAME;";
                DataTable tables = GetDataTable(sql);
                foreach (DataRow table in tables.Rows)
                {
                    ClearTable(table["NAME"].ToString());
                }
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
    }
}
