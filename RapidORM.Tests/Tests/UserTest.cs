using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using RapidORM.Interfaces;
using RapidORM.Client.MySQL;
using RapidORM.Tests.Models.SQL;
using System.Collections.Generic;

namespace RapidORM.Tests.Tests
{
    [TestClass]
    public class UserTest
    {
        SystemUser user;

        public UserTest()
        {
            Database.UseDb(DBType.SQL);
            user = new SystemUser();
        }

        [TestMethod]
        public void SaveUserTest()
        {
            user.Save(new SystemUser
            {
                Name = "Cersei Lannister",
                Email = "cersei@gmail.com",
                Password = "cersei123",
                DesignatedPosition = "Queen"
            });

            Assert.Inconclusive("New Department Saved");
        }
    }
}
