using UFSCar.BackEnd.Common;
using UFSCar.BD.Commom.Reflector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UFSCar.BackEnd.Common.Util
{
    public class ExcelImportacao
    {
        public static List<T> Importar<T>(byte[] bytesArquivo, List<FormataImportacao> listaFormatacao) where T : new()
        {
            return Importar<T>(bytesArquivo, string.Empty, true, listaFormatacao);
        }

        public static List<T> Importar<T>(byte[] bytesArquivo, bool ignoreFirstLine, List<FormataImportacao> listaFormatacao) where T : new()
        {
            return Importar<T>(bytesArquivo, string.Empty, ignoreFirstLine, listaFormatacao);
        }

        public static List<T> Importar<T>(byte[] bytesArquivo, string NomeABA, bool ignoreFirstLine, List<FormataImportacao> listaFormatacao) where T : new()
        {
            try
            {
                int rowInitial = (ignoreFirstLine) ? 2 : 1;

                List<T> lista = new List<T>();

                using (var ms = new MemoryStream(bytesArquivo))
                {
                    using (var package = new ExcelPackage(ms))
                    {
                        // Get the work book in the file
                        ExcelWorkbook workBook = package.Workbook;
                        if (workBook != null)
                        {
                            if (workBook.Worksheets.Count > 0)
                            {
                                // Get the first worksheet OR GET NomeABA
                                ExcelWorksheet currentWorksheet = (string.IsNullOrEmpty(NomeABA) ? workBook.Worksheets.First() : workBook.Worksheets[NomeABA]);

                                for (int row = rowInitial; row <= currentWorksheet.Dimension.End.Row; row++)
                                {
                                    T objeto = new T();
                                    foreach (var format in listaFormatacao)
                                    {
                                        try
                                        {
                                            object valor = currentWorksheet.Cells[row, format.ColumnIndex].Value;
                                            objeto = Popular<T>(objeto, format.NomePropriedade, valor);
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new ArgumentException("Coluna [" + format.ColumnIndex + "] está com formato inválido, por favor verifique.");
                                        }
                                    }

                                    lista.Add(objeto);
                                }
                            }
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static T Popular<T>(object obj, string nomePropriedade, object valorPropriedade) where T : new()
        {
            if (obj == null)
                obj = new T();

            RefletorDinamico refletor = new RefletorDinamico(obj.GetType());

            object objvalue = valorPropriedade;
            if (objvalue != null && objvalue.ToString().ToLower() != "null")
            {
                if (nomePropriedade.Contains("."))
                {
                    string nomeInstancia = nomePropriedade.Split('.')[0];
                    string nomeProp = nomePropriedade.Split('.')[1];

                    //PROPRIEDADE RICA
                    PropertyInfo pi = refletor.Propriedade(nomeInstancia, false);

                    PropertyInfo piRico = refletor.Propriedade(nomePropriedade.Split('.')[0], false);
                    Type tipoRico = piRico.PropertyType;
                    objvalue = PopularObjetoRico(objvalue, tipoRico, nomePropriedade, obj);

                    //POPULA PROP RICA
                    pi.SetValue(obj, refletor.GetConvertedObject(pi.PropertyType, objvalue), null);
                }
                else
                {
                    PropertyInfo pi = refletor.Propriedade(nomePropriedade, false);
                    if (pi != null)
                        pi.SetValue(obj, refletor.GetConvertedObject(pi.PropertyType, objvalue), null);
                }
            }

            return (T)obj;
        }

        private static object PopularObjetoRico(object objvalue, Type tipoRico, string nomePropriedadeCompleta, object objetoPopulado)
        {
            RefletorDinamico refletor = new RefletorDinamico(tipoRico);

            string nomeInstancia = nomePropriedadeCompleta.Split('.')[0];
            string nomeProp = nomePropriedadeCompleta.Split('.')[1];
            object novo = null;

            RefletorDinamico refletorPai = new RefletorDinamico(objetoPopulado.GetType());
            PropertyInfo piPai = refletorPai.Propriedade(nomeInstancia, false);
            if (piPai != null)
            {
                novo = piPai.GetValue(objetoPopulado, null);
            }

            if (novo == null)
                novo = Activator.CreateInstance(tipoRico);

            PropertyInfo pii = refletor.Propriedade(nomeProp, true);
            if (pii != null)
                pii.SetValue(novo, refletor.GetConvertedObject(pii.PropertyType, objvalue), null);

            return novo;
        }

    }

    public class FormataImportacao
    {
        public FormataImportacao() { }
        public FormataImportacao(int columnIndex, string nomePropriedade) { ColumnIndex = columnIndex; NomePropriedade = nomePropriedade; }

        public int ColumnIndex { get; set; }
        public string NomePropriedade { get; set; }
    }
}
