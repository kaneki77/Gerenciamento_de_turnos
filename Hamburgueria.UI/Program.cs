
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hamburgueria.Domain;
using Hamburgueria.Data; // Necessário para acessar a camada de dados

namespace Hamburgueria.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                // Teste de conexão e SELECT real (Critério de Aceitação D15)
                var clienteService = new ClienteService();
                List<Cliente> clientes = clienteService.BuscarTodos();

                Console.WriteLine("==================================================");
                Console.WriteLine("Teste de Conexão C# <-> MySQL (SELECT Real)");
                Console.WriteLine("==================================================");

                if (clientes.Any())
                {
                    Console.WriteLine($"SUCESSO! {clientes.Count} cliente(s) encontrado(s) no banco de dados:");
                    foreach (var cliente in clientes)
                    {
                        Console.WriteLine($"- ID: {cliente.Id}, Nome: {cliente.Nome}, Telefone: {cliente.Telefone}");
                    }
                }
                else
                {
                    Console.WriteLine("SUCESSO! Conexão estabelecida, mas nenhum cliente encontrado.");
                }
                Console.WriteLine("==================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("FALHA NO TESTE DE CONEXÃO/SELECT!");
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine("\nVerifique se o MySQL está rodando e se a ConnectionString em Hamburgueria.Data/DbConnection.cs está correta.");
                Console.WriteLine("==================================================");
            }

            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());
        }
    }
}

