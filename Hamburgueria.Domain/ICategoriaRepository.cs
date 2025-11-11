using System.Collections.Generic;

namespace Hamburgueria.Domain
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Remover(int id);
        Categoria GetById(int id);
        List<Categoria> GetAll();
    }
}
