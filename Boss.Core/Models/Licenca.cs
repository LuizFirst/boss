namespace Boss.Core.Models
{
    public class Licenca
    {
        public int Id { get; set; }
        public string Chave { get; set; } = string.Empty;
        public DateTime DataAtivacao { get; set; } = DateTime.Now;
        public DateTime? DataExpiracao { get; set; }
        public bool Ativa { get; set; } = true;
        public string Tipo { get; set; } = "Vitalicia"; // Vitalicia, Mensal, Anual
    }
}