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
                UoW.ImportacaoArquivoRepository.Inserir(arquivo);
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
            arquivo.lstCandidatos = ConvertItem(iArquivo.Registros);

            return arquivo;
        }

        public List<ImportacaoCandidato> ConvertItem(List<IArquivoItem> iList)
        {
            List<ImportacaoCandidato> lstCandidatos = new List<ImportacaoCandidato>();

            if (iList != null && iList.Count > 0)
            {
                ImportacaoCandidato candidato = null;

                foreach (ArquivoCandidatosItem item in iList)
                {
                    candidato = new Model.ImportacaoCandidato();
                    //candidato.ImportacaoArquivoID = importacaoArquivoID;
                    candidato.DATA_GERACAO = item.DATA_GERACAO;
                    candidato.HORA_GERACAO = item.HORA_GERACAO;
                    candidato.ANO_ELEICAO = item.ANO_ELEICAO;
                    candidato.NUM_TURNO = item.NUM_TURNO;
                    candidato.DESCRICAO_ELEICAO = item.DESCRICAO_ELEICAO;
                    candidato.SIGLA_UF = item.SIGLA_UF;
                    candidato.SIGLA_UE = item.SIGLA_UE;
                    candidato.DESCRICAO_UE = item.DESCRICAO_UE;
                    candidato.CODIGO_CARGO = item.CODIGO_CARGO;
                    candidato.DESCRICAO_CARGO = item.DESCRICAO_CARGO;
                    candidato.NOME_CANDIDATO = item.NOME_CANDIDATO;
                    candidato.SEQUENCIAL_CANDIDATO = item.SEQUENCIAL_CANDIDATO;
                    candidato.NUMERO_CANDIDATO = item.NUMERO_CANDIDATO;
                    candidato.CPF_CANDIDATO = item.CPF_CANDIDATO;
                    candidato.NOME_URNA_CANDIDATO = item.NOME_URNA_CANDIDATO;
                    candidato.COD_SITUACAO_CANDIDATURA = item.COD_SITUACAO_CANDIDATURA;
                    candidato.DES_SITUACAO_CANDIDATURA = item.DES_SITUACAO_CANDIDATURA;
                    candidato.NUMERO_PARTIDO = item.NUMERO_PARTIDO;
                    candidato.SIGLA_PARTIDO = item.SIGLA_PARTIDO;
                    candidato.NOME_PARTIDO = item.NOME_PARTIDO;
                    candidato.CODIGO_LEGENDA = item.CODIGO_LEGENDA; ;
                    candidato.SIGLA_LEGENDA = item.SIGLA_LEGENDA;
                    candidato.COMPOSICAO_LEGENDA = item.COMPOSICAO_LEGENDA;
                    candidato.NOME_LEGENDA = item.NOME_LEGENDA;
                    candidato.CODIGO_OCUPACAO = item.CODIGO_OCUPACAO;
                    candidato.DESCRICAO_OCUPACAO = item.DESCRICAO_OCUPACAO;
                    candidato.DATA_NASCIMENTO = item.DATA_NASCIMENTO;
                    candidato.NUM_TITULO_ELEITORAL_CANDIDATO = item.NUM_TITULO_ELEITORAL_CANDIDATO;
                    candidato.IDADE_DATA_ELEICAO = item.IDADE_DATA_ELEICAO;
                    candidato.CODIGO_SEXO = item.CODIGO_SEXO;
                    candidato.DESCRICAO_SEXO = item.DESCRICAO_SEXO;
                    candidato.COD_GRAU_INSTRUCAO = item.COD_GRAU_INSTRUCAO;
                    candidato.DESCRICAO_GRAU_INSTRUCAO = item.DESCRICAO_GRAU_INSTRUCAO;
                    candidato.CODIGO_ESTADO_CIVIL = item.CODIGO_ESTADO_CIVIL;
                    candidato.DESCRICAO_ESTADO_CIVIL = item.DESCRICAO_ESTADO_CIVIL;
                    candidato.CODIGO_COR_RACA = item.CODIGO_COR_RACA;
                    candidato.DESCRICAO_COR_RACA = item.DESCRICAO_COR_RACA;
                    candidato.CODIGO_NACIONALIDADE = item.CODIGO_NACIONALIDADE;
                    candidato.DESCRICAO_NACIONALIDADE = item.DESCRICAO_NACIONALIDADE;
                    candidato.SIGLA_UF_NASCIMENTO = item.SIGLA_UF_NASCIMENTO;
                    candidato.CODIGO_MUNICIPIO_NASCIMENTO = item.CODIGO_MUNICIPIO_NASCIMENTO;
                    candidato.NOME_MUNICIPIO_NASCIMENTO = candidato.NOME_MUNICIPIO_NASCIMENTO;
                    candidato.DESPESA_MAX_CAMPANHA = candidato.DESPESA_MAX_CAMPANHA;
                    candidato.COD_SIT_TOT_TURNO = item.COD_SIT_TOT_TURNO;
                    candidato.DESC_SIT_TOT_TURNO = item.DESC_SIT_TOT_TURNO;
                    candidato.NM_EMAIL = candidato.NM_EMAIL;

                    lstCandidatos.Add(candidato);
                }
            }

            return lstCandidatos;
        }

    }
}
