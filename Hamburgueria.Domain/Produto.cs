
using System;

namespace Hamburgueria.Domain
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
