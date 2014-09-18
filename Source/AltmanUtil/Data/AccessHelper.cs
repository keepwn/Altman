using System;
using System.Data;
using System.Data.OleDb;

namespace Altman.Data
{
    /// <summary>
    ///access数据库帮助类
    /// </summary>
    internal static class AccessHelper
    {
        private const string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb";
        private static OleDbConnection _conn = null;

        /// <summary>
        /// 打开数据库
        /// </summary>
        private static void OpenConnection()
        {
            _conn = new OleDbConnection(ConnString); //创建实例
            if (_conn.State == ConnectionState.Closed)
            {
                try
                {
                    _conn.Open();
                }
                catch (Exception e)
                { 
                    throw new Exception(e.Message); 
                }

            }

        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        private static void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
                _conn.Dispose();
            }
        }
        
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public static bool ExcuteSql(OleDbCommand cmd)
        {
            bool flag;
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception e)
            {
                flag = false;
                throw new Exception(e.Message);
            }
            finally
            { 
                CloseConnection(); 
            }
            return flag;
        }

        public static bool ExcuteSqlParameter(OleDbCommand cmd)
        {
            bool flag;
            try
            {
                OpenConnection();
                cmd.Connection = _conn;

                if (Convert.ToInt32(cmd.ExecuteNonQuery()) > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception e)
            {
                flag = false;
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;

        }       

        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象，使用时请注意关闭这个对象
        /// </summary>
        public static OleDbDataReader GetDataReader(OleDbCommand cmd)
        {
            OleDbDataReader dr = null;
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    CloseConnection();
                }
                catch { }
            }
            return dr;
        }
        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象,使用时请注意关闭
        /// </summary>
        public static void GetDataReader(OleDbCommand cmd, ref OleDbDataReader dr)
        {
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    if (dr != null && !dr.IsClosed)
                        dr.Close();
                }
                catch{ }
                finally
                {
                    CloseConnection();
                }
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        public static DataSet GetDataSet(OleDbCommand cmd)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {            
                OpenConnection();
                cmd.Connection = _conn;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return ds;
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        public static void GetDataSet(OleDbCommand cmd, ref DataSet ds)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 返回指定sql语句的datatable
        /// </summary>
        public static DataTable GetDataTable(OleDbCommand cmd, string sqlstr)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        /// <summary>
        /// 返回指定sql语句的datatable
        /// </summary>
        public static void GetDataTable(OleDbCommand cmd, ref DataTable dt)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataview
        /// </summary>
        public static DataView GetDataView(OleDbCommand cmd)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            DataView dv;
            try
            {
                OpenConnection();
                cmd.Connection = _conn;
                da.SelectCommand = cmd;
                da.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dv;
        }
    }
}
