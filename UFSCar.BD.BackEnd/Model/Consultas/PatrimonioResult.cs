using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.BackEnd.Model
{
    public class AnaliseFiltro
    {
        public int Ano { get; set; }

        public bool? TodosAnos { get; set; }

        public string Sexo { get; set; }

        public int EscolaridadeID { get; set; }

        public int Ocupacao { get; set; }

        public string Regiao { get; set; }

        public string EstadoSigla { get; set; }

        public int MunicipioID { get; set; }

        public string PartidoSigla { get; set; }

        public int CargoPretendidoID { get; set; }

    }
}
