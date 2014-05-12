using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Altman.Common.AltData;
using Altman.ModelCore;
using PluginFramework;

namespace Plugin_DbManager
{
    public class DbManagerService
    {
        private HostService _hostService;
        private ShellStruct _shellData;
        public DbManagerService(HostService hostService, ShellStruct data)
        {
            this._hostService = hostService;
            this._shellData = data;
        }

        /// <summary>
        /// 将string转化为DataTable
        /// </summary>
        private DataTable ConvertStrToDataTable(string str)
        {
            List<string> list = str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            DataTable table = new DataTable();
            //Columns
            string[] columns = list[0].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string c in columns)
            {
                table.Columns.Add(c);
            }
            //Rows
            list.RemoveAt(0);//第一行为column，故移除
            foreach (string l in list)
            {
                string[] cols = l.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string c in cols)
                {
                    table.Rows.Add(c);
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


        #region 获取数据库名
        public event EventHandler<RunWorkerCompletedEventArgs> GetDbNameCompletedToDo;
        public void GetDbName(string connStr)
        {
            RunBackground(getDdName_DoWork, connStr, getDbName_RunWorkerCompleted);
        }
        private void getDdName_DoWork(object sender, DoWorkEventArgs e)
        {
            string par = e.Argument as string;
            byte[] resultBytes = _hostService.SubmitCommand(_shellData, "GetDbName_OLEDB", new string[] { par });
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            string[] dbs = tmp.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
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

        #region 获取数据库表名
        public event EventHandler<RunWorkerCompletedEventArgs> GetDbTableNameCompletedToDo;

        public void GetTableName(string connStr,string dbName)
        {
            RunBackground(getDdTableName_DoWork, new string[] { connStr, dbName }, getBdTableName_RunWorkerCompleted);
        }
        private void getDdTableName_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _hostService.SubmitCommand(_shellData, "GetTableName_OLEDB", par);
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            string[] dbs = tmp.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            e.Result = dbs;
        }
        private void getBdTableName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetDbTableNameCompletedToDo != null)
            {
                GetDbTableNameCompletedToDo(null, e);
            }
        }
        #endregion

        #region 获取字段类型
        public event EventHandler<RunWorkerCompletedEventArgs> GetColumnTypeCompletedToDo;

        public void GetColumnType(string connStr, string dbName,string columnName)
        {
            RunBackground(getColumnType_DoWork, new string[] { connStr, dbName, columnName }, getColumnType_RunWorkerCompleted);
        }
        private void getColumnType_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] par = e.Argument as string[];
            byte[] resultBytes = _hostService.SubmitCommand(_shellData, "GetColumnType_OLEDB", par);
            string tmp = ResultMatch.GetResultFromInterval(resultBytes, Encoding.GetEncoding(_shellData.WebCoding));

            string[] columns = tmp.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] res = new string[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                //将a\tb变形为a（b）
                res[i] = columns[i].Replace("\t", "(")+")";
            }
            e.Result = res;
        }
        private void getColumnType_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GetColumnTypeCompletedToDo != null)
            {
                GetColumnTypeCompletedToDo(null, e);
            }
        }
        #endregion
    }
}
