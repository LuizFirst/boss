using Boss.Core.Models;
using Boss.Core.ViewModels;
using Boss.Infrastructure.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace Boss.Desktop.ViewModels
{
    public partial class ProcedimentosViewModel : ViewModelBase
    {
        public ObservableCollection<Procedimento> Procedimentos { get; set; } = new();
        private List<Procedimento> _todosProcedimentos = new();

        private readonly ProcedimentoRepository _repository = new();

        public ProcedimentosViewModel()
        {
            CarregarProcedimentos();
        }

        public void CarregarProcedimentos()
        {
            _todosProcedimentos = _repository.ListarTodos();
            Procedimentos.Clear();

            foreach (var p in _todosProcedimentos)
            {
                Procedimentos.Add(p);
            }

            if (Procedimentos.Count == 0)
            {
                // Dados de teste se banco vazio
                Procedimentos.Add(new Procedimento
                {
                    Marca = "SAMSUNG",
                    Modelo = "A32",
                    TipoBloqueio = "Google",
                    Autor = "lbadmin"
                });
            }
        }

        public void AplicarFiltro(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                Procedimentos.Clear();
                foreach (var p in _todosProcedimentos)
                    Procedimentos.Add(p);
                return;
            }

            termo = termo.ToLower();

            var filtrados = _todosProcedimentos.Where(p =>
                p.Marca.ToLower().Contains(termo) ||
                p.Modelo.ToLower().Contains(termo) ||
                p.NumeroModelo.ToLower().Contains(termo) ||
                p.TipoBloqueio.ToLower().Contains(termo) ||
                p.ProcedimentoTexto.ToLower().Contains(termo)
            ).ToList();

            Procedimentos.Clear();
            foreach (var p in filtrados)
            {
                Procedimentos.Add(p);
            }
        }
    }
}