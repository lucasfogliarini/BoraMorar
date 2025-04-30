namespace BoraMorar.Indices.Repository;

public interface IIndiceAplicadoRepository : IAddRepository<IndiceAplicado>
{
    Task<IndiceAplicado?> FindAsync(int id);
}
