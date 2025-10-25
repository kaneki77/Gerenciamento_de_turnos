
using System.Collections.Generic;
using Hamburgueria.Data;
using System;

namespace Hamburgueria.Domain
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService()
        {
            _clienteRepository = new ClienteRepository();
        }

        // Regra de Negócio: Validação de campos obrigatórios (Nome e Telefone)
        public void Adicionar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new ArgumentException("O nome do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(cliente.Telefone))
            {
                throw new ArgumentException("O telefone do cliente é obrigatório.");
            }
            
            // Aqui poderiam vir outras validações, como formato de telefone e email

            _clienteRepository.Adicionar(cliente);
        }

        public List<Cliente> BuscarTodos()
        {
            return _clienteRepository.GetAll();
        }

        public void Atualizar(Cliente cliente)
        {
            if (cliente.Id <= 0)
            {
                throw new ArgumentException("ID do cliente inválido para atualização.");
            }
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new ArgumentException("O nome do cliente é obrigatório.");
            }
            // ... outras validações ...

            _clienteRepository.Atualizar(cliente);
        }

        public void Remover(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID do cliente inválido para remoção.");
            }
            _clienteRepository.Remover(id);
        }
    }
}

