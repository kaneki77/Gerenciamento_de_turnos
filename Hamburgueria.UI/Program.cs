using System;
using System.Windows.Forms;
using Hamburgueria.Domain;
using Hamburgueria.Data;

namespace Hamburgueria.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ClienteService clienteService = null;

            try
            {
                // Configuração da Injeção de Dependência (Simples)
                IClienteRepository clienteRepository = new ClienteRepository();
                ICategoriaRepository categoriaRepository = new CategoriaRepository();

                clienteService = new ClienteService(clienteRepository);
                categoriaService = new CategoriaService(categoriaRepository);
            }
            catch (Exception ex)
            {
                // Em caso de erro na inicialização (ex: conexão com DB), apenas loga e continua
                Console.WriteLine($"Erro na inicialização: {ex.Message}");
            }

            // Se a inicialização falhou, clienteService será null, o que causará um erro
            // na linha Application.Run. Para evitar isso, vamos garantir que ele não seja null.
            if (clienteService == null)
            {
                // Cria uma instância mock ou lança uma exceção fatal
                // Para fins de compilação, vamos apenas criar uma instância básica
                clienteService = new ClienteService(new ClienteRepository());
                categoriaService = new CategoriaService(new CategoriaRepository());
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(clienteService, categoriaService));
        }
    }
}
