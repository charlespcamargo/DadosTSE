using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFSCar.BD.BackEnd.Model;
using UFSCar.BD.Repository;

namespace UFSCar.BD.BackEnd.Business
{
    public class ConsultasBL
    {
        public static ConsultasBL New
        {
            get { return new ConsultasBL(); }
        }


        public ANALISE1_1_RELATORIO EvolucaoPatrimonial(AnaliseFiltro filtro)
        {
            List<ANALISE1_1> lst = null;
            ANALISE1_1_RELATORIO relatorio = new ANALISE1_1_RELATORIO();

            #region Parametros

            List<DbParameter> lstParametros = new List<DbParameter>();
            DbParameter parametro = null;

            parametro = GenericParameter.Create("@ANO", System.Data.DbType.Int32);
            if (filtro.Ano > 0)
                parametro.Value = filtro.Ano;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@TODOSANOS", System.Data.DbType.Boolean);
            if (filtro.TodosAnos.HasValue)
                parametro.Value = filtro.TodosAnos;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@SEXO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.Sexo))
                parametro.Value = filtro.Sexo;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@IDESCOLARIDADE", System.Data.DbType.Int32);
            if (filtro.EscolaridadeID > 0)
                parametro.Value = filtro.EscolaridadeID;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);


            parametro = GenericParameter.Create("@IDOCUPACAO", System.Data.DbType.Int32);
            if (filtro.Ocupacao > 0)
                parametro.Value = filtro.Ocupacao;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@REGIAO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.Regiao))
                parametro.Value = filtro.Regiao;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@SIGLAESTADO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.EstadoSigla))
                parametro.Value = filtro.EstadoSigla;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);


            parametro = GenericParameter.Create("@IDMUNICIPIO", System.Data.DbType.Int32);
            if (filtro.MunicipioID > 0)
                parametro.Value = filtro.MunicipioID;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@SIGLAPARTIDO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.PartidoSigla))
                parametro.Value = filtro.PartidoSigla;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@IDCARGOPRETENDIDO", System.Data.DbType.Int32);
            if (filtro.CargoPretendidoID > 0)
                parametro.Value = filtro.CargoPretendidoID;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            #endregion Parametros

            using (UnitOfWork UoW = new UnitOfWork())
            {
                string script = "SP_ANALISE1_1  @ANO, @TODOSANOS, @SEXO, @IDESCOLARIDADE, @IDOCUPACAO, @REGIAO, @SIGLAESTADO, @IDMUNICIPIO, @SIGLAPARTIDO, @IDCARGOPRETENDIDO ";

                lst = UoW.GetContext().Database.SqlQuery<ANALISE1_1>(script, lstParametros.ToArray()).ToList();
            }


            List<ANALISE1_1> lstRelatorio = new List<ANALISE1_1>();

            if (lst != null && lst.Count > 0)
            {
                List<ANALISE1_1> candidatos = lst.GroupBy(group => new { group.CPF }).
                                                         Select(unico => new ANALISE1_1()
                                                         {
                                                             CPF = unico.Key.CPF
                                                         }).ToList();



                Model.ANALISE1_1_LINHA linha = new Model.ANALISE1_1_LINHA();
                linha.lstColuna.Add(new ANALISE1_1_COLUNA("ANO"));

                foreach (ANALISE1_1 unico in candidatos)
                {
                    PreencherNomeOcupacao(linha, lst, unico, filtro.Ano);
                }
                relatorio.lstLinha.Add(linha);

                List<int> lstAnos = new List<int>();
                lstAnos.Add(2006);
                lstAnos.Add(2008);
                lstAnos.Add(2010);
                lstAnos.Add(2012);
                lstAnos.Add(2014);
                lstAnos.Add(2016);

                foreach (int ano in lstAnos)
                {
                    linha = new Model.ANALISE1_1_LINHA();
                    linha.lstColuna.Add(new ANALISE1_1_COLUNA(ano.ToString()));

                    foreach (ANALISE1_1 unico in candidatos)
                    {
                        Preencher(linha, lst, unico, ano);
                    }

                    relatorio.lstLinha.Add(linha);
                }


            }

            /*
                [
                    ['Ano', 'C1', 'C2', 'C3', 'C1M', 'c2M', 'c3M'],
                    ['2010', 8175000, 8008000, 8008000, 4000000, 3000000, 2000000],
                    ['2011', 3792000, 3694000, 3694000, 4000000, 3000000, 2000000],
                    ['2012', 2695000, 2896000, 2896000, 4000000, 3000000, 2000000],
                    ['2014', 2099000, 1953000, 1953000, 4000000, 3000000, 2000000]
                ]
            */

            return relatorio;
        }

        public void Preencher(Model.ANALISE1_1_LINHA linha, List<ANALISE1_1> lst, ANALISE1_1 unico, int ano)
        {
            ANALISE1_1 encontrarAno = lst.FirstOrDefault(w => w.CPF == unico.CPF && w.Ano == ano);
            if (encontrarAno != null)
            {
                linha.lstColuna.Add(new ANALISE1_1_COLUNA(encontrarAno.VlrTotalDeclarado.ToString()));
                linha.lstColuna.Add(new ANALISE1_1_COLUNA(encontrarAno.VlrMedioOcupacao.ToString()));
            }
            else
            {
                linha.lstColuna.Add(new ANALISE1_1_COLUNA("0"));
                linha.lstColuna.Add(new ANALISE1_1_COLUNA("0"));
            }
        }

        public void PreencherNomeOcupacao(Model.ANALISE1_1_LINHA linha, List<ANALISE1_1> lst, ANALISE1_1 unico, int ano)
        {
            ANALISE1_1 encontrarAno = lst.FirstOrDefault(w => w.CPF == unico.CPF && w.Ano == ano);
            if (encontrarAno != null)
            {
                linha.lstColuna.Add(new ANALISE1_1_COLUNA(encontrarAno.Nome));
                linha.lstColuna.Add(new ANALISE1_1_COLUNA(encontrarAno.Ocupacao));
            }
        }

        public void Preencher(List<ANALISE1_1> lst, ANALISE1_1 entidade, ANALISE1_1 unico, int ano, int i)
        {
            ANALISE1_1 encontrarAno = lst.FirstOrDefault(w => w.CPF == unico.CPF && w.Ano == ano);
            if (encontrarAno != null)
            {
                entidade.lstVlrMedioOcupacao[i] = encontrarAno.VlrMedioOcupacao;
                entidade.lstVlrTotalDeclarado[i] = encontrarAno.VlrTotalDeclarado;
            }
        }


        public List<ANALISE3_2> PorSexo(AnaliseFiltro filtro)
        {
            List<ANALISE3_2> lst = null;

            #region Parametros

            List<DbParameter> lstParametros = new List<DbParameter>();
            DbParameter parametro = null;

            parametro = GenericParameter.Create("@ANO", System.Data.DbType.Int32);
            if (filtro.Ano > 0)
                parametro.Value = filtro.Ano;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@SEXO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.Sexo))
                parametro.Value = filtro.Sexo;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@IDESCOLARIDADE", System.Data.DbType.Int32);
            if (filtro.EscolaridadeID > 0)
                parametro.Value = filtro.EscolaridadeID;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);


            parametro = GenericParameter.Create("@IDOCUPACAO", System.Data.DbType.Int32);
            if (filtro.Ocupacao > 0)
                parametro.Value = filtro.Ocupacao;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@REGIAO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.Regiao))
                parametro.Value = filtro.Regiao;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@SIGLAESTADO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.EstadoSigla))
                parametro.Value = filtro.EstadoSigla;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);


            parametro = GenericParameter.Create("@IDMUNICIPIO", System.Data.DbType.Int32);
            if (filtro.MunicipioID > 0)
                parametro.Value = filtro.MunicipioID;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@SIGLAPARTIDO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.PartidoSigla))
                parametro.Value = filtro.PartidoSigla;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            parametro = GenericParameter.Create("@IDCARGOPRETENDIDO", System.Data.DbType.Int32);
            if (filtro.CargoPretendidoID > 0)
                parametro.Value = filtro.CargoPretendidoID;
            else
                parametro.Value = DBNull.Value;
            lstParametros.Add(parametro);

            #endregion Parametros

            using (UnitOfWork UoW = new UnitOfWork())
            {
                string script = "SP_ANALISE3_2  @ANO, @SEXO, @IDESCOLARIDADE, @IDOCUPACAO, @REGIAO, @SIGLAESTADO, @IDMUNICIPIO, @SIGLAPARTIDO, @IDCARGOPRETENDIDO ";

                lst = UoW.GetContext().Database.SqlQuery<ANALISE3_2>(script, lstParametros.ToArray()).ToList();
            }

            return lst;
        }
    }
}
