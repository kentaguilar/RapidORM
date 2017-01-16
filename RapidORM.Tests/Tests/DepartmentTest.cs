using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using RapidORM.Interfaces;
using RapidORM.Client.MySQL;
using RapidORM.Tests.Models.MySQL;
using System.Collections.Generic;

namespace RapidORM.Tests.Tests
{
    [TestClass]
    public class DepartmentTest
    {
        Department department;

        public DepartmentTest()
        {
            Database.UseMySqlDb();
            department = new Department();
        }

        [TestMethod]
        public void SaveDepartmentTest()
        {            
            department.Save(new Department 
            { 
                Name = "Inventory",
                DateCreated = DateHelper.GetDateTimeForDB()
            });

            Assert.Inconclusive("New Department Saved");
        }

        [TestMethod]
        public void GetAllDepartmentsTest()
        {
            IEnumerable<Department> departments = department.GetAllDepartments();

            Assert.AreEqual(5, departments.Count());
        }

        [TestMethod]
        public void GetDepartmentByDateTest()
        {
            IEnumerable<Department> departments = department.GetDepartmentByDate(DateTime.Now);

            Assert.AreEqual(5, departments.Count());
        }
    }
}
