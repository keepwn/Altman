using System.Collections.Generic;
using System.Data;

namespace Altman.Plugin.Interface
{
    public interface IHostDb
    {
		/// <summary>
		/// 检查指定表是否存在
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <returns></returns>
        bool CheckTable(string tableName);

		/// <summary>
		/// 初始化指定表
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="definition">
		/// 创建表需要的定义参数，例如：
		/// <code><![CDATA[
		/// List<string> de = new List<string>
        /// {
        ///    "id INTEGER PRIMARY KEY",
        ///    "firstname TEXT NOT NULL",
        ///    "lastname TEXT NOT NULL",
		/// };
		/// definition = de.ToArray();
		/// ]]></code>
		/// </param>
		/// <returns>如果表已存在，返回true；如果不存在则创建表，如果创建成功则返回true</returns>
        bool InitTable(string tableName, string[] definition);

		/// <summary>
		/// 获取数据库表
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <returns>返回数据库表</returns>
        DataTable GetDataTable(string tableName);

		/// <summary>
		/// 插入数据
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="data">
		/// 待插入数据，例如：
		/// <code><![CDATA[
		/// var data = new Dictionary<string, object>
        /// {
        ///     //{"id", null},//主键自增长字段，不需要设置
		///     {"firstname", "kee"},
		///     {"lastname", "pwn:)"}
		/// };
		/// ]]></code>
		/// </param>
		/// <returns>如果插入成功，返回true</returns>
        bool Insert(string tableName, Dictionary<string, object> data);

		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="data">
		/// 待更新数据，例如：
		/// <code><![CDATA[
		/// var data = new Dictionary<string, object>
		/// {
		///     //{"id", null},//主键自增长字段，不需要设置
		///     {"firstname", "new"},
		///     {"lastname", "pwn:)"}
		/// };
		/// ]]></code>
		/// </param>
		/// <param name="where">
		/// 更新条件
		/// <code><![CDATA[
		/// var where = new KeyValuePair<string, object>("id",10);
		/// ]]></code>
		/// </param>
		/// <returns>如果更新成功，返回true</returns>
        bool Update(string tableName, Dictionary<string, object> data, KeyValuePair<string, object> where);
        
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="where">
		/// 删除条件
		/// <code><![CDATA[
		/// var where = new KeyValuePair<string, object>("id",10);
		/// ]]></code>
		/// </param>
		/// <returns>如果删除成功，返回true</returns>
		bool Delete(string tableName, KeyValuePair<string, object> where);
    }
}
