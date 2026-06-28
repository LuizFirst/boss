using System;
using System.Windows;
using System.Windows.Controls;
using Boss.Core.Models;
using Boss.Infrastructure.Repositories;
using System.Collections.ObjectModel;

namespace Boss.Desktop.Views
{
    public partial class LicencaPage : Page
    {
        private readonly LicencaRepository _repository = new();
        public ObservableCollection<Licenca> Licencas { get; set; } = new();

        public LicencaPage()
        {
            InitializeComponent();
            lvLicencas.ItemsSource = Licencas;
            CarregarTudo();
        }

        private void CarregarTudo()
        {
            var licenca = _repository.ObterLicencaAtiva();

            // Status
            txtStatus.Text = licenca != null && licenca.Ativa
                ? $"✅ Licença Ativa - {licenca.Tipo}"
                : "❌ Nenhuma licença ativa";

            // Lista
            Licencas.Clear();
            if (licenca != null)
            {
                Licencas.Add(licenca);
            }
            lvLicencas.Items.Refresh();
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

                if (tipoSelecionado.Contains("Mês"))
                    licenca.DataExpiracao = DateTime.Now.AddMonths(1);
                else if (tipoSelecionado.Contains("Ano"))
                    licenca.DataExpiracao = DateTime.Now.AddYears(1);

                _repository.Adicionar(licenca);

                MessageBox.Show($"Licença {tipoSelecionado} ativada com sucesso!", "Sucesso");
                txtChave.Clear();
                CarregarTudo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ativar: " + ex.Message);
            }
        }
    }
}