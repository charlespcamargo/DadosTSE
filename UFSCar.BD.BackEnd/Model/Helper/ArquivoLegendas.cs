using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UFSCar.BD.Model.Enumeradores;

namespace UFSCar.BD.Model
{
    public class ArquivoLegendas : IArquivo
    {
        public int Ano { get; set; }

        public string Nome { get; set; }

        public eTipoArquivo TipoArquivo { get; set; }

        public string UF { get; set; }

        List<IArquivoItem> IArquivo.Registros { get; set; }
    }
}
