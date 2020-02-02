using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore_Angular.Entity
{
    public class Produto
    {
        public long Id { get; set; }
        public long ImagemId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Imagem Imagem { get; set; }
    }
}
