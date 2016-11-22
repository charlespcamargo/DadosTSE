using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFSCar.BD.Commom.Conversores;

namespace UFSCar.BD.BackEnd.Model
{
    public class ANALISE1_1
    {
        public int Ano { get; set; }

        public string Regiao { get; set; }

        public string SiglaEstado { get; set; }

        public string Municipio { get; set; }

        public string Nome { get; set; }

        public string Ocupacao { get; set; }

        [JsonConverter(typeof(CustomMoneyReal))]
        public decimal VlrMedioOcupacao { get; set; }

        [JsonConverter(typeof(CustomMoneyReal))]
        public decimal VlrTotalDeclarado { get; set; }

        [JsonConverter(typeof(CustomMoneyReal))]
        public decimal DiferencaMedia { get; set; }

        public string CargoPolitico { get; set; }

        public string Partido { get; set; }

        public string CPF { get; set; }
         

    }
     
}
