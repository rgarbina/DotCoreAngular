using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore_Angular.Entity
{
    public class Imagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Diretorio { get; set; }
        public int Tamanho { get; set; }
        public string Extensao { get; set; }
    }
}
