using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data
{
    public class DBContext
    {
        public static DBConnection ConnectionString { get; set; }
        public static bool IsUTF8 { get; set; }

        public static string GetSqlConnection()
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

        public static string GetMySqlConnection()
        {

            string connection = string.Format(@"Server={0};Database={1};Uid={2};Pwd={3};{4}", ConnectionString.Server, ConnectionString.Database,
                ConnectionString.Username, ConnectionString.Password, IsUTF8 ? "charset=utf8;" : "");

            return connection;
        }

        public static string GetSQLiteConnection()
        {
            string connection = string.Format(@"Data Source={0};Version=3;", ConnectionString.Database);

            return connection;
        }
    }
}
