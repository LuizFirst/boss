namespace Boss.Core.Models
{
    public class Procedimento
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string NumeroModelo { get; set; } = string.Empty;
        public string TipoBloqueio { get; set; } = string.Empty;
        public string ProcedimentoTexto { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? UltimaEdicao { get; set; }
        public string Autor { get; set; } = "lbadmin";
    }
}