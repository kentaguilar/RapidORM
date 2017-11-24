using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using RapidORM.Data.Common;
using RapidORM.Data.SQL;
using RapidORM.Tests.Models.SQL;
using System.Collections.Generic;

namespace RapidORM.Tests.Tests
{
    [TestClass]
    public class UserTest
    {
        SystemUser systemUser;

        public UserTest()
        {
            Database.UseDb(DatabaseType.SQL);
            systemUser = new SystemUser();
        }

        [TestMethod]
        public void SaveUserTest()
        {
            systemUser.Save(new SystemUser
            {
                Name = "Cersei Lannister",
                Email = "cersei@gmail.com",
                Password = "cersei123",
                DesignatedPosition = "Queen"
            });

            Assert.Inconclusive("New Department Saved");
        }

        [TestMethod]
        public void InsertUserAndReturnAnIdTest()
        {
            int savedId = systemUser.InsertUserAndReturnAnId(new SystemUser
            {
                Name = "Queen Elizabeth",
                Email = "elizabeth@gmail.com",
                Password = "1234",
                DesignatedPosition = "Queen"
            });

            Assert.AreEqual(1, savedId);
        }

        [TestMethod]
        public void DeleteUserByPropertyNameTest()
        {
            systemUser.DeleteUserByPropertyName("elizabeth@gmail.com");

            Assert.Inconclusive("User deleted");
        }

        [TestMethod]
        public void DeleteUserByObjectTest()
        {
            systemUser.DeleteUserByObject(new SystemUser
            {
                Id = 2
            });

            Assert.Inconclusive("User deleted");
        }

        [TestMethod]
        public void GetAllSystemUsersTest()
        {
            IEnumerable<SystemUser> systemUsers = systemUser.GetAllSystemUsers();

            Assert.AreEqual(5, systemUsers.Count());
        }

        [TestMethod]
        public void GetUserByDesignatedPositionTest()
        {
            IEnumerable<SystemUser> systemUsers = systemUser.GetUserByDesignatedPosition("Queen");

            Assert.AreEqual(5, systemUsers.Count());
        }

        [TestMethod]
        public void GetUsersByStringCriteriaTest()
        {
            IEnumerable<SystemUser> systemUsers = systemUser.GetUsersByStringCriteria(5);

            Assert.AreEqual(5, systemUsers.Count());
        }

        [TestMethod]
        public void GetUsersByMultipleCriteriaTest()
        {
            IEnumerable<SystemUser> systemUsers = systemUser.GetUsersByMultipleCriteria("Cersei Lannister", "Queen");

            Assert.AreEqual(5, systemUsers.Count());
        }
    }
}
