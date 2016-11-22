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
    public class FracaoDolarAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            decimal retNum;

            try
            {
                retNum = (decimal)value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
