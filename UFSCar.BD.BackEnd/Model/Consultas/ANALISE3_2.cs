using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.BackEnd.Model
{
    public class ANALISE3_2
    {
        public int Ano { get; set; }
        public string Regiao { get; set; }
        public string SiglaEstado { get; set; }
        public string Municipio { get; set; }
        public string Partido { get; set; }
        public int QtdMasculino { get; set; }
        public int QtdFeminino { get; set; }
        public int QtdTotal { get; set; }
        public decimal PercentualFeminino { get; set; }
        public decimal PercentualMasculino { get; set; }

    }
}
