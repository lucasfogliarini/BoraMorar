namespace BoraMorar.Propostas
{
    public class Proposta : AggregateRoot, IStatusManaged<PropostaStatus>
    {
        public int CotacaoId { get; set; }
        public string Numero { get; private set; }
        public PropostaStatus Status { get; private set; }

        private Proposta() { }

        public Proposta(int cotacaoId)
        {
            Numero = GerarNumero("PROP");
            CotacaoId = cotacaoId;
            CreatedNow();
            ChangeStatus(PropostaStatus.Gerada);
        }

        public void ChangeStatus(PropostaStatus newStatus)
        {
            Status = newStatus;
            UpdatedNow();
        }
    }
}
