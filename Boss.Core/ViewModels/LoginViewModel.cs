using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Boss.Core.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? mensagemErro;

        public bool RealizarLogin(string login, string senha)
        {
            // Administrador padrão conforme especificação
            if (login == "lbadmin" && senha == "cellyadmin")
            {
                // Aqui virá autenticação real + verificação de licença
                return true;
            }

            return false;
        }
    }
}