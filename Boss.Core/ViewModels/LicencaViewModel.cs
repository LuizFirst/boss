using Boss.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Boss.Core.ViewModels
{
    public class LicencaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Licenca> Licencas { get; set; } = new();

        private string _statusTexto = "Verificando...";
        public string StatusTexto
        {
            get => _statusTexto;
            set
            {
                if (_statusTexto != value)
                {
                    _statusTexto = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
