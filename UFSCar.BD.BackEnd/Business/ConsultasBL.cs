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


        public List<PatrimonioResult> EvolucaoPatrimonial(PatrimonioResult filtro)
        {
            List<PatrimonioResult> lst = null;

            using (UnitOfWork UoW = new UnitOfWork())
            {
                DbParameter parametro = GenericParameter.Create("@SIGLA_UE", System.Data.DbType.AnsiString);
                parametro.Value = "72435";

                List<DbParameter> lstParametros = new List<DbParameter>();
                lstParametros.Add(parametro);


                lst = UoW.GetContext().Database.SqlQuery<PatrimonioResult>("spListarBensPrefeitos @SIGLA_UE", lstParametros.ToArray()).ToList();
            }

            return lst;
        }

    }
}
