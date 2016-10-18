using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFSCar.BD.Model
{
    [Table("EleicaoCargo")]
    public class EleicaoCargo
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("EleicaoID")]
        public int EleicaoID { get; set; }

        [DataMember]
        [Column("CargoID")]
        public int CargoID { get; set; }

    }
}
