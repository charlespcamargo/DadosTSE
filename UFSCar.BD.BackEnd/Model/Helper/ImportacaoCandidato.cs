using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{
    [Table("ImportacaoCandidato")]
    public class ImportacaoCandidato : IArquivoItem
    {
        public string DATA_GERACAO { get; set; }
        public string HORA_GERACAO { get; set; }
        public string ANO_ELEICAO { get; set; }
        public string NUM_TURNO { get; set; }
        public string DESCRICAO_ELEICAO { get; set; }
        public string SIGLA_UF { get; set; }
        public string SIGLA_UE { get; set; }
        public string DESCRICAO_UE { get; set; }
        public string CODIGO_CARGO { get; set; }
        public string DESCRICAO_CARGO { get; set; }
        public string NOME_CANDIDATO { get; set; }
        public string SEQUENCIAL_CANDIDATO { get; set; }
        public string NUMERO_CANDIDATO { get; set; }
        public string CPF_CANDIDATO { get; set; }
        public string NOME_URNA_CANDIDATO { get; set; }
        public string COD_SITUACAO_CANDIDATURA { get; set; }
        public string DES_SITUACAO_CANDIDATURA { get; set; }
        public string NUMERO_PARTIDO { get; set; }
        public string SIGLA_PARTIDO { get; set; }
        public string NOME_PARTIDO { get; set; }
        public string CODIGO_LEGENDA { get; set; }
        public string SIGLA_LEGENDA { get; set; }
        public string COMPOSICAO_LEGENDA { get; set; }
        public string NOME_LEGENDA { get; set; }
        public string CODIGO_OCUPACAO { get; set; }
        public string DESCRICAO_OCUPACAO { get; set; }
        public string DATA_NASCIMENTO { get; set; }
        public string NUM_TITULO_ELEITORAL_CANDIDATO { get; set; }
        public string IDADE_DATA_ELEICAO { get; set; }
        public string CODIGO_SEXO { get; set; }
        public string DESCRICAO_SEXO { get; set; }
        public string COD_GRAU_INSTRUCAO { get; set; }
        public string DESCRICAO_GRAU_INSTRUCAO { get; set; }
        public string CODIGO_ESTADO_CIVIL { get; set; }
        public string DESCRICAO_ESTADO_CIVIL { get; set; }
        public string CODIGO_COR_RACA { get; set; }
        public string DESCRICAO_COR_RACA { get; set; }
        public string CODIGO_NACIONALIDADE { get; set; }
        public string DESCRICAO_NACIONALIDADE { get; set; }
        public string SIGLA_UF_NASCIMENTO { get; set; }
        public string CODIGO_MUNICIPIO_NASCIMENTO { get; set; }
        public string NOME_MUNICIPIO_NASCIMENTO { get; set; }
        public string DESPESA_MAX_CAMPANHA { get; set; }
        public string COD_SIT_TOT_TURNO { get; set; }
        public string DESC_SIT_TOT_TURNO { get; set; }
        public string NM_EMAIL { get; set; }



        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ImportacaoArquivoID")]
        public int ImportacaoArquivoID { get; set; }

    }
}
