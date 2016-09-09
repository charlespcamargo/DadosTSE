using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFSCar.BD.Model;
using UFSCar.BD.Repository;

namespace UFSCar.BD.BackEnd.Business
{
    public class ImportacaoArquivoBL
    {
        public static ImportacaoArquivoBL New
        {
            get { return new ImportacaoArquivoBL(); }
        }

        public void Salvar(IArquivo iArquivo)
        {
            ImportacaoArquivo arquivo = Convert(iArquivo);

            using (UnitOfWork UoW = new UnitOfWork())
            {
                ImportacaoArquivo arquivoSalvo = UoW.ImportacaoArquivoRepository.Carregar(c => c.NomeArquivo == iArquivo.Nome, o => o.OrderBy(by => by.ID));

                if (arquivoSalvo == null)
                    UoW.ImportacaoArquivoRepository.Inserir(arquivo);
                else
                {
                    arquivo.ID = arquivoSalvo.ID;
                    UoW.ImportacaoArquivoRepository.Alterar(arquivo, "ID");
                }

                UoW.Save();
            }
        }


        public ImportacaoArquivo Convert(IArquivo iArquivo)
        {
            ImportacaoArquivo arquivo = new ImportacaoArquivo();
            arquivo.NomeArquivo = iArquivo.Nome;
            arquivo.TipoArquivoID = (int)iArquivo.TipoArquivo;
            arquivo.Ano = iArquivo.Ano;
            arquivo.UF = iArquivo.UF;
            Convert(arquivo, iArquivo.Registros);

            return arquivo;
        }

        public void Convert(ImportacaoArquivo arquivo, List<IArquivoItem> registros)
        {
            if (registros != null && registros.Count > 0)
            {
                switch ((Enumeradores.eTipoArquivo)arquivo.TipoArquivoID)
                {
                    case Enumeradores.eTipoArquivo.Candidatos:
                        arquivo.lstCandidatos = ConvertItemCandidato(registros);
                        break;

                    case Enumeradores.eTipoArquivo.BensDosCandidatos:
                        arquivo.lstBens = ConvertItemBens(registros);
                        break;

                    case Enumeradores.eTipoArquivo.Legendas:
                        arquivo.lstLegendas = ConvertItemLegendas(registros);
                        break;

                    case Enumeradores.eTipoArquivo.Vagas:
                        arquivo.lstVagas = ConvertItemVagas(registros);
                        break;

                    default:
                        break;
                }

            }
        }

        public List<ImportacaoCandidato> ConvertItemCandidato(List<IArquivoItem> iList)
        {
            List<ImportacaoCandidato> lstItems = new List<ImportacaoCandidato>();

            if (iList != null && iList.Count > 0)
            {
                iList.ForEach(f => lstItems.Add(f as ImportacaoCandidato));
            }

            return lstItems;
        }

        public List<ImportacaoBensCandidato> ConvertItemBens(List<IArquivoItem> iList)
        {
            List<ImportacaoBensCandidato> lstItems = new List<ImportacaoBensCandidato>();

            if (iList != null && iList.Count > 0)
            {
                iList.ForEach(f => lstItems.Add(f as ImportacaoBensCandidato));
            }

            return lstItems;
        }

        public List<ImportacaoLegenda> ConvertItemLegendas(List<IArquivoItem> iList)
        {
            List<ImportacaoLegenda> lstItems = new List<ImportacaoLegenda>();

            if (iList != null && iList.Count > 0)
            {
                iList.ForEach(f => lstItems.Add(f as ImportacaoLegenda));
            }

            return lstItems;
        }

        public List<ImportacaoVaga> ConvertItemVagas(List<IArquivoItem> iList)
        {
            List<ImportacaoVaga> lstItems = new List<ImportacaoVaga>();

            if (iList != null && iList.Count > 0)
            {
                iList.ForEach(f => lstItems.Add(f as ImportacaoVaga));
            }

            return lstItems;
        }

    }
}
