using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace RapidORM.Data.Common
{
    public interface IQueryBuilder
    {
        /// <summary>
        /// Retrieves list of data using SQL Data Adapter
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="queryParameter"></param>
        /// <param name="commandType"></param>
        DataTable GetDataUsingDataAdapter(string sql, DbParameter[] queryParameter = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Returns an object that can iterate over the entire Result Set
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="queryParameter"></param>
        /// <param name="commandType"></param>
        IDataReader ExecuteReader(string sql, DbParameter[] queryParameter = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Returns the value from the first column on the first row of your query
        /// </summary>
        /// <param name="sql"></param>        
        object ExecuteScalar(string sql);

        /// <summary>
        /// Returns only no. of rows affected by the Insert, Update or Delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="queryParameter"></param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(string sql, DbParameter[] queryParameter = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Builds the parameter used on most query builder transactions
        /// Instead of SqlParameter or MySqlParameter, use this.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <param name="dbtype"></param>
        DbParameter MakeParameter(string name, object obj, DbType dbtype);
    }
}
