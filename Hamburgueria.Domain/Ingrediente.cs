
using System;

namespace Hamburgueria.Domain
{
    public class Ingrediente : EntidadeBase
    {
        public string Nome { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal EstoqueMinimo { get; set; }
    }
}
