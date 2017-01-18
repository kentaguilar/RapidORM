using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Client.MySQL;
using RapidORM.Interfaces;
using RapidORM.Helpers;
using RapidORM.Tests.Core;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace RapidORM.Tests.CustomQueries
{
    [TestClass]
    public class MySqlQueryBuilderTest
    {
        public IQueryBuilder queryBuilder;

        public MySqlQueryBuilderTest()
        {
            Database.UseDb(DBType.MySql);
            queryBuilder = new MySqlQueryBuilder();
        }

        [TestMethod]
        public void QueryBuilderMySqlExecuteNonQueryTest()
        {
            string sql = "insert into car(name,power,cost) VALUES(@name,@power,@cost)";
            queryBuilder.ExecuteNonQuery(sql, new[]{
                new MySqlParameter{ ParameterName="name", Value = "Toyota Fortuner" },
                new MySqlParameter{ ParameterName="power", Value = 100 },
                new MySqlParameter{ ParameterName="cost", Value = 10000 },
            });

            Assert.Inconclusive("Car Inserted");
        }
    }
}
