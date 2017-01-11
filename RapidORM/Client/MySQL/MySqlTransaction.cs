﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using RapidORM.Data;
using RapidORM.Common;

namespace RapidORM.Client.MySQL
{
    public class MySqlTransaction<T> : Query<T>
    {
        protected void ExecuteNonQuery(string sql)
        {
            using (var conn = new MySqlConnection(DBContext.GetMySqlConnection()))
            {
                try
                {
                    var command = new MySqlCommand(sql, conn);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void ExecuteNonQuery(ImageParameterQueryContainer imageParameterQueryContainer)
        {
            using (var conn = new MySqlConnection(DBContext.GetMySqlConnection()))
            {
                try
                {
                    var command = new MySqlCommand(imageParameterQueryContainer.SqlQuery, conn);
                    conn.Open();

                    foreach (var imageParameter in imageParameterQueryContainer.ImageParameterList)
                    {
                        CreateParameters(ref command, imageParameter);
                    }

                    command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void CreateParameters(ref MySqlCommand command, ImageParameter imageParameter)
        {
            command.Parameters.Add(imageParameter.Parameter, MySqlDbType.Blob).Value = imageParameter.Value;
        }

        protected object ExecuteScalar(string sql)
        {
            using (var conn = new MySqlConnection(DBContext.GetMySqlConnection()))
            {
                try
                {
                    var cmd = new MySqlCommand(string.Format("{0} SELECT LAST_INSERT_ID();", sql), conn);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

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
            using (var conn = new MySqlConnection(DBContext.GetMySqlConnection()))
            {
                try
                {
                    string returnVal = null;
                    MySqlCommand database = new MySqlCommand(sql, conn);
                    conn.Open();
                    MySqlDataReader reader = database.ExecuteReader();

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

        protected MySqlDataReader GetMySqlDataReader(string sql)
        {
            var connection = new MySqlConnection(DBContext.GetSqlConnection());
            connection.Open();
            MySqlCommand database = new MySqlCommand(sql, connection);
            MySqlDataReader reader = database.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        protected List<T> GetObjectsByCriteria(PropertyInfo field, string criteria)
        {
            string table = GetTableName();
            
            string strSQL = "select * from " + table + " where ";
            strSQL += GetColumnName(field) + "=" + FormatRawSqlQuery(criteria, field);

            return GetValues(strSQL);
        }

        public List<T> GetValues(string sql)
        {
            var values = new List<T>();
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var reader = GetMySqlDataReader(sql);
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
