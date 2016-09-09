using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{
    [Table("ImportacaoVaga")]
    public class ImportacaoVaga : IArquivoItem
    {
        public string DATA_GERACAO { get; set; }
        public string HORA_GERACAO { get; set; }
        public string ANO_ELEICAO { get; set; }
        public string DESCRICAO_ELEICAO { get; set; }
        public string SIGLA_UF { get; set; }
        public string SIGLA_UE { get; set; }
        public string NOME_UE { get; set; }
        public string CODIGO_CARGO { get; set; }
        public string DESCRICAO_CARGO { get; set; }
        public string QTDE_VAGAS { get; set; }


        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ImportacaoArquivoID")]
        public int ImportacaoArquivoID { get; set; }
    }
}
