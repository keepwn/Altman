using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Altman.Common.AltData;
using Altman.Model;
using PluginFramework;

namespace Plugin_DbManager
{
    public class DbManagerService
    {
        private IHost _host;
        private Shell _shellData;
        private string _dbType;
        public DbManagerService(IHost host, Shell data, string dbType)
        {
            this._host = host;
            this._shellData = data;
            this._dbType = dbType;
        }

        /// <summary>
        /// 将string转化为DataTable
        /// </summary>
        private DataTable ConvertStrToDataTable(string str)
        {
            List<string> list = str.TrimEnd(new char[] { '\r', '\n' }).Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList<string>();
            DataTable table = new DataTable();

            //如果返回的结果中没有匹配的话，返回空DataTable
            if (list.Count > 0)
            {
                //Columns
                string[] columns = list[0].TrimEnd(new char[] { '\t','|' }).Split(new string[] { "\t|\t" }, StringSplitOptions.None);
                foreach (string c in columns)
                {
                    table.Columns.Add(c);
                }
                //Rows
                list.RemoveAt(0); //第一行为column，故移除
                foreach (string l in list)
                {
                    string[] cols = l.TrimEnd(new char[] { '\t','|' }).Split(new string[] { "\t|\t" }, StringSplitOptions.None);
                    table.Rows.Add(cols);
                }
            }
            return table;
        }

        private void RunBackground(DoWorkEventHandler doWork, object argument, RunWorkerCompletedEventHandler runWorkerCompleted)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                backgroundWorker.DoWork += doWork;
                backgroundWorker.RunWorkerCompleted += runWorkerCompleted;
                backgroundWorker.RunWorkerAsync(argument);
            }
        }

        #region ConnectDb
        public event EventHandler<RunWorkerCompletedEventArgs> ConnectDbCompletedToDo;
        public void ConnectDb(string connStr)
        {
            RunBackground(connectDb_DoWork, connStr, connectDb_RunWorkerCompleted);
        }
        private void connectDb_DoWork(object sender, DoWorkEventArgs e)
        {
            string par = e.Argument as string;
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "DbManager/" + _dbType + "/ConnectDb", new string[] { par });

            e.Result = ResultMatch.MatchResultToBool(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));
        }
        private void connectDb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ConnectDbCompletedToDo != null)
            {
                ConnectDbCompletedToDo(null, e);
            }
        }
        #endregion

        #region GetDbName
        public event EventHandler<RunWorkerCompletedEventArgs> GetDbNameCompletedToDo;
        public void GetDbName(string connStr)
        {
            RunBackground(getDdName_DoWork, connStr, getDbName_RunWorkerCompleted);
        }
        private void getDdName_DoWork(object sender, DoWorkEventArgs e)
        {
            string par = e.Argument as string;
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "DbManager/" + _dbType + "/GetDbName", new string[] { par });
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            string[] dbs = tmp.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            e.Result = dbs;
        }
        private void getDbName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetDbNameCompletedToDo != null)
            {
                GetDbNameCompletedToDo(null, e);
            }
        }
        #endregion

        #region GetTableName
        public event EventHandler<RunWorkerCompletedEventArgs> GetDbTableNameCompletedToDo;

        public void GetTableName(string connStr,string dbName)
        {
            RunBackground(getDdTableName_DoWork, new string[] { connStr, dbName }, getBdTableName_RunWorkerCompleted);
        }
        private void getDdTableName_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "DbManager/" + _dbType + "/GetTableName", par);
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            string[] tables = tmp.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            e.Result = tables;
        }
        private void getBdTableName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetDbTableNameCompletedToDo != null)
            {
                GetDbTableNameCompletedToDo(null, e);
            }
        }
        #endregion

        #region GetColumnType
        public event EventHandler<RunWorkerCompletedEventArgs> GetColumnTypeCompletedToDo;

        public void GetColumnType(string connStr, string dbName,string columnName)
        {
            RunBackground(getColumnType_DoWork, new string[] { connStr, dbName, columnName }, getColumnType_RunWorkerCompleted);
        }
        private void getColumnType_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "DbManager/" + _dbType + "/GetColumnType", par);
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            string[] columns = tmp.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            e.Result = columns;
        }
        private void getColumnType_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetColumnTypeCompletedToDo != null)
            {
                GetColumnTypeCompletedToDo(null, e);
            }
        }
        #endregion

        #region ExecuteReader
        public event EventHandler<RunWorkerCompletedEventArgs> ExecuteReaderCompletedToDo;

        public void ExecuteReader(string connStr, string dbName, string sqlStr)
        {
            RunBackground(executeReader_DoWork, new string[] { connStr, dbName, sqlStr }, executeReader_RunWorkerCompleted);
        }
        private void executeReader_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "DbManager/" + _dbType + "/ExecuteReader", par);
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            e.Result = ConvertStrToDataTable(tmp);
        }
        private void executeReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ExecuteReaderCompletedToDo != null)
            {
                ExecuteReaderCompletedToDo(null, e);
            }
        }
        #endregion

        #region ExecuteNonQuery
        public event EventHandler<RunWorkerCompletedEventArgs> ExecuteNonQueryCompletedToDo;

        public void ExecuteNonQuery(string connStr, string dbName, string sqlStr)
        {
            RunBackground(executeNonQuery_DoWork, new string[] { connStr, dbName, sqlStr }, executeNonQuery_RunWorkerCompleted);
        }
        private void executeNonQuery_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _host.Core.SubmitCommand(_shellData, "DbManager/" + _dbType + "/ExecuteNonQuery", par);
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            e.Result = ConvertStrToDataTable(tmp);
        }
        private void executeNonQuery_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ExecuteNonQueryCompletedToDo != null)
            {
                ExecuteNonQueryCompletedToDo(null, e);
            }
        }
        #endregion
    }
}
