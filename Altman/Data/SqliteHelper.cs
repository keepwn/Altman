using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Mono.Data.Sqlite;

namespace Altman.Data
{
    /// <summary>
    /// sqlite数据库帮助类
    /// </summary>
    internal class SqliteHelper
    {
        /// <summary>
        /// 数据库连接配置
        /// </summary>
        private static string _dbConStr = "";
        public static string DbConStr
        {
            get { return _dbConStr; }
            set { _dbConStr = value; }
        }

        private static SqliteParameter CreateParameter(string paramName, DbType paramType, object paramValue)
        {
            var param = new SqliteParameter();
            param.DbType = paramType;
            param.ParameterName = paramName;
            param.Value = paramValue;
            return param;
        }

        private static SqliteCommand CreateCommand(SqliteConnection conn, string sqlText, object[] sqlParams)
        {
            var command = new SqliteCommand(sqlText, conn);
            command.CommandType = CommandType.Text;
            if (sqlParams != null && sqlParams.Length > 0)
            {
                var sqliteParams = ConvertToSqliteParameters(sqlText, sqlParams);
                command.Parameters.AddRange(sqliteParams);
            }
            return command;
        }

        private static SqliteParameter[] ConvertToSqliteParameters(string sqlText, object[] sqlParams)
        {
            var paramList = new List<SqliteParameter>();
            string parmString = sqlText.Substring(sqlText.IndexOf("@", System.StringComparison.Ordinal));
            parmString = parmString.Replace(",", " ,");
            //匹配sql语句中定义的参数
            string pattern = @"(@)\S*(.*?)\b";
            Regex ex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mc = ex.Matches(parmString);
            string[] paramNames = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                paramNames[i] = m.Value;
                i++;
            }

            // now let's type the parameters  
            int j = 0;
            foreach (object o in sqlParams)
            {
                string type = (o == null) ? typeof(Nullable).ToString() : o.GetType().ToString();
                SqliteParameter parm = new SqliteParameter();
                switch (type)
                {                    
                    case ("DBNull"):
                    case ("Char"):
                    case ("SByte"):
                    case ("UInt16"):
                    case ("UInt32"):
                    case ("UInt64"):
                        throw new SystemException("Invalid data type");
                    case ("System.Nullable"):
                        parm.DbType = DbType.String;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string)sqlParams[j];
                        paramList.Add(parm);
                        break;
                    case ("System.String"):
                        parm.DbType = DbType.String;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string)sqlParams[j];
                        paramList.Add(parm);
                        break;
                    case ("System.Byte[]"):
                        parm.DbType = DbType.Binary;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (byte[])sqlParams[j];
                        paramList.Add(parm);
                        break;
                    case ("System.Int32"):
                        parm.DbType = DbType.Int32;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (int)sqlParams[j];
                        paramList.Add(parm);
                        break;
                    case ("System.Boolean"):
                        parm.DbType = DbType.Boolean;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (bool)sqlParams[j];
                        paramList.Add(parm);
                        break;
                    case ("System.DateTime"):
                        parm.DbType = DbType.DateTime;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDateTime(sqlParams[j]);
                        paramList.Add(parm);
                        break;
                    case ("System.Double"):
                        parm.DbType = DbType.Double;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDouble(sqlParams[j]);
                        paramList.Add(parm);
                        break;
                    case ("System.Decimal"):
                        parm.DbType = DbType.Decimal;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDecimal(sqlParams[j]);
                        break;
                    case ("System.Guid"):
                        parm.DbType = DbType.Guid;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (System.Guid)(sqlParams[j]);
                        break;
                    case ("System.Object"):
                        parm.DbType = DbType.Object;
                        parm.ParameterName = paramNames[j];
                        parm.Value = sqlParams[j];
                        paramList.Add(parm);
                        break;
                    default:
                        throw new SystemException("Value is of unknown data type");
                } // end switch
                j++;
            }
            return paramList.ToArray();
        }

        /// <summary>
        /// 获取所有数据类型信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSchema()
        {
            using (var connection = new SqliteConnection(_dbConStr))
            {
                connection.Open();
                DataTable dt = connection.GetSchema("TABLES");
                return dt;
            }
        }

        /// <summary>
        /// 执行查询语句，返回IDataReader实例
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="sqlParams">查询语句所需要的参数</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string sql, object[] sqlParams)
        {
            using (var connection = new SqliteConnection(_dbConStr))
            {
                connection.Open();

                SqliteCommand command = CreateCommand(connection, sql, sqlParams);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        /// <summary>
        /// 执行查询语句，获取包含查询结果的datatable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="sqlParams">查询语句所需要的参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, object[] sqlParams)
        {
            using (var connection = new SqliteConnection(_dbConStr))
            {
                connection.Open();

                SqliteCommand command = CreateCommand(connection, sql, sqlParams);
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

        /// <summary>
        /// 执行查询语句，返回查询结果的第一行第一列
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="sqlParams">查询语句所需要的参数</param>
        /// <returns></returns>
        public static string ExecuteScalar(string sql, object[] sqlParams)
        {
            using (var connection = new SqliteConnection(_dbConStr))
            {
                connection.Open();

                SqliteCommand command = CreateCommand(connection, sql, sqlParams);
                object value = command.ExecuteScalar();
                return value != null ? value.ToString() : "";
            }
        }

        /// <summary>
        /// 执行查询语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="sqlParams">查询语句所需要的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, object[] sqlParams)
        {
            int affectedRows = 0;
            using (var connection = new SqliteConnection(_dbConStr))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    SqliteCommand command = CreateCommand(connection, sql, sqlParams);
                    affectedRows = command.ExecuteNonQuery();
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
        public static bool Update(string tableName, Dictionary<string, object> data, KeyValuePair<string, object> where)
        {
            bool returnCode = true;

            var columnsList = data.Keys;
            var valuesList = data.Values.ToList();
            string columnsStr = string.Join(", ", (from column in columnsList select column+"=@"+column));

            string whereStr = string.Format(" {0}=@{1} ",where.Key,where.Key);
            valuesList.Add(where.Value);
            try
            {
                string sql = string.Format("update {0} set {1} where {2};", tableName, columnsStr, whereStr);
                ExecuteNonQuery(sql, valuesList.ToArray());
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
        public static bool Delete(string tableName, KeyValuePair<string, object> where)
        {
            bool returnCode = true;

            string whereStr = string.Format(" {0}=@{1} ", where.Key, where.Key);
            try
            {
                string sql = string.Format("delete from {0} where {1};", tableName, whereStr);
                ExecuteNonQuery(sql, new object[]{where.Value});
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
        public static bool Insert(string tableName, Dictionary<string, object> data)
        {
            bool returnCode = true;

            var columnsList = data.Keys;
            var valuesList = data.Values;
            string columnsStr = string.Join(", ", columnsList);
            string paramsStr = string.Join(", ", (from column in columnsList select "@"+column));
            try
            {
                string sql = string.Format("insert into {0}({1}) values({2});", tableName, columnsStr,paramsStr);
                ExecuteNonQuery(sql,valuesList.ToArray());
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
        /// <param name="definition">表的定义</param>
        /// <returns></returns>
        public static bool CreateTable(string tableName, string[] definition)
        {
            bool returnCode = true;
            try
            {
                string values = string.Join(", ", definition);
                string sql = string.Format("create table {0}({1});", tableName, values);
                ExecuteNonQuery(sql,null);
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
        public static bool ClearTable(string tableName)
        {
            bool returnCode = true;
            try
            {
                string sql = string.Format("delete from {0};", tableName);
                ExecuteNonQuery(sql,null);
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
        public static bool ClearDb()
        {
            bool returnCode = true;
            try
            {
                string sql = "select NAME from SQLITE_MASTER where type='table' order by NAME;";
                DataTable tables = ExecuteDataTable(sql,null);
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
