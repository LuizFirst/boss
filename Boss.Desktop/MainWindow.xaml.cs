using System.Windows;
using Boss.Desktop.Views;

namespace Boss.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new DashboardPage());
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardPage());
        }

        private void Procedimentos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProcedimentosPage());
        }
        private void Licencas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LicencaPage());
        }
    }
}