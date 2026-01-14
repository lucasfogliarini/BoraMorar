namespace BoraMorar.Imoveis
{
    /// <summary>
    /// Representa um imóvel disponível para cotação ou exibição no sistema BoraMorar.
    /// </summary>
    public class Imovel : AggregateRoot
    {
        // Propriedades com setters privados
        public string Nome { get; private set; }
        public Endereco Endereco { get; private set; }
        public Incorporadora Incorporadora { get; private set; }
        public decimal? Preco { get; private set; }
        public decimal AreaPrivativa { get; private set; }
        public int Quartos { get; private set; }
        public int Banheiros { get; private set; }
        public int VagasGaragem { get; private set; }
        public string PaginaUrl { get; private set; }
        public string BookUrl { get; private set; }
        public List<string> Caracteristicas { get; private set; } = new();

        /// <summary>
        /// Valor do metro quadrado calculado com base no preço total do imóvel
        /// dividido pela área real privativa.
        /// </summary>
        public decimal? PrecoPorAreaPrivativa => (Preco.HasValue && AreaPrivativa > 0) ? Preco.Value / AreaPrivativa : null;
    }
}
