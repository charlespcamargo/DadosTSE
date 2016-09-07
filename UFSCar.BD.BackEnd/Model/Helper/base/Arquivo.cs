using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UFSCar.BD.Model.Enumeradores;

namespace UFSCar.BD.Model
{
    public interface IArquivo
    {
        string Nome { get; set; }
        eTipoArquivo TipoArquivo { get; set; }
        int Ano { get; set; }
        string UF { get; set; }

        List<IArquivoItem> Registros { get; set; }
    }
}
