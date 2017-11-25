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

    public class NewSearchCriteria
    {
        public object Column { get; set; }
        public string Value { get; set; }
    }

    public static class MyExtensionMethods
    {
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static string ToPropertyName(this DateTime dt)
        {            
            return PropertyHelper.GetPropertyName(() => dt);
        }
    }
}
