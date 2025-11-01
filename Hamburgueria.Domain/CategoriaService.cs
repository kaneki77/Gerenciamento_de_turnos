using System;
using System.Collections.Generic;
using System.Linq;

namespace Hamburgueria.Domain
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // Regra de Negócio: Validação de campos obrigatórios (Nome)
        public void Adicionar(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            // Regra de Negócio: Não permitir categorias com o mesmo nome
            if (_categoriaRepository.GetAll().Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A categoria '{categoria.Nome}' já existe.");
            }

            _categoriaRepository.Adicionar(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            // Regra de Negócio: Não permitir categorias com o mesmo nome (exceto a própria)
            if (_categoriaRepository.GetAll().Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase) && c.Id != categoria.Id))
            {
                throw new InvalidOperationException($"A categoria '{categoria.Nome}' já existe.");
            }

            _categoriaRepository.Atualizar(categoria);
        }

        public void Remover(int id)
        {
            // Regra de Negócio: Poderia haver uma verificação se a categoria está em uso por algum produto
            // Por enquanto, apenas remove.
            _categoriaRepository.Remover(id);
        }

        public Categoria GetById(int id)
        {
            return _categoriaRepository.GetById(id);
        }

        public List<Categoria> GetAll()
        {
            return _categoriaRepository.GetAll();
        }
    }
}
