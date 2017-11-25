using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Reflection;
using RapidORM.Data;
using RapidORM.Data.Common;

namespace RapidORM.Data.SQLite
{
    public class SQLiteTransaction<T> : Query<T>
    {
        protected void ExecuteNonQuery(string sql)
        {
            using (var connection = new SQLiteConnection(DBConnection.GetConnectionString(DatabaseType.SQLite)))
            {
                try
                {
                    var command = new SQLiteCommand(sql, connection);
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
            using (var connection = new SQLiteConnection(DBConnection.GetConnectionString(DatabaseType.SQLite)))
            {
                try
                {
                    var command = new SQLiteCommand(imageParameterQueryContainer.SqlQuery, connection);
                    connection.Open();

                    foreach (var imageParameter in imageParameterQueryContainer.ImageParameterList)
                    {
                        command.Parameters.Add(new SQLiteParameter(imageParameter.Parameter, imageParameter.Value));
                    }

                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected object ExecuteScalar(string sql)
        {
            using (var connection = new SQLiteConnection(DBConnection.GetConnectionString(DatabaseType.SQLite)))
            {
                try
                {
                    var command = new SQLiteCommand(string.Format("{0};select last_insert_rowid();", sql), connection);
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
            query += uniqueField + "=" + FormatRawSqlQuery(piUnique.GetValue(o, null).ToString(), piUnique, SpecialCharacter.No);

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
            query += GetColumnName(field) + " = " + FormatRawSqlQuery(field.GetValue(o, null).ToString(), field, SpecialCharacter.No);

            ExecuteNonQuery(query);
        }

        protected string GetSingleValue(string sql, string field)
        {
            using (var connection = new SQLiteConnection(DBConnection.GetConnectionString(DatabaseType.SQLite)))
            {
                try
                {
                    string returnVal = null;
                    SQLiteCommand command = new SQLiteCommand(sql, connection);
                    connection.Open();
                    SQLiteDataReader reader = command.ExecuteReader();

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

        protected SQLiteDataReader GetSQLiteDataReader(string sql)
        {
            var connection = new SQLiteConnection(DBConnection.GetConnectionString(DatabaseType.SQLite));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        protected List<T> GetObjectsByCriteria(PropertyInfo field, string criteria)
        {
            string table = GetTableName();

            string query = "select * from " + table + " where ";
            query += GetColumnName(field) + "=" + FormatRawSqlQuery(criteria, field, SpecialCharacter.No);
            
            return GetValues(query);
        }

        public List<T> GetValues(string query)
        {
            var values = new List<T>();
            object instance = null;

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var reader = GetSQLiteDataReader(query);

            while (reader.Read())
            {
                instance = Activator.CreateInstance(typeof(T));
                foreach (var property in properties)
                {
                    property.SetValue(instance, reader[GetColumnName(property)]);
                }

                values.Add((T)instance);
            }

            reader.Close();
            return values;
        }
    }
}
