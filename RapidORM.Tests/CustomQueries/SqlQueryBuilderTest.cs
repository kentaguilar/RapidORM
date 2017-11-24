using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Data.SQL;
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
            Database.UseDb(DatabaseType.SQL);
            queryBuilder = new SqlQueryBuilder();
        }

        [TestMethod]
        public void QueryBuilderSqlExecuteNonQueryTest()
        {
            string sql = "insert into systemuser(name,email,password,designatedposition) values(@name,@email,@password,@designation)";
            queryBuilder.ExecuteNonQuery(sql, new[]{
                new SqlParameter{ ParameterName="name", Value = "Queen Victoria" },
                new SqlParameter{ ParameterName="email", Value = "victoria@gmail.com" },                
                new SqlParameter{ ParameterName="password", Value = "1234" },                
                new SqlParameter{ ParameterName="designation", Value = "Queen" }
            });

            Assert.Inconclusive("New User Saved");
        }

        [TestMethod]
        public void QueryBuilderSqlExecuteScalarTest()
        {
            string sql = "select * from systemuser order by id desc";
            var systemUsers = queryBuilder.ExecuteScalar(sql);
            
            Assert.AreEqual(5, systemUsers);
        }

        [TestMethod]
        public void QueryBuilderSqlExecuteReaderTest()
        {
            string sql = "select * from systemuser where id=@id";
            var reader = queryBuilder.ExecuteReader(sql, new[]{
                new SqlParameter{ ParameterName="@id", Value = "5" }
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
        public void QueryBuilderSqlGetDataUsingDataAdapterTest()
        {
            string sql = "select * from systemuser where id=@id";
            DbParameter[] dbParameter = new DbParameter[]{
                queryBuilder.MakeParameter("@id", 6, DbType.String)
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
