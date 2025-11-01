using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class Form1 : Form
    {
        private readonly ClienteService _clienteService;
        private readonly CategoriaService _categoriaService;

        public Form1(ClienteService clienteService, CategoriaService categoriaService)
        {
            InitializeComponent();
            _clienteService = clienteService;
            _categoriaService = categoriaService;
            this.Text = "Menu Principal";
        }

        private void btnCategoria_Click(object sender, System.EventArgs e)
        {
            var formCategoria = new FormCategoria(_categoriaService);
            formCategoria.ShowDialog();
        }
    }
}
