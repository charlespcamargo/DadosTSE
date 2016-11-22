using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace UFSCar.BD.Commom.Atributos
{
    /// <summary>
    /// Classe DataAnnotation que válida CPF
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CPF_CNPJAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            if (value.ToString().Length > 11)
                return CustomAnnotationUtil.ValidarCnpj(value.ToString());
            else
                return CustomAnnotationUtil.ValidarCPF(value.ToString());
        }
    }
}
