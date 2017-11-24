using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using RapidORM.Data;
using RapidORM.Data.Common;

namespace RapidORM.Data.SQL
{
    public class SqlTransaction<T> : Query<T>
    {
        protected void ExecuteNonQuery(string sql)
        {
            using (var connection = new SqlConnection(DBConnection.GetConnectionString(DatabaseType.SQL)))
            {
                try
                {
                    var command = new SqlCommand(sql, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected int ExecuteScalar(string sql)
        {
            using (var connection = new SqlConnection(DBConnection.GetConnectionString(DatabaseType.SQL)))
            {
                try
                {
                    var command = new SqlCommand(string.Format("{0} SELECT SCOPE_IDENTITY()", sql), connection);
                    connection.Open();
                    int result = (int)(decimal)command.ExecuteScalar();

                    return result;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected bool CheckIfAlreadyExists(T o)
        {
            string table = GetTableName();
            string uniqueField = GetPrimaryKey().Name;

            PropertyInfo piUnique = typeof(T).GetProperty(uniqueField);
            string query = "select " + uniqueField + " from " + table + " where ";
            query += uniqueField + "=" + FormatRawSqlQuery(piUnique.GetValue(o, null).ToString(), piUnique);

            string strReadBack = GetSingleValue(query, uniqueField);
            if (string.IsNullOrEmpty(strReadBack))
            {
                return false;
            }

            return true;
        }

        protected void DeleteObject(T o, PropertyInfo field)
        {
            string query = "delete from " + GetTableName() + " where ";
            query += GetColumnName(field) + " = " + FormatRawSqlQuery(field.GetValue(o, null).ToString(), field);

            ExecuteNonQuery(query);
        }

        protected string GetSingleValue(string sql, string field)
        {
            using (var connection = new SqlConnection(DBConnection.GetConnectionString(DatabaseType.SQL)))
            {
                try
                {
                    string returnVal = null;
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        returnVal = reader[field].ToString();
                    }

                    return returnVal;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected SqlDataReader GetSqlDataReader(string sql)
        {
            var connection = new SqlConnection(DBConnection.GetConnectionString(DatabaseType.SQL));
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        protected List<T> GetObjectsByCriteria(PropertyInfo field, string criteria)
        {
            string table = GetTableName();
            
            string query = "select * from " + table + " where ";
            query += GetColumnName(field) + "=" + FormatRawSqlQuery(criteria, field);

            return GetValues(query);
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
