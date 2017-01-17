<img src="http://deepmirage.com/git/rapidorm.png" alt="koa middleware framework for nodejs" width="255px" />

Expressive, dynamic and functional Object Relational Mapping technology that allows you to easily perform CRUD operations. RapidORM lets you focus more on the behavior of the app instead of spending more time with the DB communication.
<br/><br/>
More so, RapidORM supports multiple databases in the industry(and growing). <br/>
Depending on your preference, RapidORM also allows you to execute good ol' SQL queries via the QueryBuilder.

## Supported Database

SQL Server, MYSQL

## Getting Started

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
        public static void UseSqlDb()
        {
            DBContext.ConnectionString = new DBConnection()
            {
                Server = "",
                Database = "",
                Username = "",
                Password = ""
            };
        }
    }
}

- Reference the basic libraries -> RapidORM.Data, RapidORM.Helpers, RapidORM.Interfaces. You can choose what library to remove and/or add depending on your requirements.

-  
