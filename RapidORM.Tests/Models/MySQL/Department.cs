using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Attributes;
using RapidORM.Interfaces;
using RapidORM.Client.MySQL;
using RapidORM.Common;

namespace RapidORM.Tests.Models.MySQL
{
    [TableName("department")]
    public class Department
    {
        [IsPrimaryKey(true)]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("date_created")]
        public string DateCreated { get; set; }
        
        private IDBEntity<Department> dbEntity = null;

        public Department()
        {
            dbEntity = new MySqlEntity<Department>();
        }
        
        public Department(Dictionary<string, object> args)
        {
            Id = Convert.ToInt32(args["id"].ToString());
            Name = args["name"].ToString();
            DateCreated = args["date_created"].ToString();
        }

        #region Class Methods
        public void Save(Department department)
        {
            dbEntity.SaveChanges(department);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            IEnumerable<Department> departments = dbEntity.GetAllObjects();

            return departments;
        }

        public IEnumerable<Department> GetDepartmentByDate()
        {
            var departments = dbEntity.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                Value = DateTime.Now.ToString("yyyy-MM-dd")
            });

            return departments;
        }
        #endregion
    }
}
