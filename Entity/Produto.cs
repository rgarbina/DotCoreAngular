using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore_Angular.Entity
{
    public class Produto
    {
        public long Id { get; set; }
        [Required]
        public string ImagemPath { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}
