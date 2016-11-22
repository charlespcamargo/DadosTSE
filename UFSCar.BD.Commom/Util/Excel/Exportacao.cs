using UFSCar.BackEnd.Common;
using UFSCar.BD.Commom.Reflector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;

namespace UFSCar.BD.Commom.Util.Excel
{
    public class RelatorioExcel
    {
        public string Campo { get; set; }
        public string Titulo { get; set; }
        public int Posicao { get; set; }

    }

    public class Exportacao
    {
        public static byte[] ObterRelatorio(DataTable dados, int primeiraLinhaDados, int linhaTitulo, int linhaCampo, string arquivoTemplate, string nomePlanilha)
        {
            //ResultBusiness<byte[]> resultBusiness = new ResultBusiness<byte[]>();

            List<RelatorioExcel> posicaoColunas = new List<RelatorioExcel>();

            FileInfo template = new FileInfo(arquivoTemplate);

            using (ExcelPackage excelPackage = new ExcelPackage(template, true))
            {
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[nomePlanilha];

                if (workSheet.Cells["D3"].Value != null && workSheet.Cells["D3"].Value.Equals("##data_relatorio##"))
                    workSheet.Cells["D3"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

                //--------------------------------------------------------------------------------------------------
                //Identifica a posição das colunas no Excel
                //--------------------------------------------------------------------------------------------------
                string nomeCampo = null;
                string titulo = null;

                for (int coluna = 1; coluna <= 400; coluna++)
                {
                    nomeCampo = workSheet.Cells[linhaCampo, coluna].Value != null ? workSheet.Cells[linhaCampo, coluna].Value.ToString():"";
                    titulo = workSheet.Cells[linhaTitulo, coluna].Value != null ? workSheet.Cells[linhaTitulo, coluna].Value.ToString():"";

                    if (!String.IsNullOrEmpty(nomeCampo))
                        posicaoColunas.Add(new RelatorioExcel { Campo = nomeCampo, Posicao = coluna, Titulo = titulo });
                    else
                        break;
                }


                //--------------------------------------------------------------------------------------------------
                //Obtem as coluna do datatable
                //--------------------------------------------------------------------------------------------------
                List<string> colunasDT = new List<string>();
                foreach (DataColumn item in dados.Columns)
                    colunasDT.Add(item.ColumnName);


                //--------------------------------------------------------------------------------------------------
                //Popular as linhas de dados
                //--------------------------------------------------------------------------------------------------
                foreach (DataRow dr in dados.Rows)
                {
                    foreach (RelatorioExcel posColuna in posicaoColunas)
                    {
                        workSheet.Cells[primeiraLinhaDados, posColuna.Posicao].Value = dr[posColuna.Campo];
                    }

                    primeiraLinhaDados++;
                }


                //--------------------------------------------------------------------------------------------------
                //Limpar a linha contendo o nome dos camposq do relatório
                //--------------------------------------------------------------------------------------------------
                foreach (RelatorioExcel posColuna in posicaoColunas)
                    workSheet.Cells[linhaCampo, posColuna.Posicao].Value = "";

                return excelPackage.GetAsByteArray();
            }
            
        }
        
        public static byte[] ExportarExcels<T>(string nome, List<T> listaValores, List<FormataExportacao> listaFormatada)
        {
            ExcelPackage pck = ExtraiPackage<T>(nome, listaValores, listaFormatada);
            byte[] novaPlanilha = pck.GetAsByteArray();

            return novaPlanilha;
        }

        public static byte[] ExportarExcels<T>(List<string> nome, List<List<T>> listaValores, List<List<FormataExportacao>> listaFormatada)
        {
            ExcelPackage pck = ExtraiPackage<T>(nome, listaValores, listaFormatada);
            byte[] novaPlanilha = pck.GetAsByteArray();

            return novaPlanilha;
        }

        public static void ExportarExcelsPrompt<T>(string nome, List<T> listaValores, List<FormataExportacao> listaFormatada, string caminhoNovaPlanilha)
        {
            ExcelPackage pck = ExtraiPackage<T>(nome, listaValores, listaFormatada);
            byte[] novaPlanilha = pck.GetAsByteArray();

            ByteArrayToFile(Path.Combine(caminhoNovaPlanilha, nome), novaPlanilha);
        }

        private static bool ByteArrayToFile(string fileName, byte[] bytesPlanilha)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream fileStream = new System.IO.FileStream(fileName + ".xlsx", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from a byte array.
                fileStream.Write(bytesPlanilha, 0, bytesPlanilha.Length);

                // close file stream
                fileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return false;
        }

        private static ExcelPackage ExtraiPackage<T>(List<string> nome, List<List<T>> listaValores, List<List<FormataExportacao>> listaFormatada)
        {
            ExcelPackage pck = new ExcelPackage();
            for (int i = 0; i < nome.Count; i++)
            {
                var ws = pck.Workbook.Worksheets.Add(nome[i]);
                ws.View.ShowGridLines = true;
                MontarWorksheet<T>(listaValores[i], listaFormatada[i], ws);
            }
            return pck;
        }

        private static ExcelPackage ExtraiPackage<T>(string nome, List<T> listaValores, List<FormataExportacao> listaFormatada)
        {
            int row = 1;

            ExcelPackage pck = new ExcelPackage();
            //Add the Content sheet
            var ws = pck.Workbook.Worksheets.Add(nome);
            ws.View.ShowGridLines = true;

            MontarWorksheet<T>(listaValores, listaFormatada, ws);
            return pck;
        }

        private static void MontarWorksheet<T>(List<T> listaValores, List<FormataExportacao> listaFormatada, ExcelWorksheet ws)
        {
            int row = 1;

            RefletorDinamico refletor = new RefletorDinamico(typeof(T));
            try
            {
                for (int i = 0; i < listaFormatada.Count; i++)
                {
                    ws.Cells[row, i + 1].Style.Font.Bold = true;
                    ws.Cells[row, i + 1].Style.WrapText = false;
                    ws.Cells[row, i + 1].Value = listaFormatada[i].NomeColuna;
                }

                row++;

                foreach (T item in listaValores)
                {

                    for (int i = 0; i < listaFormatada.Count; i++)
                    {
                        string valor = "";
                        string propriedade = listaFormatada[i].NomePropriedade;
                        bool ehRico = false;
                        if (propriedade.Contains(".")) ehRico = true;

                        try
                        {
                            PropertyInfo prop = refletor.Propriedade(propriedade, ehRico);
                            if (!ehRico)
                                valor = prop.GetValue((T)item, null).ToString();
                            else
                                valor = refletor.GetPropValueObjRico(propriedade, (T)item).ToString();

                            if (valor == "True")
                                valor = "Sim";
                            else if (valor == "False")
                                valor = "Não";
                        }
                        catch
                        {
                            valor = "";
                        }

                        ws.Cells[row, i + 1].RichText.Add(valor);
                            ws.Cells[row, i + 1].IsRichText = true;
                        ws.Cells[row, i + 1].Style.WrapText = false;
                    }

                    row++;
                }



            }
            catch
            {
                throw;
            }

          
        }
    }

    public class FormataExportacao
    {
        public string NomeColuna { get; set; }
        public string NomePropriedade { get; set; }  
    }
}
