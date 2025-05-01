using BoraMorar.Imoveis;

namespace BoraMorar.Moradias.Crawlers
{
    public class CrawledProperty
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public Incorporadora Incorporadora { get; set; }
        public decimal? Preco { get; set; }
        public int? AreaPrivativa { get; set; }
        public int? Quartos { get; set; }
        public int? Banheiros { get; set; }
        public int? VagasGaragem { get; set; }
        public string? PaginaUrl { get; set; }
        public string? BookUrl { get; set; }
        public List<string> Caracteristicas { get; set; }
        public DateTime CrawledAt { get; set; }
    }
}
