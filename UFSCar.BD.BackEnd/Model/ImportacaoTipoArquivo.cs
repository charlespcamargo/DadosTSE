using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Model
{

    [Table("ImportacaoTipoArquivo")]
    public class ImportacaoTipoArquivo
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Nome")]
        public string Nome { get; set; }

    }

}
