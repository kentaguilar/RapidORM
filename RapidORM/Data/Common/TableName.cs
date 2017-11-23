using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : System.Attribute
    {
        public string Name { get; set; }

        public TableName() { }

        public TableName(string name)
        {
            this.Name = name;
        }
    }
}
