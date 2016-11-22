using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace UFSCar.BD.Commom.Util.Tools
{
    public class Formatar
    {
        /// <summary>
        /// Método que remove acentos, espaços e carateres indesejados
        /// </summary>
        /// <param name="texto">Texto a ser Submetido a retirada dos Caracteres Especiais</param>
        /// <returns>O texto Formatado sem Caracteres Especiais</returns>
        public static string RetirarCaracteresEspeciais(string texto)
        {
            string ComAcentos = "!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";
            string SemAcentos = "_________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";

            for (int i = 0; i < ComAcentos.Length; i++)
            {
                texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();
            }
            return texto;
        }

        public static string RetirarCaracteresEspeciaisENumeros(string texto)
        {
            string ComAcentos = "0123456789!@#$%¨&*()-+=?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";
            string SemAcentos = "_____________________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";

            for (int i = 0; i < ComAcentos.Length; i++)
            {
                texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();
            }
            return texto;
        }

        /// <summary>
        /// Método que remove acentos, espaços e carateres indesejados
        /// </summary>
        /// <param name="texto">Texto a ser Submetido a retirada dos Caracteres Especiais</param>
        /// <returns>O texto Formatado sem Caracteres Especiais</returns>
        public static string RetirarCaracteresEspeciaisTraco(string texto)
        {
            string ComAcentos = "ºª=.!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç \"";
            string SemAcentos = "---------------------AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc--";

            for (int i = 0; i < ComAcentos.Length; i++)
            {
                texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();
            }
            return texto;
        }

        public static string RetirarCaracteresEspeciaisNada(string texto)
        {
            string ComAcentos = "/ºª=.!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç \"";
            string SemAcentos = "______________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc__";

            for (int i = 0; i < ComAcentos.Length; i++)
            {
                texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();
            }
            return texto.Replace("_", "");
        }

        public static string RemoveOnlySpecialCharacterToNothing(string texto)
        {
            string ComAcentos = "/ºª=.!@#$%¨&*()-?:{}][ \"";
            string SemAcentos = "________________________";

            for (int i = 0; i < ComAcentos.Length; i++)
            {
                texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();
            }
            return texto.Replace("_", "");
        }

        public static string MascaraCnpjCpf(string pCnpjCpf)
        {
            string result = "";
            if (pCnpjCpf.Length == 14)
            {
                result = pCnpjCpf.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            }
            if (pCnpjCpf.Length == 11)
            {
                result = pCnpjCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }
            if ((pCnpjCpf.Length != 11) && (pCnpjCpf.Length != 14))
            {
                result = pCnpjCpf;
            }
            return result;
        }

        public static string RelativeDate(DateTime theDate)
        {
            Dictionary<long, string> thresholds = new Dictionary<long, string>();
            int minute = 60;
            int hour = 60 * minute;
            int day = 24 * hour;
            thresholds.Add(60, "{0} segundos atrás");
            thresholds.Add(minute * 2, " minuto atrás");
            thresholds.Add(45 * minute, "{0} minutos atrás");
            thresholds.Add(120 * minute, "hora atrás");
            thresholds.Add(day, "{0} horas atrás");
            thresholds.Add(day * 2, "ontem");
            thresholds.Add(day * 30, "{0} dias atrás");
            //thresholds.Add(day * 365, "{0} meses atrás");
            //thresholds.Add(long.MaxValue, "{0} anos atrás");

            long since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;
            foreach (long threshold in thresholds.Keys)
            {
                if (since < threshold)
                {
                    TimeSpan t = new TimeSpan((DateTime.Now.Ticks - theDate.Ticks));
                    return string.Format(thresholds[threshold], (t.Days > 365 ? t.Days / 365 : (t.Days > 0 ? t.Days : (t.Hours > 0 ? t.Hours : (t.Minutes > 0 ? t.Minutes : (t.Seconds > 0 ? t.Seconds : 0))))).ToString());
                }
            }
            return theDate.ToString("dd-MM-yyyy HH:mm");
        }

        /// <summary>
        /// Remove todo o HTML de uma String
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        public static string StripHTML(string htmlString)
        {
            string pattern = @"<(.|\n)*?>";
            return System.Text.RegularExpressions.Regex.Replace(htmlString, pattern, " ");
        }

        //Remove os caacteres - Copy and Paste do Word
        //Sybase não suporta alguns caracteres da (tabela Windows-1252)
        //CONSULTAR EM: en.wikipedia.org/wiki/Windows-1252#Codepage_layout
        public static string ReplaceWordChars(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var s = text;
                // smart single quotes and apostrophe
                s = Regex.Replace(s, "[\u2018\u2019\u201A]", "'");
                // smart double quotes
                s = Regex.Replace(s, "[\u201C\u201D\u201E]", "\"");
                // ellipsis
                s = Regex.Replace(s, "\u2026", "...");
                // dashes
                s = Regex.Replace(s, "[\u2013\u2014]", "-");
                // circumflex
                s = Regex.Replace(s, "\u02C6", "^");
                // open angle bracket
                s = Regex.Replace(s, "\u2039", "<");
                // close angle bracket
                s = Regex.Replace(s, "\u203A", ">");
                // spaces
                s = Regex.Replace(s, "[\u02DC\u00A0]", " ");
                //bullets
                s = Regex.Replace(s, "[\u002E\u2022]", "-");
                //Replace others
                s = Regex.Replace(s, "[\u2020\u2021\u02C6\u2030\u20AC]", "");

                return s;
            }
            else
                return text;
        }

    }
}
