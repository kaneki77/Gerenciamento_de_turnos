
using System;

namespace Hamburgueria.Domain
{
    public class SaidaProduto : EntidadeBase
    {
        public int IdProduto { get; set; }
        public int IdUsuario { get; set; }
        public int QuantidadeSaida { get; set; }
        public DateTime DataHora { get; set; }
    }
}
