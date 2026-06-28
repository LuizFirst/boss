using System;
using System.Windows;
using System.Windows.Controls;
using Boss.Core.Models;
using Boss.Infrastructure.Repositories;

namespace Boss.Desktop.Views
{
    public partial class CadastroProcedimentoPage : Page
    {
        private readonly Procedimento? _procedimentoParaEditar;
        private readonly bool _isEdicao;

        public CadastroProcedimentoPage(Procedimento? procedimento = null)
        {
            InitializeComponent();

            _procedimentoParaEditar = procedimento;
            _isEdicao = procedimento != null;

            if (_isEdicao && procedimento != null)
            {
                Title = "Editar Procedimento";

                cmbMarca.Text = procedimento.Marca;
                txtModelo.Text = procedimento.Modelo;
                txtNumeroModelo.Text = procedimento.NumeroModelo;
                cmbTipoBloqueio.Text = procedimento.TipoBloqueio;
                txtProcedimento.Text = procedimento.ProcedimentoTexto;
                txtObservacoes.Text = procedimento.Observacoes;
            }
            else
            {
                Title = "Novo Procedimento";
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var procedimento = _procedimentoParaEditar ?? new Procedimento();

                procedimento.Marca = cmbMarca.Text;
                procedimento.Modelo = txtModelo.Text.Trim();
                procedimento.NumeroModelo = txtNumeroModelo.Text.Trim();
                procedimento.TipoBloqueio = cmbTipoBloqueio.Text;
                procedimento.ProcedimentoTexto = txtProcedimento.Text.Trim();
                procedimento.Observacoes = txtObservacoes.Text.Trim();
                procedimento.Autor = "lbadmin";

                if (_isEdicao)
                {
                    procedimento.UltimaEdicao = DateTime.Now;
                }
                else
                {
                    procedimento.DataCadastro = DateTime.Now;
                }

                if (string.IsNullOrWhiteSpace(procedimento.Modelo) || string.IsNullOrWhiteSpace(procedimento.ProcedimentoTexto))
                {
                    MessageBox.Show("Modelo e Procedimento são obrigatórios!");
                    return;
                }

                var repo = new ProcedimentoRepository();

                if (_isEdicao)
                {
                    repo.Atualizar(procedimento);
                    MessageBox.Show("Procedimento atualizado com sucesso!");
                }
                else
                {
                    repo.Adicionar(procedimento);
                    MessageBox.Show("Procedimento cadastrado com sucesso!");
                }

                var window = Window.GetWindow(this);
                window?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }
    }
}