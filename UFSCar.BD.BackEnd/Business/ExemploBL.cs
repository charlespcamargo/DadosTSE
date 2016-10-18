using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UFSCar.BD.BackEnd.Model;
using UFSCar.BD.Model;
using UFSCar.BD.Repository;

namespace UFSCar.BD.BackEnd.Business
{
    public class ExemploBL
    {
        public static ExemploBL New
        {
            get { return new ExemploBL(); }
        }

        public List<RegistroGraficoUm> Exemplificar()
        {
            using (UnitOfWork UoW = new UnitOfWork())
            {
                DbParameter parametro = GenericParameter.Create("@SIGLA_UE", System.Data.DbType.AnsiString);
                parametro.Value = "72435";

                List<DbParameter> lstParametros = new List<DbParameter>();
                lstParametros.Add(parametro);


                List<RegistroGraficoUm> lst = UoW.GetContext().Database.SqlQuery<RegistroGraficoUm>("spListarBensPrefeitos @SIGLA_UE", lstParametros.ToArray()).ToList();

                return lst;
            }

        }
    }
}
