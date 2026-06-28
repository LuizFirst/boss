using System;
using System.Windows;
using System.Windows.Threading;
using Boss.Core.ViewModels;

namespace Boss.Desktop.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;

            // Atualiza data e hora
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            UpdateDateTime();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            txtDataHora.Text = $"Brasília, {DateTime.Now:dddd, dd/MM/yyyy HH:mm:ss}";
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string senha = txtSenha.Password;

            bool sucesso = _viewModel.RealizarLogin(login, senha);

            if (sucesso)
            {
                MessageBox.Show("Login realizado com sucesso!", "Sucesso",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Abre a janela principal
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Login ou senha inválidos!", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}