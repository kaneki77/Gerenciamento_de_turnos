
using System.Collections.Generic;
using Hamburgueria.Data;
using System;

namespace Hamburgueria.Domain
{
    public class SaidaProdutoService
    {
        private readonly SaidaProdutoRepository _saidaRepository;

        public SaidaProdutoService()
        {
            _saidaRepository = new SaidaProdutoRepository();
        }

        // Regra de Negócio: Validação de campos obrigatórios (Produto e Quantidade)
        public void RegistrarSaida(SaidaProduto saida)
        {
            if (saida.IdProduto <= 0)
            {
                throw new ArgumentException("O ID do Produto é obrigatório.");
            }
            if (saida.QuantidadeSaida <= 0)
            {
                throw new ArgumentException("A quantidade de saída deve ser maior que zero.");
            }
            
            // Aqui poderia vir a lógica de verificação de estoque antes da baixa (Regra de Negócio)
            // Mas a baixa automática será feita via Trigger no banco de dados (Fase 4)

            _saidaRepository.Adicionar(saida);
        }

        public List<SaidaProduto> BuscarTodasSaidas()
        {
            return _saidaRepository.GetAll();
        }
    }
}
