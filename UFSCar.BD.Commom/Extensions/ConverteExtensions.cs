using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Extensions
{
    public static class ConverteExtensions
    {
        public static int ToInt32(this decimal value)
        {
            return Convert.ToInt32(Math.Round(value,0));
        }

        public static decimal ToDecimal(this int value)
        {
            return Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(this decimal? value)
        {
            if (value == null)
                value = 0;

            return Convert.ToDecimal(value);
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
