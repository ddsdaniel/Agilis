using Agilis.Domain.Enums;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;

namespace Agilis.Domain.Mocks.ValueObjects.Trabalho
{
    public static class RequisitoNaoFuncionalMock
    {
        public static RequisitoNaoFuncional ObterValido()
        {
            return new Faker<RequisitoNaoFuncional>()
                .CustomInstantiator(faker => new RequisitoNaoFuncional(faker.Random.Number(1, 999999),
                                                                       nameof(RequisitoNaoFuncional),
                                                                       faker.PickRandom<TipoRequisitoNaoFuncional>(),
                                                                       UsuarioMock.ObterValido()
                                                                       )
                ).Generate();
        }
    }
}
