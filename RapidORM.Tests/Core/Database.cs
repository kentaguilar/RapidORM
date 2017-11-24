using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Data.Common;

namespace RapidORM.Tests.Core
{
    public class Database
    {
        public static void UseDb(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.MySql: ConnectToDb(); break;
                case DatabaseType.SQLite: ConnectToSQLite(); break;
                default: ConnectToDb(); break;
            }
        }

        private static void ConnectToDb()
        {
            DBConnection.ConnectionString = new DBConnectionSetting()
            {
                Server = "localhost",
                Database = "rapidorm",
                Username = "root",
                Password = ""
            };
        }

        private static void ConnectToSQLite()
        {
            DBConnection.ConnectionString = new DBConnectionSetting() 
            { 
                Database = "rapidorm.sqlite"
            };
        }
    }
}
