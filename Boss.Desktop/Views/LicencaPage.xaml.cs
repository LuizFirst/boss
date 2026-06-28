using System;
using System.Windows;
using System.Windows.Controls;
using Boss.Core.Models;
using Boss.Core.ViewModels;
using Boss.Infrastructure.Repositories;

namespace Boss.Desktop.Views
{
    public partial class LicencaPage : Page
    {
        private readonly LicencaRepository _repository = new();
        private readonly LicencaViewModel _viewModel;

        public LicencaPage()
        {
            InitializeComponent();
            
            // Criar e definir o ViewModel
            _viewModel = new LicencaViewModel();
            DataContext = _viewModel;
            
            // Carregar licenças do banco
            CarregarLicencasDoRepositorio();
        }

        private void CarregarLicencasDoRepositorio()
        {
            try
            {
                var licenca = _repository.ObterLicencaAtiva();

                _viewModel.Licencas.Clear();
                
                if (licenca != null)
                {
                    _viewModel.Licencas.Add(licenca);
                    _viewModel.StatusTexto = $"✅ Licença Ativa - {licenca.Tipo}";
                }
                else
                {
                    _viewModel.StatusTexto = "❌ Nenhuma licença ativa";
                }
            }
            catch (Exception ex)
            {
                _viewModel.StatusTexto = "❌ Erro ao carregar licenças";
                MessageBox.Show("Erro ao carregar licenças: " + ex.Message);
            }
        }

        private void BtnAtivar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChave.Text))
            {
                MessageBox.Show("Digite uma chave de ativação.", "Atenção");
                return;
            }

            try
            {
                var tipoSelecionado = (cmbTipo.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Vitalicia";

                var licenca = new Licenca
                {
                    Chave = txtChave.Text.Trim(),
                    Tipo = tipoSelecionado,
                    Ativa = true,
                    DataAtivacao = DateTime.Now
                };

                // Calcular data de expiração conforme o tipo
                if (tipoSelecionado == "1 Mês")
                    licenca.DataExpiracao = DateTime.Now.AddMonths(1);
                else if (tipoSelecionado == "3 Meses")
                    licenca.DataExpiracao = DateTime.Now.AddMonths(3);
                else if (tipoSelecionado == "6 Meses")
                    licenca.DataExpiracao = DateTime.Now.AddMonths(6);
                else if (tipoSelecionado == "1 Ano")
                    licenca.DataExpiracao = DateTime.Now.AddYears(1);

                // Salvar no banco
                _repository.Adicionar(licenca);

                // ✅ ADICIONAR À COLEÇÃO (vai atualizar a UI automaticamente)
                _viewModel.Licencas.Add(licenca);
                _viewModel.StatusTexto = $"✅ Licença Ativa - {licenca.Tipo}";

                MessageBox.Show($"Licença {tipoSelecionado} ativada com sucesso!", "Sucesso");
                txtChave.Clear();
                cmbTipo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ativar: " + ex.Message);
            }
        }
    }
}
