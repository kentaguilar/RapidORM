using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data
{
    public class DBConnectionSetting
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsWindowsAuthentication { get; set; }

        public DBConnectionSetting() { }

        public DBConnectionSetting(string server, string database, string username, string password, bool isWindowsAuthentication)
        {
            this.Server = server;
            this.Database = database;
            this.Username = username;
            this.Password = password;
            this.IsWindowsAuthentication = isWindowsAuthentication;
        }

    }
}
