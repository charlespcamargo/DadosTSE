using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UFSCar.BD.Commom.Atributos
{
    /// <summary>
    /// Classe utilitária para a biblioteca de validação 
    /// </summary>
    public class CustomAnnotationUtil
    {

        #region ValidarCPF
        
        /// <summary>
        /// Válida um CPF
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidarCPF(string value)
        {
            string cpf = value.ToString();

            if (cpf == null)
                return false;

            cpf = cpf.Replace(".", String.Empty).Replace("-", String.Empty).Trim();

            if (cpf.Length != 11)
                return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            int soma = 0;
            for (int i = 0, j = 10, d; i < 9; i++, j--)
            {
                if (!Int32.TryParse(cpf[i].ToString(), out d))
                    return false;
                soma += d * j;
            }

            int resto = soma % 11;

            string digito = (resto < 2 ? 0 : 11 - resto).ToString();
            string prefixo = cpf.Substring(0, 9) + digito;

            soma = 0;
            for (int i = 0, j = 11; i < 10; i++, j--)
                soma += Int32.Parse(prefixo[i].ToString()) * j;

            resto = soma % 11;
            digito += (resto < 2 ? 0 : 11 - resto).ToString();

            return cpf.EndsWith(digito);
        }

        #endregion

        #region ValidarCnpj
        
        /// <summary>
        /// Válida um CNPJ
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidarCnpj(string value)
        {
            if (value == null)
            {
                return true;
            }

            String cnpj = value.ToString();
            Int32[] digitos, soma, resultado;
            Int32 nrDig;
            String ftmt;
            Boolean[] cnpjOk;

            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace(@"\", "");
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("-", "");

            if (cnpj == "00000000000000")
            {
                return false;
            }

            ftmt = "6543298765432";
            digitos = new Int32[14];
            soma = new Int32[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new Int32[2];
            resultado[0] = 0;
            resultado[1] = 0;
            cnpjOk = new Boolean[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        cnpjOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        cnpjOk[nrDig] = (digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (cnpjOk[0] && cnpjOk[1]);
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region ValidarExtensaoArquivo
        
        /// <summary>
        /// Válida extensão do arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="extensoes"></param>
        /// <returns></returns>
        public static bool ValidarExtensaoArquivo(string fileName, string extensoes)
        {
            try
            {
                return extensoes.Split(',').Contains(Path.GetExtension(fileName).ToLowerInvariant());
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion

        #region LimparEspacoBranco
        
        public static void  LimparEspacoBranco(ref object valor)
        {
            if ((valor as string).Length == 1 && (valor as string).Contains(@""""))
            {
                valor = null;
            }

        }

        #endregion
    }
}
