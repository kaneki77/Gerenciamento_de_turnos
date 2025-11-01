using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class Form1 : Form
    {
        private readonly ClienteService _clienteService;

        public Form1(ClienteService clienteService)
        {
            InitializeComponent();
            _clienteService = clienteService;
            this.Text = "Cadastro de Clientes";
        }
    }
}
