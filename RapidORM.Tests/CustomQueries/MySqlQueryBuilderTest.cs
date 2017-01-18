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
using System.Xml;

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
            string sql = "insert into department(name,date_created) VALUES(@name,@datecreated)";
            queryBuilder.ExecuteNonQuery(sql, new[]{
                new MySqlParameter{ ParameterName="name", Value = "Accounting" },
                new MySqlParameter{ ParameterName="datecreated", Value = DateHelper.GetDateTimeForDB() },                
            });

            Assert.Inconclusive("New Department Saved");
        }

        [TestMethod]
        public void QueryBuilderMySqlExecuteScalarTest()
        {
            string sql = "select * from department";
            var systemUsers = queryBuilder.ExecuteScalar(sql);

            Assert.AreEqual(5, systemUsers);
        }

        [TestMethod]
        public void QueryBuilderMySqlExecuteReaderTest()
        {
            string sql = "select * from department where id=@id";
            var reader = queryBuilder.ExecuteReader(sql, new[]{
                new MySqlParameter{ ParameterName="@id", Value = "7" }
            }, CommandType.Text);

            int rows = 0;
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"].ToString());
                rows++;
            }

            Assert.AreEqual(2, rows);
        }

        [TestMethod]
        public void QueryBuilderMySqlGetDataUsingDataAdapterTest()
        {
            string sql = "select * from department where id=@id";
            DbParameter[] dbParameter = new DbParameter[]{
                queryBuilder.MakeParameter("@id", 7, DbType.String)
            };

            DataTable result = queryBuilder.GetDataUsingDataAdapter(sql, dbParameter, CommandType.Text);

            for (var i = 0; i < result.Rows.Count; i++)
            {
                Console.WriteLine(result.Rows[i]["Name"]);
            }

            Assert.AreEqual(2, result.Rows.Count);
        }
    }
}
