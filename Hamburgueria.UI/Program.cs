
using System;


using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



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
                IClienteRepository clienteRepository = new ClienteRepository();
                ClienteService clienteService = new ClienteService(clienteRepository);
                

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(clienteService));
        }
    }
}

