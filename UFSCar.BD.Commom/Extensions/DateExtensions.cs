using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Extensions
{
    public static class DateExtensions
    {
        public static string ToMonthYear(this DateTime date)
        {
            return date.ToString("dd/yyyy");
        }

        public static string ToMonthYear(this DateTime? date)
        {
            if (date != null)
                return ((DateTime)date).ToMonthYear();
            else
                return null;
        }

        private static decimal Format(string cultureInfoName, string value)
        {
            return Convert.ToDecimal(value, new CultureInfo(cultureInfoName));
        }
    }
}
