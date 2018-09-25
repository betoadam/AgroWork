using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroWork.Models
{
    public class Inseminacao
    {
        public int Id { get; set; }
        [Display(Name = "Produtor")]
        public int ProdutorId { get; set; }
        [Display(Name = "Vaca")]
        public int VacaId { get; set; }
        [Display(Name = "Inseminador")]
        public int InseminadorId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public string Hora { get; set; }
        public string Touro { get; set; }
        public double Valor { get; set; }

        public Produtor Produtor { get; set; }
        public Vaca Vaca { get; set; }
        public Inseminador Inseminador { get; set; }

    }
}
