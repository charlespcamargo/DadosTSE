using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{
    public class Enumeradores
    {
        public enum eTipoArquivo
        {
            Desconhecido = -1,
            Candidatos = 1,
            BensDosCandidatos = 2,
            Legendas = 3,
            Vagas = 4,
            Todos = 999
        }
    }
}
