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

        public static string GetSqlConnection()
        {
            string connection = @"Data Source=" + ConnectionString.Server + ";Initial Catalog=" + ConnectionString.Database;
            connection += ";Persist Security Info=True;User ID=" + ConnectionString.Username + ";Password=" + ConnectionString.Password;

            return connection;
        }

        public static string GetMySqlConnection()
        {
            string connection = string.Format(@"Server={0};Database={1};Uid={2};Pwd={3};", ConnectionString.Server, ConnectionString.Database,
                ConnectionString.Username, ConnectionString.Password);

            return connection;
        }
    }
}
