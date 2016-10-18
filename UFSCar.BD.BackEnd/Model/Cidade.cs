using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFSCar.BD.Model
{
    [Table("Cidade")]
    public class Cidade
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }
        
        [DataMember]
        [Column("Nome")]
        public string Nome { get; set; }

        [DataMember]
        [Column("EstadoID")]
        public int EstadoID { get; set; }


    }
}
