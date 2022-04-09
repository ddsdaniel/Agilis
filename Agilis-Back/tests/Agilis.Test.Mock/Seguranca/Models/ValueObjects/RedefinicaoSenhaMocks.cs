using Bogus;
using Agilis.Infra.Seguranca.Models.ValueObjects;

namespace Agilis.Test.Mock.Seguranca.Models.ValueObjects
{
    public static class RedefinicaoSenhaMocks
    {
        public static RedefinicaoSenha ObterValido()
        {
            var redefinicaoSenha = new Faker<RedefinicaoSenha>("pt_BR")
             .CustomInstantiator(f =>
             {
                 var mesmaSenha = SenhaMocks.ObterValido();

                 return new RedefinicaoSenha(
                     novaSenha: mesmaSenha,
                     confirmaSenha: mesmaSenha
                 );
             })
             .Generate();

            return redefinicaoSenha;
        }

        public static RedefinicaoSenha ObterComNovaSenha(Senha novaSenha)
        {
            var redefinicaoSenha = new Faker<RedefinicaoSenha>("pt_BR")
             .CustomInstantiator(f => new RedefinicaoSenha(
                 novaSenha,
                 confirmaSenha: SenhaMocks.ObterValido()
                 ))
             .Generate();

            return redefinicaoSenha;
        }

        public static RedefinicaoSenha ObterComConfirmaSenha(Senha confirmaSenha)
        {
            var redefinicaoSenha = new Faker<RedefinicaoSenha>("pt_BR")
             .CustomInstantiator(f => new RedefinicaoSenha(
                 novaSenha: SenhaMocks.ObterValido(),
                 confirmaSenha
                 ))
             .Generate();

            return redefinicaoSenha;
        }

    }

}
