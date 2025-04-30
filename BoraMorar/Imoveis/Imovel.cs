namespace BoraMorar.Imoveis
{
    /// <summary>
    /// Representa um imóvel disponível para cotação ou exibição no sistema BoraMorar.
    /// </summary>
    public class Imovel(
        string nome,
        Endereco endereco,
        Incorporadora incorporadora,
        decimal? preco,
        int areaPrivativa,
        int quartos,
        int banheiros,
        int vagasGaragem,
        string paginalUrl,
        string bookUrl,
        List<string> caracteristicas) : AggregateRoot
    {
        /// <summary>
        /// Nome do imóvel ou identificação informal utilizada para exibição.
        /// </summary>
        public string Nome { get; private set; } = nome;

        /// <summary>
        /// Endereço completo do imóvel.
        /// </summary>
        public Endereco Endereco { get; private set; } = endereco;

        public Incorporadora Incorporadora { get; private set; } = incorporadora;

        /// <summary>
        /// Preço estimado ou informado do imóvel. Pode ser nulo caso ainda não esteja definido.
        /// </summary>
        public decimal? Preco { get; private set; } = preco;

        /// <summary>
        /// Área real privativa da unidade, correspondente ao espaço de uso exclusivo do proprietário,
        /// conforme matrícula do imóvel. Geralmente refere-se ao interior do apartamento.
        /// </summary>
        public decimal AreaPrivativa { get; private set; } = areaPrivativa;

        /// <summary>
        /// Quantidade de quartos disponíveis na unidade.
        /// </summary>
        public int Quartos { get; private set; } = quartos;

        /// <summary>
        /// Quantidade de banheiros disponíveis na unidade.
        /// </summary>
        public int Banheiros { get; private set; } = banheiros;

        /// <summary>
        /// Quantidade de vagas de garagem associadas à unidade.
        /// </summary>
        public int VagasGaragem { get; private set; } = vagasGaragem;

        /// <summary>
        /// URL da página de detalhes do imóvel.
        /// </summary>
        public string PaginaUrl { get; private set; } = paginalUrl;

        /// <summary>
        /// URL do book ou material complementar com imagens e planta baixa do imóvel.
        /// </summary>
        public string BookUrl { get; private set; } = bookUrl;

        /// <summary>
        /// Lista de características adicionais do imóvel (ex: "Piscina", "Churrasqueira", "Portaria 24h").
        /// </summary>
        public List<string> Caracteristicas { get; private set; } = caracteristicas ?? [];

        /// <summary>
        /// Valor do metro quadrado calculado com base no preço total do imóvel
        /// dividido pela área real privativa.
        /// </summary>
        public decimal? PrecoPorAreaPrivativa => (Preco.HasValue && AreaPrivativa > 0) ? Preco.Value / AreaPrivativa : null;

    }
}
