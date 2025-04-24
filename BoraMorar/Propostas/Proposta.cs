namespace BoraMorar.Propostas
{
    public class Proposta : AggregateRoot
    {
        public int CotacaoId { get; set; }
        public string Numero { get; private set; }
        public PropostaStatus Status { get; private set; }
        public DateTime DataCriacao { get; private set; }

        private Proposta() { }

        public Proposta(int cotacaoId)
        {
            Numero = GerarNumero("PROP");
            CotacaoId = cotacaoId;
            DataCriacao = DateTime.Now;
            Status = PropostaStatus.Gerada;
        }
    }
}
