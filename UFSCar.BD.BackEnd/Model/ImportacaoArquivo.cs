using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{
    
    [Table("ImportacaoArquivo")]
    public class ImportacaoArquivo
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("NomeArquivo")]
        public string NomeArquivo { get; set; }

        [DataMember]
        [Column("TipoArquivoID")]
        public int TipoArquivoID { get; set; }

        [DataMember]
        [Column("Ano")]
        public int Ano { get; set; }

        [DataMember]
        [Column("UF")]
        public string UF { get; set; }

        [DataMember]
        [ForeignKey("ImportacaoArquivoID")]
        public List<ImportacaoCandidato> lstCandidatos { get; set; }

        [DataMember]
        [ForeignKey("ImportacaoArquivoID")]
        public List<ImportacaoBensCandidato> lstBens { get; set; }

        [DataMember]
        [ForeignKey("ImportacaoArquivoID")]
        public List<ImportacaoLegenda> lstLegendas { get; set; }

        [DataMember]
        [ForeignKey("ImportacaoArquivoID")]
        public List<ImportacaoVaga> lstVagas  { get; set; }

    }

}
