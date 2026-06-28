using System.Windows;
using System.Windows.Controls;

namespace Boss.Desktop.Views
{
    public partial class LicencasPage : Page
    {
        public LicencasPage()
        {
            InitializeComponent();
        }

        private void BtnGerarTrial_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Licença Trial gerada com sucesso!\n\nChave: TRIAL-XXXXXX\nExpira em 30 dias.", "Licença Trial", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}