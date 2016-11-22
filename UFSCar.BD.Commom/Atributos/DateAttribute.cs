using System;
using System.ComponentModel.DataAnnotations;

namespace UFSCar.BD.Commom.Atributos
{
    /// <summary>
    /// Classe DataAnnotation que válida se é um data válida
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateAttribute : ValidationAttribute
    {
        
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            DateTime retDate;
            return DateTime.TryParse(Convert.ToString(value), out retDate);
        }
    }
}
