using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnName : Attribute
    {
        public string Name { get; set; }

        public ColumnName() { }

        public ColumnName(string name)
        {
            this.Name = name;
        }
    }
}
