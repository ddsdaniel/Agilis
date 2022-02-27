using Bogus;
using Agilis.Infra.Seguranca.Models.ValueObjects;

namespace Agilis.Test.Mock.Seguranca.Models.ValueObjects
{
    public static class SenhaMocks
    {
        public static Senha ObterValido()
        {
            var senha = new Faker<Senha>("pt_BR")
             .CustomInstantiator(f => new Senha(
                 conteudo: f.Internet.Password(Senha.TAMANHO_MINIMO)
                 ))
             .Generate();

            return senha;
        }

        public static Senha ObterComConteudo(string conteudo)
        {
            var senha = new Faker<Senha>("pt_BR")
             .CustomInstantiator(f => new Senha(
                 conteudo
                 ))
             .Generate();

            return senha;
        }

    }

}
