namespace BoraMorar.Indices
{
    /// <summary>
    /// Representa o valor de um índice aplicado a um endereço em uma data de referência.
    /// </summary>
    public class IndiceAplicado(Indice indice, Endereco endereco, decimal valor, DateOnly dataReferencia) : AggregateRoot
    {
        public DateOnly DataReferencia { get; private set; } = dataReferencia;
        public Indice Indice { get; private set; } = indice;
        public Endereco Endereco { get; private set; } = endereco;
        public decimal Valor { get; private set; } = valor;
    }
}
