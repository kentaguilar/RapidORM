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
        public void SQLite_SaveEmployeeTest()
        {
            employee.Save(new Employee
            {
                Name = "Anna Diaz",
                Position = "Line Supervisor"
            });

            Assert.Inconclusive("New Employee Saved");
        }

        [TestMethod]
        public void SQLite_InsertEmployeeWithoutPrimaryKeyTest()
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
        public void SQLite_InsertEmployeeAndReturnAnIdTest()
        {
            int savedId = employee.InsertEmployeeAndReturnAnId(new Employee
            {
                Name = "Hilda Owens",
                Position = "Coporate Secretary"
            });

            Assert.AreEqual(1, savedId);
        }

        [TestMethod]
        public void SQLite_DeleteEmployeeByFieldNameTest()
        {
            employee.DeleteEmployeeByPropertyName("Anna Diaz");

            Assert.Inconclusive("Employee deleted");
        }

        [TestMethod]
        public void SQLite_DeleteEmployeeByObjectTest()
        {
            employee.DeleteEmployeeByObject(new Employee
            {
                Id = 2
            });

            Assert.Inconclusive("Employee deleted");
        }

        [TestMethod]
        public void SQLite_GetAllEmployeesTest()
        {
            IEnumerable<Employee> employees = employee.GetAllEmployees();

            Assert.AreEqual(5, employees.Count());
        }

        [TestMethod]
        public void SQLite_GetEmployeesByPositionTest()
        {
            IEnumerable<Employee> employees = employee.GetEmployeesByPosition("Network Administrator");

            Assert.AreEqual(5, employees.Count());
        }

        [TestMethod]
        public void SQLite_GetEmployeesByStringCriteriaTest()
        {
            IEnumerable<Employee> employees = employee.GetEmployeesByStringCriteria(5);

            Assert.AreEqual(5, employees.Count());
        }

        [TestMethod]
        public void SQLite_GetEmployeesByMultipleCriteriaTest()
        {
            IEnumerable<Employee> employees = employee.GetEmployeesByMultipleCriteria("Anna Diaz", "Line Supervisor");

            Assert.AreEqual(5, employees.Count());
        }
    }
}
