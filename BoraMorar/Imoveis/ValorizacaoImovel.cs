using CSharpFunctionalExtensions;
using BoraMorar.Indices;

namespace BoraMorar.Imoveis
{
    /// <summary>
    /// Representa a valorização do valor de um imóvel para uma data com base em um índice aplicado.
    /// </summary>
    public class ValorizacaoImovel
    {
        public Imovel Imovel { get; }
        public IndiceAplicado IndiceAplicado { get; }
        public decimal ValorEstimado { get; }

        private ValorizacaoImovel(Imovel imovel, IndiceAplicado indiceAplicado, decimal valorEstimado)
        {
            Imovel = imovel;
            IndiceAplicado = indiceAplicado;
            ValorEstimado = valorEstimado;
        }

        public const string ValorEstimadoAtualNull = "O imóvel deve possuir um valor estimado atual para aplicar a valorização.";
        public const string EnderecoNotEqual = "O endereço do índice aplicado deve corresponder ao endereço do imóvel.";

        public static Result<ValorizacaoImovel> Criar(Imovel imovel, IndiceAplicado indiceAplicado)
        {
            if (imovel.ValorEstimadoAtual is null)
                return Result.Failure<ValorizacaoImovel>(ValorEstimadoAtualNull);

            if (imovel.Endereco.Cidade != indiceAplicado.Endereco.Cidade)
                return Result.Failure<ValorizacaoImovel>(EnderecoNotEqual);

            imovel.ValorEstimadoAtual = imovel.ValorEstimadoAtual.Value * (1 + (indiceAplicado.Valor / 100));

            return Result.Success(new ValorizacaoImovel(imovel, indiceAplicado, imovel.ValorEstimadoAtual.Value));
        }
    }
}
