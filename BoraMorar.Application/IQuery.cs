using CSharpFunctionalExtensions;

namespace BoraMorar.Application;

/// <summary>
/// Representa uma consulta ao sistema que retorna uma resposta.
/// Cor no EventStorming: <b>Verde</b> (Read Model / Query).
/// </summary>
public interface IQuery<TResponse>;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
