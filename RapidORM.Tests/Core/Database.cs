using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;

namespace RapidORM.Tests.Core
{
    public class Database
    {
        public static void UseDb(DBType dbType)
        {
            switch (dbType)
            {
                case DBType.MySql: ConnectToDb(); break;
                default: ConnectToDb(); break;
            }
        }

        private static void ConnectToDb()
        {
            DBContext.ConnectionString = new DBConnection()
            {
                Server = "localhost",
                Database = "rapidorm",
                Username = "root",
                Password = ""
            };
        }
    }
}
