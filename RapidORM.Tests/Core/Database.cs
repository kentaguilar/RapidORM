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
                case DBType.MySql: ConnectNow(); break;
                default: ConnectNow(); break;
            }
        }

        private static void ConnectNow()
        {
            DBContext.ConnectionString = new DBConnection()
            {
                Server = "",
                Database = "",
                Username = "",
                Password = ""
            };
        }
    }
}
