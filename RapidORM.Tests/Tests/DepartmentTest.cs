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
                Name = "Human Resource",
                DateCreated = DateHelper.GetDateTimeForDB()
            });

            Assert.Inconclusive("New Department Saved");
        }        

        [TestMethod]
        public void InsertDepartmentAndReturnAnIdTest()
        {
            int savedId = department.InsertDepartmentAndReturnAnId(new Department 
            { 
                Name = "Production",
                DateCreated = DateHelper.GetDateTimeForDB()
            });

            Assert.AreEqual(1, savedId);
        }

        [TestMethod]
        public void DeleteDepartmentByFieldNameTest()
        {
            department.DeleteDepartmentByPropertyName("Inventory");

            Assert.Inconclusive("Department deleted");
        }

        [TestMethod]
        public void DeleteDepartmentByObjectTest()
        {
            department.DeleteDepartmentByObject(new Department
            {
                Id = 3
            });

            Assert.Inconclusive("Department deleted");
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
            IEnumerable<Department> departments = department.GetDepartmentsByDate(DateTime.Now);

            Assert.AreEqual(5, departments.Count());
        }

        [TestMethod]
        public void GetDepartmentsByStringCriteriaTest()
        {
            IEnumerable<Department> departments = department.GetDepartmentsByStringCriteria(4);

            Assert.AreEqual(5, departments.Count());
        }

        [TestMethod]
        public void GetDepartmentsByMultipleCriteriaTest()
        {
            IEnumerable<Department> departments = department.GetDepartmentsByMultipleCriteria("Marketing", DateTime.Now);

            Assert.AreEqual(5, departments.Count());
        }
    }
}
