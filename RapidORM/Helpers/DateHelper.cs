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
        /// <summary>
        /// Converts normal datetime value to a formatted one
        /// </summary>
        /// <param name="givenDate"></param>
        public static string GetFormattedDateFromRawDate(DateTime givenDate)
        {
            return givenDate.Year.ToString() + givenDate.Month.ToString("d2") + givenDate.Day.ToString("d2");
        }

        /// <summary>
        /// Get current time that is compatible with database datetime type
        /// </summary>
        /// <param name=""></param>
        public static string GetDateTimeForDB()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// Check if given raw date is valid date
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        /// Converts date in string format to datetime format
        /// </summary>
        /// <param name="value"></param>
        public static DateTime ConvertDateStringToDateTime(string value)
        {
            return DateTime.ParseExact(value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
