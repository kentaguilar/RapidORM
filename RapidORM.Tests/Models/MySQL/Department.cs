using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Data.Common;
using RapidORM.Data.MySQL;

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
        
        private IDBEntity<Department> dbContext = null;

        public Department()
        {
            dbContext = new MySqlEntity<Department>();
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
            dbContext.SaveChanges(department);
        }

        public int InsertDepartmentAndReturnAnId(Department department)
        {
            int returnedId = dbContext.InsertObjectAndReturnsId(department);

            return returnedId;
        }

        public void DeleteDepartmentByPropertyName(string fieldValue)
        {
            dbContext.DeleteObject(new Department 
            { 
                Name = fieldValue
            }, "Name");
        }

        public void DeleteDepartmentByObject(Department department)
        {
            dbContext.DeleteObject(department);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            IEnumerable<Department> departments = dbContext.GetAllObjects();

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByDate(DateTime givenDate)
        {
            var departments = dbContext.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                Value = givenDate.ToString("yyyy-MM-dd")
            });

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByStringCriteria(int id)
        {
            var departments = dbContext.GetObjectsByCriteria("Id", id.ToString());

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByMultipleCriteria(string name, DateTime givenDate)
        {
            var departments = dbContext.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
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
