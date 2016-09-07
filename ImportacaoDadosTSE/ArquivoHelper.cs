using UFSCar.BD.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFSCar.BD.BackEnd.Business;
using static UFSCar.BD.Model.Enumeradores;

namespace ImportacaoDadosTSE
{
    public class ArquivoHelper
    {

        public static ArquivoHelper New
        {
            get
            {
                return new ArquivoHelper();
            }
        }

        public void Iniciar()
        {
            Console.WriteLine("O que você gostaria de fazer ?");
            Console.WriteLine("0 - Limpar Console");
            Console.WriteLine("1 - Processar todos os arquivos");
            Console.WriteLine("2 - Processar os arquivos dos Candidatos");
            Console.WriteLine("3 - Processar os arquivos dos Bens dos Candidatos");
            Console.WriteLine("4 - Processar os arquivos dos Legendas");
            Console.WriteLine("5 - Processar os arquivos das Vagas");
            Console.WriteLine("9 - Fechar");
            Console.WriteLine(Environment.NewLine);


            ConsoleKeyInfo k = Console.ReadKey();

            switch (k.Key)
            {
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    Console.Clear();
                    break;

                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ProcurarArquivos(eTipoArquivo.Todos);
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    ProcurarArquivos(eTipoArquivo.Candidatos);
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    ProcurarArquivos(eTipoArquivo.BensDosCandidatos);
                    break;

                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    ProcurarArquivos(eTipoArquivo.Legendas);
                    break;

                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    ProcurarArquivos(eTipoArquivo.Vagas);
                    break;

                case ConsoleKey.NumPad9:
                case ConsoleKey.D9:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("A opção informada ({0}) é inválida.", k.Key);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine(Environment.NewLine);
                    break;
            }

            Console.WriteLine(Environment.NewLine);
            Iniciar();
        }

        private void ProcurarArquivos(eTipoArquivo tipo, string path = "")
        {
            if (String.IsNullOrEmpty(path))
            {
                path = System.Configuration.ConfigurationManager.AppSettings["DiretorioPadrao"];
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(@"O diretório atual de leituras dos arquivos é [{0}]", path);

            if (Directory.Exists(path))
            {
                DirectoryInfo d = new DirectoryInfo(path);
                FileInfo[] lstArquivos = d.GetFiles();

                if (lstArquivos != null && lstArquivos.Length > 0)
                {
                    foreach (FileInfo arquivo in lstArquivos)
                    {
                        ProcessarArquivo(arquivo, tipo);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum arquivo foi encontrado neste diretório.");

                    DirectoryInfo[] lstDiretorios = d.GetDirectories();
                    if (lstDiretorios != null && lstDiretorios.Length > 0)
                    {
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Um ou mais diretorios foram encontrados. Informe o número do diretorio que deseja processar: ");
                        for (int i = 0; i < lstDiretorios.Length; i++)
                        {
                            Console.WriteLine("{0} - Para processar o diretorio [{1}]", i, lstDiretorios[i].Name);
                        }

                        ConsoleKeyInfo tecla = Console.ReadKey();

                        if (Char.IsNumber(tecla.KeyChar))
                        {
                            int numero = Convert.ToInt32(tecla.KeyChar.ToString());

                            if (numero >= 0 && numero <= lstDiretorios.Length - 1)
                            {
                                string newpath = lstDiretorios[numero].FullName;
                                ProcurarArquivos(tipo, newpath);
                            }
                            else
                                Console.WriteLine("Número inválido de diretório, comece novamente.");
                        }
                        else
                            Console.WriteLine("Informação digitada não é um número válido, comece novamente.");
                    }

                }
            }
            else
            {
                Console.WriteLine("O diretório não existe");
            }

        }

        private void ProcessarArquivo(FileInfo arquivo, eTipoArquivo tipo)
        {
            IArquivo iArquivo = ObterDadosDoNomeDoArquivo(arquivo.Name);

            if (iArquivo != null)
            {
                ProcessarArquivoItens(iArquivo, arquivo);

                if (iArquivo.Registros != null && iArquivo.Registros.Count > 0)
                {
                    ImportacaoArquivoBL.New.Salvar(iArquivo);
                }
            }
        }

        private void ProcessarArquivoItens(IArquivo iArquivo, FileInfo arquivo)
        {
            using (StreamReader sr = arquivo.OpenText())
            {
                iArquivo.Registros = new List<IArquivoItem>();
                ArquivoCandidatosItem item = null;
                string linha = "";

                while ((linha = sr.ReadLine()) != null)
                {
                    string[] linhaPropriedades = linha.Split(new string[] { ";" }, StringSplitOptions.None);

                    item = new ArquivoCandidatosItem();
                    System.Reflection.PropertyInfo[] lstPropriedades = item.GetType().GetProperties();

                    for (int i = 0; i < linhaPropriedades.Length; i++)
                        lstPropriedades[i].SetValue(item, linhaPropriedades[i]);

                    iArquivo.Registros.Add(item);
                }
            }
        }

        private IArquivo ObterDadosDoNomeDoArquivo(string nome)
        {
            try
            {
                string[] arrNome = nome.Split(new string[] { "_", ".txt" }, StringSplitOptions.None);

                if (arrNome != null && arrNome.Length >= 4)
                {
                    ArquivoCandidatos candidatos = new ArquivoCandidatos();
                    candidatos.Nome = nome;
                    candidatos.TipoArquivo = ObterTipoArquivo(arrNome[0] + "_" + arrNome[1]);
                    candidatos.Ano = Convert.ToInt32(arrNome[2]);
                    candidatos.UF = arrNome[3];


                    return candidatos;
                }
                else
                {
                    Console.WriteLine("Arquivo em formato desconhecido");

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ao processar o Arquivo " + nome);

                throw ex;
            }

        }

        private eTipoArquivo ObterTipoArquivo(string tipoArquivo)
        {
            tipoArquivo = tipoArquivo.ToUpper();

            if (tipoArquivo == "CONSULTA_CAND")
                return eTipoArquivo.Candidatos;
            else if (tipoArquivo == "BEM_CANDIDATO")
                return eTipoArquivo.BensDosCandidatos;
            else if (tipoArquivo == "CONSULTA_LEGENDAS")
                return eTipoArquivo.Legendas;
            else if (tipoArquivo == "CONSULTA_VAGAS")
                return eTipoArquivo.Vagas;
            else
            {
                Console.WriteLine("Arquivo em formato de leitura desconhecido");
                return eTipoArquivo.Desconhecido;
            }
        }


    }


}
