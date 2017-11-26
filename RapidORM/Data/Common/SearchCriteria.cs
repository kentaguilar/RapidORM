using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using RapidORM.Helpers;

namespace RapidORM.Data.Common
{
    public class SearchCriteria
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}
