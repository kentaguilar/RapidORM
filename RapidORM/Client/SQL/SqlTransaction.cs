using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using RapidORM.Data;

namespace RapidORM.Client.SQL
{
    public class SqlTransaction<T> : Query<T>
    {
        protected void ExecuteNonQuery(string sql)
        {
            using (var conn = new SqlConnection(DBContext.GetSqlConnection()))
            {
                try
                {
                    var cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected int ExecuteScalar(string sql)
        {
            using (var conn = new SqlConnection(DBContext.GetSqlConnection()))
            {
                try
                {
                    var cmd = new SqlCommand(string.Format("{0} SELECT SCOPE_IDENTITY()", sql), conn);
                    conn.Open();
                    int result = (int)(decimal)cmd.ExecuteScalar();

                    return result;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected bool CheckIfAlreadyExists(T o)
        {
            string table = GetTableName();
            string uniqueField = GetPrimaryKey().Name;

            PropertyInfo piUnique = typeof(T).GetProperty(uniqueField);
            string strSQL = "select " + uniqueField + " from " + table + " where ";
            strSQL += uniqueField + "=" + FormatRawSqlQuery(piUnique.GetValue(o, null).ToString(), piUnique);

            string strReadBack = GetSingleValue(strSQL, uniqueField);
            if (string.IsNullOrEmpty(strReadBack))
            {
                return false;
            }

            return true;
        }

        protected void DeleteObject(T o, PropertyInfo field)
        {
            string strSql = "delete from " + GetTableName() + " where ";
            strSql += GetColumnName(field) + " = " + FormatRawSqlQuery(field.GetValue(o, null).ToString(), field);

            ExecuteNonQuery(strSql);
        }

        protected string GetSingleValue(string sql, string field)
        {
            using (var conn = new SqlConnection(DBContext.GetSqlConnection()))
            {
                try
                {
                    string returnVal = null;
                    SqlCommand database = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader reader = database.ExecuteReader();

                    if (reader.Read())
                    {
                        returnVal = reader[field].ToString();
                    }

                    return returnVal;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected SqlDataReader GetSqlDataReader(string sql)
        {
            var connection = new SqlConnection(DBContext.GetSqlConnection());
            connection.Open();
            SqlCommand database = new SqlCommand(sql, connection);
            SqlDataReader reader = database.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        protected List<T> GetObjectsByCriteria(PropertyInfo field, string criteria)
        {
            string table = GetTableName();
            
            string strSQL = "select * from " + table + " where ";
            strSQL += GetColumnName(field) + "=" + FormatRawSqlQuery(criteria, field);

            return GetValues(strSQL);
        }

        protected List<T> GetValues(string sql)
        {
            var values = new List<T>();
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            SqlDataReader reader = GetSqlDataReader(sql);
            while (reader.Read())
            {
                var args = new Dictionary<string, object>();
                string fieldName = string.Empty;
                for (int i = 0; i < fields.Length; i++)
                {
                    fieldName = GetColumnName(fields[i]);
                    args.Add(fieldName, reader[fieldName]);
                }

                values.Add((T)Activator.CreateInstance(typeof(T), args));
            }

            reader.Close();
            return values;
        }
    } 
}
