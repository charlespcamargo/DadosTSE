using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;

namespace UFSCar.BD.Commom.Atributos
{
    /// <summary>
    /// Classe DataAnnotation que válida uma extesão de arquivo com base em uma lista informada delimitada por ","
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExtensoesAttribute : ValidationAttribute
    {
        private string Extensoes = "";
        public ExtensoesAttribute(string extensoes)
        {
            this.Extensoes = extensoes;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            CustomAnnotationUtil.LimparEspacoBranco(ref value); 

            string valueAsString = value as string;
            if (valueAsString != null)
            {
                string rgPattern = @"[\\\/:\*\?""'<>|]";
                Regex objRegEx = new Regex(rgPattern);
                valueAsString = objRegEx.Replace(valueAsString, "");                

                return CustomAnnotationUtil.ValidarExtensaoArquivo(valueAsString, this.Extensoes);
            }

            return false;
        }
    }
}
