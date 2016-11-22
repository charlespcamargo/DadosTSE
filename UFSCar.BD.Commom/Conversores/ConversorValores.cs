using UFSCar.BD.Commom.Util.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Conversores
{
    public static class ConversorValores
    {
        //Precisa arrumar depois o esquema da conversão pq a lista de erros deixou de ser um atributo estático  
        
        
        
        //public static decimal ToDecimal(this string valorOrigem, string atributo)
        //{
        //    try
        //    {
        //        var valor = (valorOrigem != null && !string.IsNullOrWhiteSpace(valorOrigem.Trim()) ? Convert.ToDecimal(valorOrigem) : 0);
        //        valorOrigem = valor.ToString("N7");
        //        return Convert.ToDecimal(valorOrigem);
        //    }
        //    catch
        //    {
        //        if (ValidadorErros.ListaErros == null)
        //        {
        //            ValidadorErros.ListaErros = new List<ModeloValidacao>();
        //        }

        //        ValidadorErros.ListaErros.Add(new ModeloValidacao { Atributo = atributo, TipoValidacao = EnumTipoValidacao.ConversaoDecimal, TipoErro = EnumTipoErro.Planilha, MensagemCustom = " [" + valorOrigem + "] " });
        //        return 0;
        //    }
        //}


        //public static decimal ToPercentage(this string valorOrigem, string atributo)
        //{
        //    try
        //    {
        //        var valor = (valorOrigem != null && !string.IsNullOrWhiteSpace(valorOrigem.Trim()) ? Convert.ToDecimal(valorOrigem) : 0);
        //        valorOrigem = valor.ToString("N2");
        //        return Convert.ToDecimal(valorOrigem);
        //    }
        //    catch
        //    {
        //        if (ValidadorErros.ListaErros == null)
        //        {
        //            ValidadorErros.ListaErros = new List<ModeloValidacao>();
        //        }

        //        ValidadorErros.ListaErros.Add(new ModeloValidacao { Atributo = atributo, TipoValidacao = EnumTipoValidacao.ConversaoDecimal, TipoErro = EnumTipoErro.Planilha, MensagemCustom = " [" + valorOrigem + "] " });
        //        return 0;
        //    }
        //}

        //public static DateTime ToDateTime(this string valorOrigem, string atributo)
        //{
        //    try
        //    {
        //        return DateTime.Parse(valorOrigem);
        //    }
        //    catch
        //    {
        //        if (ValidadorErros.ListaErros == null)
        //        {
        //            ValidadorErros.ListaErros = new List<ModeloValidacao>();
        //        }

        //        ValidadorErros.ListaErros.Add(new ModeloValidacao { Atributo = atributo, TipoValidacao = EnumTipoValidacao.ConversaoData, TipoErro = EnumTipoErro.Planilha, MensagemCustom = " [" + valorOrigem + "] " });
        //        return default(DateTime);
        //    }
        //}

        //public static bool ToBool(this string valorOrigem, string atributo)
        //{
        //    if (string.IsNullOrEmpty(valorOrigem))
        //    {
        //        if (ValidadorErros.ListaErros == null)
        //        {
        //            ValidadorErros.ListaErros = new List<ModeloValidacao>();
        //        }

        //        ValidadorErros.ListaErros.Add(new ModeloValidacao { Atributo = atributo, TipoValidacao = EnumTipoValidacao.ConversaoBooleano, TipoErro = EnumTipoErro.Planilha, MensagemCustom = " [" + valorOrigem + "] " });
        //        return false;
        //    }

        //    if (valorOrigem.Trim().ToUpper() == "SIM")
        //    {
        //        return true;
        //    }
        //    else if (valorOrigem.Trim().ToUpper() == "NÃO" || valorOrigem.Trim().ToUpper() == "NAO")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (ValidadorErros.ListaErros == null)
        //        {
        //            ValidadorErros.ListaErros = new List<ModeloValidacao>();
        //        }

        //        ValidadorErros.ListaErros.Add(new ModeloValidacao { Atributo = atributo, TipoValidacao = EnumTipoValidacao.ConversaoBooleano, TipoErro = EnumTipoErro.Planilha, MensagemCustom = " [" + valorOrigem + "] " });
        //        return false;
        //    }
        //}

        //public static string ValidarTamanho(this string valorOrigem, string atributo, int tamanho)
        //{
        //    if (string.IsNullOrEmpty(valorOrigem))
        //    {
        //        return string.Empty;
        //    }

        //    if (valorOrigem.Length > tamanho)
        //    {
        //        if (ValidadorErros.ListaErros == null)
        //        {
        //            ValidadorErros.ListaErros = new List<ModeloValidacao>();
        //        }

        //        ValidadorErros.ListaErros.Add(new ModeloValidacao { Atributo = atributo, TipoValidacao = EnumTipoValidacao.TamanhoCampo, TipoErro = EnumTipoErro.Planilha, MensagemCustom = " [" + valorOrigem + "]. Tamanho esperado: " + tamanho + " Caracter" + (tamanho > 1 ? "es" : "") });
        //        return string.Empty;
        //    }
        //    return valorOrigem;
        //}
    }
}
