namespace BoraMorar.Propostas.Repository;

public interface IPropostaRepository : IAddRepository<Proposta>
{
    Task<Proposta?> FindAsync(int id);
}
