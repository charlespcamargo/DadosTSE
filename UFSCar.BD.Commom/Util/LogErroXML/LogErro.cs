using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom
{
    public static class LogErro
    {

        #region Métodos

        public static void GravarLog(Exception pex)
        {
            GravarLog(pex, null);
        }


        public static void GravarLog(Exception pex, string prefixPath)
        {
            GravarLog(pex, null, prefixPath);
        }

        /// <summary>
        /// Grava o log da exceção gerada pela aplicação
        /// </summary>
        /// <param name="pex">Exceção gerada</param>
        /// <remarks>
        /// </remarks>
        public static void GravarLog(Exception pex, Dictionary<string, string> valoresPersonalizados, string prefixPath = "")
        {
            try
            {

                DataTable dtLogErro = new DataTable("LOG");
                dtLogErro.Columns.Add("Data");
                dtLogErro.Columns.Add("Classe");
                dtLogErro.Columns.Add("Metodo");
                dtLogErro.Columns.Add("Mensagem");
                dtLogErro.Columns.Add("StackTrace");
                dtLogErro.Columns.Add("InnerException");
                if (valoresPersonalizados != null)
                {
                    foreach (var item in valoresPersonalizados)
                    {
                        dtLogErro.Columns.Add(item.Key);
                    }
                }

                string dataPath = DateTime.Now.ToShortDateString();
                dataPath = dataPath.Replace("/", "");

                string diretorio = "";
                if (ConfigurationManager.AppSettings["Path.Logs"] != null)
                    diretorio = Path.Combine(ConfigurationManager.AppSettings["Path.Logs"], dataPath);
                else
                    diretorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogErro", dataPath);

                string name = ObterNomeArquivoLog(prefixPath);

                //Verifica se existe um diretório configurado para gravar as exceções
                if (diretorio != null)
                {
                    if (!Directory.Exists(diretorio))
                    {
                        Directory.CreateDirectory(diretorio);
                    }
                    if (File.Exists(Path.Combine(diretorio, name)))
                    {
                        dtLogErro.ReadXml(Path.Combine(diretorio, name));
                    }
                }
                else
                {
                    if (File.Exists(name))
                    {
                        dtLogErro.ReadXml(name);
                    }
                }

                //Cria o schema do datatable de log
                DataRow drLogErro = dtLogErro.NewRow();
                drLogErro["Data"] = DateTime.Now;
                drLogErro["Classe"] = pex.TargetSite.ReflectedType.Name;
                drLogErro["Metodo"] = ((System.Reflection.MemberInfo)(pex.TargetSite)).Name;
                drLogErro["Mensagem"] = pex.Message;
                drLogErro["StackTrace"] = pex.StackTrace;

                if (pex.InnerException != null)
                    drLogErro["InnerException"] = pex.InnerException.Message;

                if (valoresPersonalizados != null)
                {
                    foreach (var item in valoresPersonalizados)
                    {
                        drLogErro[item.Key] = item.Value;
                    }
                }

                dtLogErro.Rows.Add(drLogErro);

                if (diretorio != null)
                {
                    dtLogErro.WriteXml(Path.Combine(diretorio, name));
                }
                else
                {
                    dtLogErro.WriteXml(name);
                }
            }
            catch
            {
                //throw ex;
            }
        }

        /// <summary>
        /// Retorna o nome do arquivo de log conforme a data atual do servidor
        /// </summary>
        /// <returns>string contendo o nome do arquivo de log</returns>
        private static string ObterNomeArquivoLog(string prefixPath)
        {
            string name = "ERRO_" + (prefixPath != "" ? prefixPath + "_" : "") + ".xml";

            return name;
        }

        #endregion
    }
}
