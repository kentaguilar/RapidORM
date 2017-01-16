using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Client.SQL;
using RapidORM.Interfaces;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace RapidORM.Tests.CustomQueries
{
    [TestClass]
    public class SqlQueryBuilderTest
    {
        public IQueryBuilder queryBuilder;

        public SqlQueryBuilderTest()
        {
            Database.UseMySqlDb();
            queryBuilder = new SqlQueryBuilder();
        }

        [TestMethod]
        public void QueryBuilderSqlExecuteNonQueryTest()
        {
            string sql = "insert into Car(Name,Power,Cost) VALUES(@name,@power,@cost)";
            queryBuilder.ExecuteNonQuery(sql, new[]{
                new SqlParameter{ ParameterName="name", Value = "Ferrari" },
                new SqlParameter{ ParameterName="power", Value = 10 },
                new SqlParameter{ ParameterName="cost", Value = 100 },
            });

            Assert.Inconclusive("Car Inserted");
        }
    }
}
