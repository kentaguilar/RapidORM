using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data.Common
{
    public enum ReturnsId
    {
        Yes,
        No
    }

    public enum SpecialCharacter
    { 
        Yes,
        No
    }

    public enum DatabaseType
    {
        SQL,
        MySql,
        SQLite
    }

    public enum FileType
    {
        CSV,
        XLSX,
        TXT
    }
}
