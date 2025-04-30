namespace BoraMorar.Imoveis.Repository;

public interface IImovelRepository : IAddRepository<Imovel>
{
    Task<Imovel?> FindAsync(long id);
}
