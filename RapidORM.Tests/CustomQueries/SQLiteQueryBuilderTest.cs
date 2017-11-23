using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Data.SQLite;
using RapidORM.Tests.Core;

namespace RapidORM.Tests.CustomQueries
{
    [TestClass]
    public class SQLiteQueryBuilderTest
    {
        public IQueryBuilder queryBuilder;

        public SQLiteQueryBuilderTest()
        {
            Database.UseDb(DBType.SQLite);
            queryBuilder = new SQLiteQueryBuilder();
        }

        [TestMethod]
        public void CreateSQLiteTableTest()
        {
            string sql = "create table employee(id int,name varchar(50),position varchar(70))";
            queryBuilder.ExecuteNonQuery(sql);

            Assert.Inconclusive("New Table Created");
        }
    }
}
