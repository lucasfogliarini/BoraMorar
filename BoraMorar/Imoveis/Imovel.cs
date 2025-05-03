namespace BoraMorar.Imoveis
{
    /// <summary>
    /// Representa um imóvel disponível para cotação ou exibição no sistema BoraMorar.
    /// </summary>
    public class Imovel(
        string nome,
        Endereco endereco,
        Incorporadora incorporadora,
        string paginalUrl,
        string bookUrl) : AggregateRoot
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
        /// URL da página de detalhes do imóvel.
        /// </summary>
        public string PaginaUrl { get; private set; } = paginalUrl;

        /// <summary>
        /// URL do book ou material complementar com imagens e planta baixa do imóvel.
        /// </summary>
        public string BookUrl { get; private set; } = bookUrl;
        /// <summary>
        /// Preço estimado ou informado do imóvel. Pode ser nulo caso ainda não esteja definido.
        /// </summary>
        public decimal? Preco { get; set; }
        /// <summary>
        /// Valor atual estimado do imóvel
        /// </summary>
        public decimal? ValorEstimadoAtual { get; set; }

        /// <summary>
        /// Área real privativa da unidade, correspondente ao espaço de uso exclusivo do proprietário,
        /// conforme matrícula do imóvel. Geralmente refere-se ao interior do apartamento.
        /// </summary>
        public decimal AreaPrivativa { get; set; }

        /// <summary>
        /// Quantidade de quartos disponíveis na unidade.
        /// </summary>
        public int Quartos { get; set; }

        /// <summary>
        /// Quantidade de banheiros disponíveis na unidade.
        /// </summary>
        public int Banheiros { get; set; }

        /// <summary>
        /// Quantidade de vagas de garagem associadas à unidade.
        /// </summary>
        public int VagasGaragem { get; set; }
        /// <summary>
        /// Valor do metro quadrado calculado com base no preço total do imóvel
        /// dividido pela área real privativa.
        /// </summary>
        public decimal? PrecoPorAreaPrivativa => (Preco.HasValue && AreaPrivativa > 0) ? Preco.Value / AreaPrivativa : null;
    }
}
