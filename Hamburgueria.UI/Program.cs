
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
                var saidaService = new SaidaProdutoService();
                List<SaidaProduto> saidas = saidaService.BuscarTodasSaidas();

                Console.WriteLine("==================================================");
                Console.WriteLine("Teste de Conexão C# <-> MySQL (SELECT Real - Saídas)");
                Console.WriteLine("==================================================");

                if (saidas.Any())
                {
                    Console.WriteLine($"SUCESSO! {saidas.Count} registro(s) de saída encontrado(s) no banco de dados:");
                    foreach (var saida in saidas)
                    {
                        Console.WriteLine($"- ID: {saida.Id}, Produto ID: {saida.IdProduto}, Qtd: {saida.QuantidadeSaida}, Data: {saida.DataHora}");
                    }
                }
                else
                {
                    Console.WriteLine("SUCESSO! Conexão estabelecida, mas nenhum registro de saída encontrado.");
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
