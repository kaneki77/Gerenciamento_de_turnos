
using System.Collections.Generic;
using Hamburgueria.Data;

namespace Hamburgueria.Domain
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService()
        {
            _clienteRepository = new ClienteRepository();
        }

        public List<Cliente> BuscarTodos()
        {
            // Aqui estaria a lógica de negócio, mas por enquanto apenas repassa a chamada
            return _clienteRepository.GetAll();
        }
    }
}

