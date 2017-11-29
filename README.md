<img src="http://www.deepmirage.com/git/rapidormlogo.png" alt="RapidORM" width="300px"/>

Expressive, dynamic, functional, and easy-to-use Object Relational Mapping technology that allows you to do your CRUD operations on-the-fly. RapidORM lets you focus more on the behavior of your app instead of spending more time composing queries for database operations.
<br/><br/>
More so, RapidORM supports multiple databases in the industry(and growing). <br/>

## NuGet Package

[Rapid ORM Library](https://www.nuget.org/packages/RapidORM.dll/) - Actual Rapid ORM library published on NuGet<br/>
[Rapid ORM Core](https://www.nuget.org/packages/RapidOrm.Core/) - Needed external libraries for RapidORM. Package includes MySql.Data and SQLLite dependencies


## Supported Database

SQL Server, MYSQL, SQLite

## Getting Started
This right here is a sample for SQL Server.
- First off, you need to import the RapidORM packages from NuGet. As easy as finding "RapidORM" after clicking Manage NuGet from your project. Install those two packages and you're all set to use RapidORM.

- Create a database wrapper class inside your project. Create a static method. The content shoud be the instantiation of the DBConnection class. Reference RapidORM.Data. Like so,

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;

namespace RapidORM.Tests.Core
{
    public class Database
    {
        public static void UseDB()
        {
            DBConnection.ConnectionString = new DBConnectionSetting()
            {
                Server = "",
                Database = "",
                Username = "",
                Password = ""
            };
        }
    }
}
```

- Now, create your model. Your model should be the counterpart of your database table. Hence, if I have a "department" table, you could have a "Department" class as well. Please also note that you can interactively create your model using the RapiORM Entity Creator.

- Reference the basic libraries -> <b>RapidORM.Data, RapidORM.Data.Common and RapidORM.Data.SQL(SQL Server)</b>(This could be <b>RapidORM.Data.MySql</b> for MySQL, and <b>RapidORM.Data.SQLite</b> for SQLite). You can choose what library to remove and/or add depending on your requirements.

- Now, map your properties

```c#
[IsPrimaryKey(true)]
[ColumnName("ID")]
public int Id { get; set; }

[ColumnName("Column1")]
public string Column1 { get; set; }
```

- Declare the DB Entity

```c#
private SqlEntity<MyClass> dbEntity = new SqlEntity<MyClass>();;
```

- Here's the sample model.

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Data.Common
using RapidORM.Data.MySQL;

namespace MyNamespace
{
    [TableName("DB Table Name")]
    public class MyClass
    {
        [IsPrimaryKey(true)]
        [ColumnName("ID")]
        public int Id { get; set; }

        [ColumnName("Column1")]
        public string Column1 { get; set; }

        private IDBEntity<MyClass> dbEntity = new SqlEntity<MyClass>();

        #region Class Methods
        public void GetAllUserProfiles()
        {
            var myList = dbEntity.GetAllObjects();
            foreach (var item in myList)
            {
                Console.WriteLine(item.Column1);
            }
        }
        
        //Your other methods here(Please see the test project for reference)
        #endregion 
    }
}
```

- You see, in just a few lines of code, you can now perform DB operations. Basic understanding of OOP is needed and you're set to go. <br/><br/>By the way, RapidORM also comes with a QueryBuilder. So, if you are into queries or your app involves complex joins or logic that can only be achieved using a query, you can solve it using RapidORM as well. A separate comprehensive tutorial will be published soon to highlight RapiORM's capability on different platforms.
<br/><br/>

- That should be it.
