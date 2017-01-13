using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Attributes;
using RapidORM.Interfaces;
using RapidORM.Client.SQL;
using RapidORM.Common;
using System.Data;
using System.Data.Common;

namespace RapidORM.Tests.Models.SQL
{
    [TableName("SystemUser")]
    public class User
    {
        [IsPrimaryKey(true)]
        public int ID { get; set; }

        [ColumnName("Name")]
        public string Name { get; set; }

        [ColumnName("Email")]
        public string Email { get; set; }

        [ColumnName("Password")]
        public string Password { get; set; }

        [ColumnName("DesignatedPosition")]
        public string DesignatedPosition { get; set; }

        private SqlEntity<User> dbEntity = null;

        public User()
        {
            dbEntity = new SqlEntity<User>();
        }

        public User(Dictionary<string, object> args)
        {
            ID = Convert.ToInt32(args["Id"].ToString());
            Name = args["Name"].ToString();
            Email = args["Email"].ToString();
            Password = args["Password"].ToString();
            DesignatedPosition = args["DesignatedPosition"].ToString();
        }

        #region Instance Methods
        public void SaveUser()
        {
            dbEntity.SaveChanges(new User
            {
                Name = "Cersei Lannister",
                Email = "cersei@gmail.com",
                Password = "cersei123",
                DesignatedPosition = "queen"
            });

            Console.WriteLine("Data Saved");
        }       
        #endregion
    }
}
