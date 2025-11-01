using System.Collections.Generic;

namespace Hamburgueria.Domain
{
    public interface IClienteRepository
    {
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(int id);
        Cliente GetById(int id);
        List<Cliente> GetAll();
    }
}
