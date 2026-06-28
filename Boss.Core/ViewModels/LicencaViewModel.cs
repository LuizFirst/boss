using Boss.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Boss.Core.ViewModels
{
    public partial class LicencaViewModel : ViewModelBase
    {
        public ObservableCollection<Licenca> Licencas { get; set; } = new();

        [ObservableProperty]
        private string statusTexto = "Verificando...";

        public LicencaViewModel()
        {
            CarregarLicencas();
        }

        public void CarregarLicencas()
        {
            // Aqui vamos carregar as licenças do banco
            // Por enquanto, deixar vazio até conectar ao repositório
            Licencas.Clear();
        }

        public void AdicionarLicenca(Licenca licenca)
        {
            // Método que será chamado quando uma nova licença for criada
            Licencas.Add(licenca);
            AtualizarStatus();
        }

        private void AtualizarStatus()
        {
            if (Licencas.Count > 0)
            {
                var ativa = Licencas.FirstOrDefault(l => l.Ativa);
                if (ativa != null)
                {
                    StatusTexto = $"✅ Licença Ativa - {ativa.Tipo}";
                }
            }
            else
            {
                StatusTexto = "❌ Nenhuma licença ativa";
            }
        }
    }
}
