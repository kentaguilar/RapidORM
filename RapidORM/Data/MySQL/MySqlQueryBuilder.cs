using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using RapidORM.Data;
using RapidORM.Data.Common;

namespace RapidORM.Data.MySQL
{
    public class MySqlQueryBuilder : IQueryBuilder
    {
        //Retrieves list of data using SQL Data Adapter
        public DataTable GetDataUsingDataAdapter(string sql, DbParameter[] queryParameter = null, CommandType commandType = CommandType.Text)
        {
            DataTable dataTable = null;
            using (MySqlConnection connection = new MySqlConnection(DBContext.GetMySqlConnection()))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.CommandType = commandType;

                if (queryParameter != null)
                {
                    foreach (var db in queryParameter)
                    {
                        command.Parameters.Add(db);
                    }
                }

                MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
                sqlAdapter.SelectCommand = command;

                dataTable = new DataTable();
                sqlAdapter.Fill(dataTable);

                connection.Close();
            }

            return dataTable;
        }

        //Returns an object that can iterate over the entire Result Set
        public IDataReader ExecuteReader(string sql, DbParameter[] queryParameter = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(DBContext.GetMySqlConnection());

                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.CommandType = commandType;

                if (queryParameter != null)
                {
                    foreach (var db in queryParameter)
                    {
                        command.Parameters.Add(db);
                    }
                }

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        //Returns the value from the first column on the first row of your query
        public object ExecuteScalar(string sql)
        {
            object result = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DBContext.GetMySqlConnection()))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    result = command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        //Returns only no. of rows affected by the Insert, Update or Delete
        public int ExecuteNonQuery(string sql, DbParameter[] queryParameter = null, CommandType commandType = CommandType.Text)
        {
            int result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DBContext.GetMySqlConnection()))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.CommandType = commandType;

                    if (queryParameter != null)
                    {
                        foreach (var db in queryParameter)
                        {
                            command.Parameters.Add(db);
                        }
                    }

                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        //Builds the parameter used on most query builder transactions
        public DbParameter MakeParameter(string name, object obj, DbType dbtype)
        {
            using (MySqlCommand com = new MySqlCommand())
            {
                DbParameter param = com.CreateParameter();
                param.ParameterName = name;
                param.Value = obj;
                param.DbType = dbtype;
                return param;
            }
        }
    }
}
