using System.Windows;
using System.Windows.Controls;
using Boss.Core.Models;
using Boss.Desktop.ViewModels;
using Boss.Infrastructure.Repositories;

namespace Boss.Desktop.Views
{
    public partial class ProcedimentosPage : Page
    {
        private readonly ProcedimentosViewModel _viewModel;

        public ProcedimentosPage()
        {
            InitializeComponent();

            _viewModel = new ProcedimentosViewModel();
            DataContext = _viewModel;

            // Binding da lista
            dgProcedimentos.ItemsSource = _viewModel.Procedimentos;

            // Evento de pesquisa
            txtPesquisa.TextChanged += TxtPesquisa_TextChanged;
        }

        private void TxtPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.AplicarFiltro(txtPesquisa.Text);
        }

        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            var cadastroWindow = new Window
            {
                Title = "Novo Procedimento - Boss",
                Content = new CadastroProcedimentoPage(),
                Width = 1080,
                Height = 880,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.CanResizeWithGrip
            };

            cadastroWindow.ShowDialog();

            _viewModel.CarregarProcedimentos();
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CarregarProcedimentos();
            MessageBox.Show("Lista atualizada do banco!", "Boss");
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcedimentos.SelectedItem is Procedimento procedimentoSelecionado)
            {
                var janelaEdicao = new Window
                {
                    Title = "Editar Procedimento - Boss",
                    Content = new CadastroProcedimentoPage(procedimentoSelecionado),
                    Width = 1080,
                    Height = 880,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ResizeMode = ResizeMode.CanResizeWithGrip
                };

                janelaEdicao.ShowDialog();

                _viewModel.CarregarProcedimentos();
            }
            else
            {
                MessageBox.Show("Selecione um procedimento para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcedimentos.SelectedItem is Procedimento procedimentoSelecionado)
            {
                var resultado = MessageBox.Show(
                    $"Tem certeza que deseja excluir o procedimento:\n\n{procedimentoSelecionado.Marca} {procedimentoSelecionado.Modelo}?",
                    "Confirmar Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        var repo = new ProcedimentoRepository();
                        repo.Excluir(procedimentoSelecionado.Id);

                        MessageBox.Show("Procedimento excluído com sucesso!", "Sucesso");
                        _viewModel.CarregarProcedimentos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um procedimento para excluir.", "Atenção");
            }
        }
    }
}