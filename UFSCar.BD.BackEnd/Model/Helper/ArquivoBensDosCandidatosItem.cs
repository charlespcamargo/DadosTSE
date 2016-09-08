using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace UFSCar.BD.Model
{
    public class ArquivoBensDosCandidatosItem : IArquivoItem
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

    }
}
