using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Data.SQLite;
using RapidORM.Tests.Core;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace RapidORM.Tests.CustomQueries
{
    [TestClass]
    public class SQLiteQueryBuilderTest
    {
        public IQueryBuilder queryBuilder;

        public SQLiteQueryBuilderTest()
        {
            Database.UseDb(DatabaseType.SQLite);
            queryBuilder = new SQLiteQueryBuilder();
        }

        [TestMethod]
        public void QueryBuilder_SQLite_CreateDatabase()
        {
            DBConnection.CreateDatabase(DatabaseType.SQLite, "rapidorm");

            Assert.Inconclusive("Database Created");
        }

        [TestMethod]
        public void QueryBuilder_SQLite_CreateTableTest()
        {
            string sql = "create table if not exists employee(id integer primary key autoincrement,name varchar(50),position varchar(70))";
            queryBuilder.ExecuteNonQuery(sql);

            Assert.Inconclusive("New Table Created");
        }

        [TestMethod]
        public void QueryBuilder_SQLite_ExecuteNonQueryTest()
        {
            string sql = "insert into employee(name, position) VALUES(@name, @position)";
            queryBuilder.ExecuteNonQuery(sql, new[]{
                new SQLiteParameter("name", "Justin"),
                new SQLiteParameter("position", "VP"),                
            });

            Assert.Inconclusive("New Employee Saved");
        }

        [TestMethod]
        public void QueryBuilder_SQLite_ExecuteScalarTest()
        {
            string sql = "select * from employee order by 1 desc limit 1";
            var employees = queryBuilder.ExecuteScalar(sql);

            Assert.AreEqual(5, employees);
        }

        [TestMethod]
        public void QueryBuilder_SQLite_ExecuteReaderTest()
        {
            string sql = "select * from employee where id=@id";
            var reader = queryBuilder.ExecuteReader(sql, new[]{
                new SQLiteParameter{ ParameterName="@id", Value = "5" }
            }, CommandType.Text);

            int rows = 0;
            while (reader.Read())
            {
                Console.WriteLine(reader["name"].ToString());
                rows++;
            }

            Assert.AreEqual(2, rows);
        }

        [TestMethod]
        public void QueryBuilder_SQLite_GetDataUsingDataAdapterTest()
        {
            string sql = "select * from employee where id=@id";
            DbParameter[] dbParameter = new DbParameter[]{
                queryBuilder.MakeParameter("@id", 5, DbType.String)
            };

            DataTable result = queryBuilder.GetDataUsingDataAdapter(sql, dbParameter, CommandType.Text);

            for (var i = 0; i < result.Rows.Count; i++)
            {
                Console.WriteLine(result.Rows[i]["name"]);
            }

            Assert.AreEqual(2, result.Rows.Count);
        }
    }
}
