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
        public static void UseSqlDb()
        {
            DBContext.ConnectionString = new DBConnection()
            {
                Server = "",
                Database = "",
                Username = "",
                Password = ""
            };
        }

        public static void UseMySqlDb()
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
