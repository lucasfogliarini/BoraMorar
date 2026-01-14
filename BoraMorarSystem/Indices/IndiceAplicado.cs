namespace BoraMorar.Indices
{
    /// <summary>
    /// Representa o valor de um índice aplicado a um endereço em uma data de referência.
    /// </summary>
    public class IndiceAplicado : AggregateRoot
    {
        public IndiceAplicado(Indice indice, Endereco endereco, decimal valor, DateOnly dataReferencia)
        {
            DataReferencia = dataReferencia;
            Indice = indice;
            Endereco = endereco;
            Valor = valor;
        }

        public IndiceAplicado()
        {
        }

        public DateOnly DataReferencia { get; private set; }
        public Indice Indice { get; private set; }
        public Endereco Endereco { get; private set; }
        public decimal Valor { get; private set; }
    }
}
