using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Data.Common;
using RapidORM.Data.MySQL;
using System.Reflection;

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
        public DateTime DateCreated { get; set; }

        private MySqlEntity<Department> entity = new MySqlEntity<Department>();  

        #region Class Methods
        public void Save(Department department)
        {
            entity.SaveChanges(department);
        }

        public int InsertDepartmentAndReturnAnId(Department department)
        {
            int returnedId = entity.InsertObjectAndReturnsId(department);

            return returnedId;
        }

        public void DeleteDepartmentByPropertyName(string fieldValue)
        {
            entity.DeleteObject(new Department 
            { 
                Name = fieldValue
            }, "Name");
        }

        public void DeleteDepartmentByObject(Department department)
        {
            entity.DeleteObject(department);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            IEnumerable<Department> departments = entity.GetAllObjects();

            return (departments.Count() > 0) ? departments : null;            
        }

        public IEnumerable<Department> GetDepartmentsByDate(DateTime givenDate)
        {
            var departments = entity.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByStringCriteria(int id)
        {
            var departments = entity.GetObjectsByCriteria("Id", id.ToString());

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByMultipleCriteria(string name, DateTime givenDate)
        {
            var departments = entity.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
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
