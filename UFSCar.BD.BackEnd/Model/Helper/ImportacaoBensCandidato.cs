using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{
    [Table("ImportacaoBensCandidato")]
    public class ImportacaoBensCandidato : IArquivoItem
    {
        public string DATA_GERACAO { get; set; }

        public string HORA_GERACAO { get; set; }

        public string ANO_ELEICAO { get; set; }

        public string DESCRICAO_ELEICAO { get; set; }

        public string SIGLA_UF { get; set; }

        public string SQ_CANDIDATO { get; set; }

        public string CD_TIPO_BEM_CANDIDATO { get; set; }

        public string DS_TIPO_BEM_CANDIDATO { get; set; }

        public string DETALHE_BEM { get; set; }

        public string VALOR_BEM { get; set; }

        public string DATA_ULTIMA_ATUALIZACAO { get; set; }

        public string HORA_ULTIMA_ATUALIZACAO { get; set; }



        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ImportacaoArquivoID")]
        public int ImportacaoArquivoID { get; set; }
    }
}
