
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
        public void Adicionar(T entidade)
        {
            // Simulação de operação de banco de dados
            Console.WriteLine($"Adicionando {typeof(T).Name}");
        }
    }
}

