using BoraMorar.Imoveis;

namespace BoraMorar.Moradias.Crawlers
{
    public static class CrawledPropertyExtensions
    {
        public static List<Imovel> ToImoveis(this IEnumerable<CrawledProperty> propriedades)
        {
            return propriedades.Select(p => new Imovel(
                nome: p.Nome,
                endereco: p.Endereco,
                incorporadora: p.Incorporadora,
                preco: p.Preco,
                areaPrivativa: p.AreaPrivativa.GetValueOrDefault(),
                quartos: p.Quartos.GetValueOrDefault(),
                banheiros: p.Banheiros.GetValueOrDefault(),
                vagasGaragem: p.VagasGaragem.GetValueOrDefault(),
                paginalUrl: p.PaginaUrl,
                bookUrl: p.BookUrl,
                caracteristicas: p.Caracteristicas ?? new List<string>()
            )).ToList();
        }
    }
}
