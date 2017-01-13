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
        public static void SqlDb()
        {
            DBContext.ConnectionString = new DBConnection()
            {
                Server = "",
                Database = "",
                Username = "",
                Password = ""
            };
        }

        public static void MySqlDb()
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
