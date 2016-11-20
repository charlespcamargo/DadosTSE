using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.BackEnd.Model
{
    public class PatrimonioResult
    {
        public int Ano { get; set; }

        public string Sexo { get; set; }

        public int EscolaridadeID { get; set; }

        public int Ocupacao { get; set; }

        public string Regiao { get; set; }

        public int EstadoID { get; set; }

        public int MunicipioID { get; set; }
        
    }
}
