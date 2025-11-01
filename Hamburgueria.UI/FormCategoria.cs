using System;
using System.Windows.Forms;
using Hamburgueria.Domain;
using System.Collections.Generic;

namespace Hamburgueria.UI
{
    public partial class FormCategoria : Form
    {
        private readonly CategoriaService _categoriaService;
        private List<Categoria> _categorias;

        public FormCategoria(CategoriaService categoriaService)
        {
            InitializeComponent();
            _categoriaService = categoriaService;
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            try
            {
                _categorias = _categoriaService.GetAll();
                // Assumindo que você tem um DataGridView chamado dgvCategorias
                // dgvCategorias.DataSource = _categorias;
                MessageBox.Show("Categorias carregadas com sucesso. (Ainda sem DataGridView)");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            // Assumindo que você tem TextBox para Nome (txtNome) e Descricao (txtDescricao)
            // txtNome.Clear();
            // txtDescricao.Clear();
            // Assumindo que você tem um botão Salvar/Atualizar (btnSalvar)
            // btnSalvar.Text = "Salvar";
            // txtNome.Tag = null; // Limpa o ID para indicar novo cadastro
            MessageBox.Show("Campos limpos. (Ainda sem controles visuais)");
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Assumindo que você tem TextBox para Nome (txtNome) e Descricao (txtDescricao)
                // var nome = txtNome.Text;
                // var descricao = txtDescricao.Text;
                var nome = "Nova Categoria"; // Placeholder
                var descricao = "Descrição da Nova Categoria"; // Placeholder

                var categoria = new Categoria
                {
                    Nome = nome,
                    Descricao = descricao
                };

                // Assumindo que você tem um ID armazenado no Tag do txtNome para atualização
                // if (txtNome.Tag != null)
                // {
                //     categoria.Id = (int)txtNome.Tag;
                //     _categoriaService.Atualizar(categoria);
                //     MessageBox.Show("Categoria atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // }
                // else
                // {
                    _categoriaService.Adicionar(categoria);
                    MessageBox.Show("Categoria adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // }

                CarregarCategorias();
                LimparCampos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Erro de Negócio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar categoria: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Outros métodos como dgvCategorias_CellClick para edição e btnRemover_Click
    }
}
