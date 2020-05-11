using Agilis.Domain.Models.Entities.Pessoas;
using Bogus;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;

namespace Agilis.Domain.Tests.Unidade.Mocks.ValueObjects.Seguranca.Senhas
{
    public static class SenhaMock
    {
        public static SenhaMedia ObterValida()
            => new Faker<SenhaMedia>()
                .CustomInstantiator(s => new SenhaMedia(s.Internet.Password(Usuario.TAMANHO_MINIMO_SENHA,
                                                                            false,
                                                                            "\\w",
                                                                            "1a@"),
                                                        Usuario.TAMANHO_MINIMO_SENHA))
                .Generate();

    }
}
