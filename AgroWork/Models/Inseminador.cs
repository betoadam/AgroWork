using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroWork.Models
{
    public class Inseminador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string Carro { get; set; }

        public ICollection<Inseminacao> Inseminacaos { get; set; }
    }
}
