using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Helpers;

namespace RapidORM.Data.MySQL
{
    public class MySqlTransaction<T> : Query<T>
    {
        protected void ExecuteNonQuery(string sql)
        {
            using (var connection = new MySqlConnection(DBConnection.GetConnectionString(DatabaseType.MySql)))
            {
                try
                {
                    var command = new MySqlCommand(sql, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void ExecuteNonQuery(ImageParameterQueryContainer imageParameterQueryContainer)
        {
            using (var connection = new MySqlConnection(DBConnection.GetConnectionString(DatabaseType.MySql)))
            {
                try
                {
                    var command = new MySqlCommand(imageParameterQueryContainer.SqlQuery, connection);
                    connection.Open();

                    foreach (var imageParameter in imageParameterQueryContainer.ImageParameterList)
                    {
                        CreateParameters(ref command, imageParameter);
                    }

                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void CreateParameters(ref MySqlCommand command, ImageParameter imageParameter)
        {
            command.Parameters.Add(imageParameter.Parameter, MySqlDbType.Blob).Value = imageParameter.Value;
        }

        protected object ExecuteScalar(string sql)
        {
            using (var connection = new MySqlConnection(DBConnection.GetConnectionString(DatabaseType.MySql)))
            {
                try
                {
                    var command = new MySqlCommand(string.Format("{0} SELECT LAST_INSERT_ID();", sql), connection);
                    connection.Open();
                    object result = command.ExecuteScalar();

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
            using (var connection = new MySqlConnection(DBConnection.GetConnectionString(DatabaseType.MySql)))
            {
                try
                {
                    string returnVal = null;
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

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

        protected MySqlDataReader GetMySqlDataReader(string sql)
        {
            var connection = new MySqlConnection(DBConnection.GetConnectionString(DatabaseType.MySql));
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        protected List<T> GetObjectsByCriteria(PropertyInfo field, string criteria)
        {
            string table = GetTableName();
            
            string query = "select * from " + table + " where ";
            query += GetColumnName(field) + "=" + FormatRawSqlQuery(criteria, field);

            return GetValues(query);
        }

        public List<T> GetValues(string query)
        {
            var values = new List<T>();
            object instance = null;

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var reader = GetMySqlDataReader(query);

            while (reader.Read())
            {
                instance = Activator.CreateInstance(typeof(T));

                foreach (var property in properties)
                {
                    if (property.PropertyType.ToString().Contains(DbType.String.ToString()))
                    {
                        property.SetValue(instance, Convert.ToString(reader[GetColumnName(property)]));
                    }
                    else
                    {
                        property.SetValue(instance, reader[GetColumnName(property)]);
                    }
                }                

                values.Add((T)instance);                
            }           
            
            reader.Close();
            return values;            
        }
    }
}
