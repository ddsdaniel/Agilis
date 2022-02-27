using Bogus;
using Agilis.Infra.Seguranca.Models.ValueObjects;

namespace Agilis.Test.Mock.Seguranca.Models.ValueObjects
{
    public static class AlterarMinhaSenhaMocks
    {
        public static AlterarMinhaSenha ObterValido()
        {
            var alterarMinhaSenha = new Faker<AlterarMinhaSenha>("pt_BR")
             .CustomInstantiator(f =>
             {
                 var senhaAtual = SenhaMocks.ObterValido();
                 var novaSenha = SenhaMocks.ObterValido();

                 return new AlterarMinhaSenha(
                     senhaAtual,
                     novaSenha,
                     confirmaSenha: novaSenha
                 );
             })
             .Generate();

            return alterarMinhaSenha;
        }

        public static AlterarMinhaSenha ObterComSenhaAtual(Senha senhaAtual)
        {
            var alterarMinhaSenha = new Faker<AlterarMinhaSenha>("pt_BR")
             .CustomInstantiator(f =>
             {
                 var novaSenha = SenhaMocks.ObterValido();

                 return new AlterarMinhaSenha(
                     senhaAtual,
                     novaSenha,
                     confirmaSenha: novaSenha
                 );
             })
             .Generate();

            return alterarMinhaSenha;
        }

    }

}
