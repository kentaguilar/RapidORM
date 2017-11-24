using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Data.SQLite;
using RapidORM.Tests.Core;
using RapidORM.Tests.Models.SQLite;
using System.Collections.Generic;
using System.Linq;

namespace RapidORM.Tests.Tests
{
    [TestClass]
    public class EmployeeTest
    {
        Employee employee;

        public EmployeeTest()
        {
            Database.UseDb(DatabaseType.SQLite);
            employee = new Employee();
        }

        [TestMethod]
        public void SaveEmployeeTest()
        {
            employee.Save(new Employee
            {
                Name = "Anna Diaz",
                Position = "Line Supervisor"
            });

            Assert.Inconclusive("New Employee Saved");
        }

        [TestMethod]
        public void InsertEmployeeWithoutPrimaryKeyTest()
        {
            employee.InsertWithoutPrimaryKey(new Employee
            {
                Id = 5,
                Name = "Kyle Pearson",
                Position = "Network Administrator"
            });

            Assert.Inconclusive("New Employee Saved");
        }

        [TestMethod]
        public void InsertEmployeeAndReturnAnIdTest()
        {
            int savedId = employee.InsertEmployeeAndReturnAnId(new Employee
            {
                Name = "Hilda Owens",
                Position = "Coporate Secretary"
            });

            Assert.AreEqual(1, savedId);
        }

        [TestMethod]
        public void DeletEmployeeByFieldNameTest()
        {
            employee.DeleteEmployeeByPropertyName("Hilda Owens");

            Assert.Inconclusive("Employee deleted");
        }

        [TestMethod]
        public void DeleteEmployeeByObjectTest()
        {
            employee.DeleteEmployeeByObject(new Employee
            {
                Id = 2
            });

            Assert.Inconclusive("Employee deleted");
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            IEnumerable<Employee> employees = employee.GetAllEmployees();

            Assert.AreEqual(5, employees.Count());
        }

        [TestMethod]
        public void GetEmployeesByPositionTest()
        {
            IEnumerable<Employee> employees = employee.GetEmployeesByPosition("Supervisor");

            Assert.AreEqual(5, employees.Count());
        }

        [TestMethod]
        public void GetEmployeesByStringCriteriaTest()
        {
            IEnumerable<Employee> employees = employee.GetEmployeesByStringCriteria(4);

            Assert.AreEqual(5, employees.Count());
        }

        [TestMethod]
        public void GetEmployeesByMultipleCriteriaTest()
        {
            IEnumerable<Employee> employees = employee.GetEmployeesByMultipleCriteria("Pogi Points", "HR Assistant");

            Assert.AreEqual(5, employees.Count());
        }
    }
}
