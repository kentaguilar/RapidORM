using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using RapidORM.Data.Common;
using RapidORM.Data.MySQL;
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
            Database.UseDb(DatabaseType.MySql);
            department = new Department();
        }

        [TestMethod]
        public void MySql_SaveDepartmentTest()
        {            
            department.Save(new Department 
            { 
                Name = "Accounting",
                DateCreated = DateTime.Now.Date
            });

            Assert.Inconclusive("New Department Saved");
        }        

        [TestMethod]
        public void MySql_InsertDepartmentAndReturnAnIdTest()
        {
            int savedId = department.InsertDepartmentAndReturnAnId(new Department 
            { 
                Name = "Production",
                DateCreated = DateTime.Now.Date
            });

            Assert.AreEqual(1, savedId);
        }

        [TestMethod]
        public void MySql_DeleteDepartmentByFieldNameTest()
        {
            department.DeleteDepartmentByPropertyName("Production");

            Assert.Inconclusive("Department deleted");
        }

        [TestMethod]
        public void MySql_DeleteDepartmentByObjectTest()
        {
            department.DeleteDepartmentByObject(new Department
            {
                Id = 27
            });

            Assert.Inconclusive("Department deleted");
        }

        [TestMethod]
        public void MySql_GetAllDepartmentsTest()
        {
            IEnumerable<Department> departments = department.GetAllDepartments();

            Assert.AreEqual(5, departments.Count());
        }

        [TestMethod]
        public void MySql_GetDepartmentByDateTest()
        {
            IEnumerable<Department> departments = department.GetDepartmentsByDate(DateTime.Now.Date);

            Assert.IsNull(departments);            
        }

        [TestMethod]
        public void MySql_GetDepartmentsByStringCriteriaTest()
        {
            IEnumerable<Department> departments = department.GetDepartmentsByStringCriteria(19);

            Assert.AreEqual(5, departments.Count());
        }

        [TestMethod]
        public void MySql_GetDepartmentsByMultipleCriteriaTest()
        {
            IEnumerable<Department> departments = department.GetDepartmentsByMultipleCriteria("Marketing", DateTime.Now);

            Assert.AreEqual(5, departments.Count());
        }
    }
}
