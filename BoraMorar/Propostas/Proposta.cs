namespace BoraMorar
{
    public class Proposta : AggregateRoot
    {
        public int CotacaoId { get; set; }
        public string Numero { get; private set; }
        public StatusProposta Status { get; private set; }
        public DateTime DataCriacao { get; private set; }

        private Proposta() { }

        public Proposta(int cotacaoId)
        {
            Numero = GerarNumero("PROP");
            CotacaoId = cotacaoId;
            DataCriacao = DateTime.UtcNow;
        }
    }

    public enum StatusProposta
    {
        Solicitada,
        AprovadaFinanceiramente,        
        AprovadaPeloCliente,
        ContratoAssinado,
        Rejeitada,
        Cancelada
    }
}
