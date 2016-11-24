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


            parametro = GenericParameter.Create("@OCUPACAO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.Ocupacao))
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
                string script = "SP_ANALISE1_1  @ANO, @TODOSANOS, @SEXO, @IDESCOLARIDADE, @OCUPACAO, @REGIAO, @SIGLAESTADO, @IDMUNICIPIO, @SIGLAPARTIDO, @IDCARGOPRETENDIDO ";

                lst = UoW.GetContext().Database.SqlQuery<ANALISE1_1>(script, lstParametros.ToArray()).ToList();
            }

            return lst;
        }

        public object EvolucaoPatrimonialGrafico(string cpf)
        {
            throw new NotImplementedException();
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


            parametro = GenericParameter.Create("@OCUPACAO", System.Data.DbType.AnsiString);
            if (!String.IsNullOrEmpty(filtro.Ocupacao))
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
                string script = "SP_ANALISE3_2  @ANO, @SEXO, @IDESCOLARIDADE, @OCUPACAO, @REGIAO, @SIGLAESTADO, @IDMUNICIPIO, @SIGLAPARTIDO, @IDCARGOPRETENDIDO ";

                lst = UoW.GetContext().Database.SqlQuery<ANALISE3_2>(script, lstParametros.ToArray()).ToList();
            }

            return lst;
        }
    }
}
