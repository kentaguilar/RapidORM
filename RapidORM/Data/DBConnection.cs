using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data
{
    public class DBConnection
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DBConnection() { }

        public DBConnection(string server, string database, string username, string password)
        {
            this.Server = server;
            this.Database = database;
            this.Username = username;
            this.Password = password;
        }

    }
}
