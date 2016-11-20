using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFSCar.BD.Model
{
    [Table("Ocupacao")]
    public class Ocupacao
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Descricao")]
        public string Descricao { get; set; }

    }
}
