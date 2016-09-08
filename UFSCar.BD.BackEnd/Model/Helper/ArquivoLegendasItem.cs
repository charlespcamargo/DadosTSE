using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{
    public class ArquivoLegendasItem : IArquivoItem
    {
        public string DATA_GERACAO { get; set; }
        public string HORA_GERACAO { get; set; }
        public string ANO_ELEICAO { get; set; }
        public string NUM_TURNO { get; set; }
        public string DESCRICAO_ELEICAO { get; set; }
        public string SIGLA_UF { get; set; }
        public string SIGLA_UE { get; set; }
        public string NOME_UE { get; set; }
        public string CODIGO_CARGO { get; set; }
        public string DESCRICAO_CARGO { get; set; }
        public string TIPO_LEGENDA { get; set; }
        public string NUM_PARTIDO { get; set; }
        public string SIGLA_PARTIDO { get; set; }
        public string NOME_PARTIDO { get; set; }
        public string SIGLA_COLIGACAO { get; set; }
        public string NOME_COLIGACAO { get; set; }
        public string COMPOSICAO_COLIGACAO { get; set; }
        public string SEQUENCIAL_COLIGACAO { get; set; }


    }
}
