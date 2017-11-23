using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Data.Common;
using RapidORM.Data.SQLite;

namespace RapidORM.Tests.Models.SQLite
{
    [TableName("employee")]
    public class Employee
    {
        [IsPrimaryKey(true)]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("position")]
        public string Position { get; set; }

        private IDBEntity<Employee> dbContext = null;

        public Employee()
        {
            dbContext = new SQLiteEntity<Employee>();
        }

        public Employee(Dictionary<string, object> args)
        {
            Id = Convert.ToInt32(args["id"].ToString());
            Name = args["name"].ToString();
            Position = args["position"].ToString();
        }

        #region Class Methods
        public void CreateDatabase(string databaseName)
        {
            dbContext.CreateDatabase(databaseName);
        }

        public void Save(Employee employee)
        {
            dbContext.SaveChanges(employee);
        }
        #endregion
    }
}
