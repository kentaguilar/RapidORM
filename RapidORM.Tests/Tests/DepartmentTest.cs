using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using RapidORM.Interfaces;
using RapidORM.Client.MySQL;
using RapidORM.Tests.Models.MySQL;

namespace RapidORM.Tests.Tests
{
    [TestClass]
    public class DepartmentTest
    {
        public IQueryBuilder queryBuilder;

        public DepartmentTest()
        {
            Database.MySqlDb();
            queryBuilder = new MySqlQueryBuilder();
        }

        [TestMethod]
        public void SaveDepartmentTest()
        {
            Department department = new Department();
            department.Save(new Department 
            { 
                Name = "Inventory",
                DateCreated = DateHelper.GetDateTimeForDB()
            });

            Assert.Inconclusive("New Department Saved");
        }
    }
}
