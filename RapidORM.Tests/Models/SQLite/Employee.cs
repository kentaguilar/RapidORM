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
        public void Save(Employee employee)
        {
            dbContext.SaveChanges(employee);
        }

        public int InsertEmployeeAndReturnAnId(Employee employee)
        {
            int returnedId = dbContext.InsertObjectAndReturnsId(employee);

            return returnedId;
        }

        public void DeleteEmployeeByPropertyName(string fieldValue)
        {
            dbContext.DeleteObject(new Employee
            {
                Name = fieldValue
            }, "Name");
        }

        public void DeleteEmployeeByObject(Employee employee)
        {
            dbContext.DeleteObject(employee);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> employees = dbContext.GetAllObjects();

            return (employees.Count() > 0) ? employees : null;
        }

        public IEnumerable<Employee> GetEmployeesByPosition(string position)
        {
            var employees = dbContext.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.Position),
                Value = position
            });

            return (employees.Count() > 0) ? employees : null;
        }

        public IEnumerable<Employee> GetEmployeesByStringCriteria(int id)
        {
            var employees = dbContext.GetObjectsByCriteria("Id", id.ToString());

            return (employees.Count() > 0) ? employees : null;
        }

        public IEnumerable<Employee> GetEmployeesByMultipleCriteria(string name, string position)
        {
            var employees = dbContext.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
            { 
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.Name),
                    Value = name
                },
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.Position),
                    Value = position
                }
            });

            return (employees.Count() > 0) ? employees : null;
        }
        #endregion
    }
}
