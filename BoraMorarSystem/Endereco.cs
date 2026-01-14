using CSharpFunctionalExtensions;

namespace BoraMorar
{
    public class Endereco : Entity<long>
    {
        public TipoEndereco Tipo { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string? Bairro { get; private set; }
        public string? Logradouro { get; private set; }

        private Endereco() { }

        public Endereco(TipoEndereco tipo, string cep, string cidade, string? bairro = null, string? logradouro = null)
        {
            Tipo = tipo;
            Cep = cep;
            Cidade = cidade;
            Bairro = bairro;
            Logradouro = logradouro;
        }
    }

    public enum TipoEndereco
    {
        Cidade,
        Bairro,
        Logradouro
    }
}
