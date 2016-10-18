using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace UFSCar.BD.BackEnd.Model
{

    public class RegistroGraficoUm
    {
        public string NomeUrna { get; set; }

        public decimal Bens2006 { get; set; }
        public decimal Bens2008 { get; set; }
        public decimal Bens2010 { get; set; }
        public decimal Bens2012 { get; set; }
        public decimal Bens2014 { get; set; }
        public decimal Bens2016 { get; set; }
    }
}
