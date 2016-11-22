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


        public List<ANALISE1_1> EvolucaoPatrimonial(AnaliseFiltro filtro)
        {
            List<ANALISE1_1> lst = null;

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


                ANALISE1_1 entidade = null;

                foreach (ANALISE1_1 unico in candidatos)
                {
                    entidade = lst.FirstOrDefault(w => w.CPF == unico.CPF);
                    entidade.lstVlrMedioOcupacao = new decimal[6];
                    entidade.lstVlrTotalDeclarado = new decimal[6];
                    Preencher(lst, entidade, unico, 2006, 0);
                    Preencher(lst, entidade, unico, 2008, 1);
                    Preencher(lst, entidade, unico, 2010, 2);
                    Preencher(lst, entidade, unico, 2012, 3);
                    Preencher(lst, entidade, unico, 2014, 4);
                    Preencher(lst, entidade, unico, 2016, 5);

                    lstRelatorio.Add(entidade);
                }
            }

            return lstRelatorio;
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
