using CSharpFunctionalExtensions;

namespace BoraMorar.Application;

/// <summary>
/// Representa um comando que expressa uma intenção de ação no domínio, sem retorno.
/// Cor no EventStorming: <b>Azul</b> (Command).
/// </summary>
public interface ICommand;

/// <summary>
/// Representa um comando que expressa uma intenção de ação no domínio e espera uma resposta.
/// Cor no EventStorming: <b>Azul</b> (Command).
/// </summary>
public interface ICommand<TResponse>;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken = default);
}
