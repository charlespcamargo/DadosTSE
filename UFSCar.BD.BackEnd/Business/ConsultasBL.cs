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
                string script = "SP_ANALISE1_1  @ANO, @SEXO, @IDESCOLARIDADE, @IDOCUPACAO, @REGIAO, @SIGLAESTADO, @IDMUNICIPIO, @SIGLAPARTIDO, @IDCARGOPRETENDIDO ";

                lst = UoW.GetContext().Database.SqlQuery<ANALISE1_1>(script, lstParametros.ToArray()).ToList();
            }


            List<ANALISE1_1> lstRelatorio = new List<ANALISE1_1>();

            if (lst != null && lst.Count > 0)
            {
                List<ANALISE1_1> candidatos = lst.GroupBy(group => new { group.SiglaEstado, group.Municipio, group.Nome }).
                                                         Select(unico => new ANALISE1_1()
                                                         {
                                                             SiglaEstado = unico.Key.SiglaEstado,
                                                             Municipio = unico.Key.Municipio,
                                                             Nome = unico.Key.Nome
                                                         }).ToList();

                List<ANALISE1_1> lstEleicoesDoCandidato = null;
                ANALISE1_1 entidade = null;

                foreach (ANALISE1_1 unico in candidatos)
                {
                    lstEleicoesDoCandidato = lst.Where(w => w.SiglaEstado == unico.SiglaEstado && w.Municipio == unico.Municipio && w.Nome == unico.Nome).ToList();

                    entidade = lstEleicoesDoCandidato.FirstOrDefault();
                    entidade.lstVlrMedioOcupacao = new decimal[lstEleicoesDoCandidato.Count];
                    entidade.lstVlrTotalDeclarado = new decimal[lstEleicoesDoCandidato.Count];

                    for (int i = 0; i < lstEleicoesDoCandidato.Count; i++)
                    {
                        entidade.lstVlrMedioOcupacao[i] = lstEleicoesDoCandidato[i].VlrMedioOcupacao;
                        entidade.lstVlrTotalDeclarado[i] = lstEleicoesDoCandidato[i].VlrTotalDeclarado;
                    }

                    lstRelatorio.Add(entidade);
                }
            }

            return lstRelatorio;
        }

    }
}
