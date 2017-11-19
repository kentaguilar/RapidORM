using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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

        public static bool IsValidDate(string value)
        {
            string format = "MM/dd/yy";
            DateTime dt;

            if (DateTime.TryParseExact((String)value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DateTime ConvertDateStringToDateTime(string value)
        {
            return DateTime.ParseExact(value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
