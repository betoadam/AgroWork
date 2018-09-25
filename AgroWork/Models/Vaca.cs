using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroWork.Models
{
    public class Vaca
    {
        public int Id { get; set; }
        [Display(Name = "Raça")]
        public string Raca { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Tipo { get; set; }
        [Display(Name ="Produtor")]
        public int  ProdutorId { get; set; }

        public Produtor Produtor { get; set; }

        public ICollection<Inseminacao> Inseminacaos { get; set; }
    }
}
