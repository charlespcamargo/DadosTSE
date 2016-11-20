using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFSCar.BD.Model
{
    [Table("Estado")]
    public class Estado
    { 
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Sigla")]
        public string Sigla { get; set; }

        [DataMember]
        [Column("Nome")]
        public string Nome { get; set; }
        
        [DataMember]
        [Column("Regiao")]
        public string Regiao { get; set; }

        [DataMember]
        [Column("PaisID")]
        public int PaisID { get; set; }

        [DataMember]
        [Column("SiglaUE")]
        public string SiglaUE { get; set; }


    }
}
