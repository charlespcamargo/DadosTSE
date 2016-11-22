using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UFSCar.BD.Commom.Atributos
{
    /// <summary>
    /// Classe DataAnnotation que válida se o valor é um double no formato PT - PR
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DoubleAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            double retNum;
            return double.TryParse(Convert.ToString(value),
                NumberStyles.Number |
                NumberStyles.AllowCurrencySymbol |
                NumberStyles.AllowThousands |
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite,
                CultureInfo.CreateSpecificCulture("pt-BR"), out retNum);
        }

    }
}
