namespace BoraMorar.Application.Imoveis.Buscar;

public record BuscarImovelCommand(int Id) : ICommand<BuscarImovelResponse>;
