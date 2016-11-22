using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UFSCar.BD.Commom.Atributos
{
    /// <summary>
    /// Classe DataAnnotation que válida se o valor é um decimal no formato PT - PR
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class FracaoRealAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            decimal retNum;

            try
            {
                string valorFinal = ((decimal)value).ToString("N2", new CultureInfo("pt-BR")).ToString();
                return decimal.TryParse(valorFinal,
                                        NumberStyles.Number |
                                        NumberStyles.AllowThousands |
                                        NumberStyles.AllowLeadingSign |
                                        NumberStyles.AllowLeadingWhite |
                                        NumberStyles.AllowTrailingWhite,
                                        CultureInfo.CreateSpecificCulture("pt-BR"), out retNum);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
