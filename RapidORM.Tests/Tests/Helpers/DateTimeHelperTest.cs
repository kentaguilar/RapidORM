using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RapidORM.Helpers;

namespace RapidORM.Tests.Tests.Helpers
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        public void GetDateTimeForDBTest()
        {
            Assert.AreEqual("test", DateHelper.GetDateTimeForDB());
        }
    }
}
