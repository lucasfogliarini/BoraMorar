namespace BoraMorar.Cotacoes.Repository;

public interface ICotacaoRepository : IAddRepository<Cotacao>
{
    Task<Cotacao?> FindAsync(int id);
}
