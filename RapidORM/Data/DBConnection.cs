using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using RapidORM.Data.Common;

namespace RapidORM.Data
{
    public class DBConnection
    {
        public static DBConnectionSetting ConnectionString { get; set; }
        public static bool IsUTF8 { get; set; }

        public static void CreateDatabase(DatabaseType dbType, string databaseName = "")
        {
            switch (dbType)
            { 
                case DatabaseType.MySql:
                    break;
                case DatabaseType.SQLite:
                    SQLiteConnection.CreateFile(string.Format("{0}.sqlite", databaseName));
                    break;
                default:
                    break;
            }
        }

        public static string GetConnectionString(DatabaseType dbType)
        {
            string connectionString = string.Empty;
            switch (dbType)
            { 
                case DatabaseType.MySql:
                    connectionString = GetMySqlConnectionString();
                    break;
                case DatabaseType.SQLite:
                    connectionString = GetSQLiteConnectionString();
                    break;
                default:
                    connectionString = GetSqlConnectionString();
                    break;
            }

            return connectionString;
        }

        private static string GetSqlConnectionString()
        {
            string connection = string.Empty;
            if (ConnectionString.IsWindowsAuthentication)
            {
                connection = "Data Source=" + ConnectionString.Server + ";Initial Catalog=" + ConnectionString.Database + ";Integrated Security=True;";
            }
            else
            {
                connection = @"Data Source=" + ConnectionString.Server + ";Initial Catalog=" + ConnectionString.Database;
                connection += ";Persist Security Info=True;User ID=" + ConnectionString.Username + ";Password=" + ConnectionString.Password;
            }

            return connection;
        }

        private static string GetMySqlConnectionString()
        {

            string connection = string.Format(@"Server={0};Database={1};Uid={2};Pwd={3};{4}", ConnectionString.Server, ConnectionString.Database,
                ConnectionString.Username, ConnectionString.Password, IsUTF8 ? "charset=utf8;" : "");

            return connection;
        }

        private static string GetSQLiteConnectionString()
        {
            string connection = string.Format(@"Data Source={0};Version=3;", ConnectionString.Database);

            return connection;
        }
    }
}
