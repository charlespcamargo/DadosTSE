using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Extensions
{
    public static class CurrencyExtensions
    {
        public static decimal ToReal(this string value)
        {
            return Format("pt-BR", value);
        }

        private static decimal Format(string cultureInfoName, string value)
        {
            return Convert.ToDecimal(value, new CultureInfo(cultureInfoName));
        }
    }
}
