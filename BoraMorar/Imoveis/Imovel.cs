namespace BoraMorar.Imoveis
{
    public class Imovel(
        string nome,
        Endereco endereco,
        decimal? preco,
        int areaUtil,
        int quartos,
        int banheiros,
        int vagasGaragem,
        string url,
        string bookUrl,
        List<string> caracteristicas) : AggregateRoot
    {
        public string Nome { get; private set; } = nome;
        public Endereco Endereco { get; private set; } = endereco;
        public decimal? Preco { get; private set; } = preco;
        public int AreaUtil { get; private set; } = areaUtil;
        public int Quartos { get; private set; } = quartos;
        public int Banheiros { get; private set; } = banheiros;
        public int VagasGaragem { get; private set; } = vagasGaragem;
        public string Url { get; private set; } = url;
        public string BookUrl { get; private set; } = bookUrl;
        public List<string> Caracteristicas { get; private set; } = caracteristicas ?? [];
    }
}
