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

        public int InsertDepartmentAndReturnsAnId(Department department)
        {
            int returnedId = dbEntity.InsertObjectAndReturnsId(department);

            return returnedId;
        }

        public void DeleteDepartmentByFieldName()
        {
            dbEntity.DeleteObject(new Department 
            { 
                Name = "Inventory"
            }, "name");
        }

        public void DeleteDepartmentByObject()
        {
            dbEntity.DeleteObject(new Department 
            { 
                Id = 3
            });
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            IEnumerable<Department> departments = dbEntity.GetAllObjects();

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentByDate(DateTime givenDate)
        {
            var departments = dbEntity.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                Value = givenDate.ToString("yyyy-MM-dd")
            });

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByStringCriteria()
        {
            var departments = dbEntity.GetObjectsByCriteria("id", "47");

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByMultipleCriteria(string name, DateTime givenDate)
        {
            var departments = dbEntity.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
            { 
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.Name),
                    Value = name
                },
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                    Value = givenDate.ToString("yyyy-MM-dd")
                }
            });

            return (departments.Count() > 0) ? departments : null;
        }
        #endregion
    }
}
