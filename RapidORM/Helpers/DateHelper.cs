using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Helpers
{
    public class DateHelper
    {
        public static string GetFormattedDateFromRawDate(DateTime givenDate)
        {
            return givenDate.Year.ToString() + givenDate.Month.ToString("d2") + givenDate.Day.ToString("d2");
        }

        public static string GetDateTimeForDB()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
