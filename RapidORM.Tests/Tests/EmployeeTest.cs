using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Data.SQLite;
using RapidORM.Tests.Core;
using RapidORM.Tests.Models.SQLite;

namespace RapidORM.Tests.Tests
{
    [TestClass]
    public class EmployeeTest
    {
        Employee employee;

        public EmployeeTest()
        {
            Database.UseDb(DBType.SQLite);
            employee = new Employee();
        }

        [TestMethod]
        public void CreateSQLiteEmployeeDatabaseTest()
        {
            employee.CreateDatabase("rapidorm");

            Assert.Inconclusive("SQLite DB Created");
        }

        [TestMethod]
        public void SaveEmployeeTest()
        {
            employee.Save(new Employee
            {
                Name = "Jane Hugh",
                Position = "Sales Administrator"
            });

            Assert.Inconclusive("New Employee Saved");
        }
    }
}
