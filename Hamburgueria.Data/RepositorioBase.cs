
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgueria.Data
{
    // Classe base vazia para simular a camada de dados
    public abstract class RepositorioBase<T> where T : class
    {
        // Método base para simular o CRUD - Adicionar
        public void Adicionar(T entidade)
        {
            // A implementação real será feita nos repositórios específicos
            Console.WriteLine($"Simulação: Adicionando {typeof(T).Name}");
        }

        // Método base para simular o CRUD - Atualizar
        public void Atualizar(T entidade)
        {
            Console.WriteLine($"Simulação: Atualizando {typeof(T).Name}");
        }

        // Método base para simular o CRUD - Remover
        public void Remover(int id)
        {
            Console.WriteLine($"Simulação: Removendo ID: {id}");
        }
    }
}

