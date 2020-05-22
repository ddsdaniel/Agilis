using Agilis.Domain.Enums;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;

namespace Agilis.Domain.Mocks.ValueObjects.Trabalho
{
    public static class RequisitoNaoFuncionalMock
    {
        public static RequisitoNaoFuncional ObterValido(int numero)
        {
            return new Faker<RequisitoNaoFuncional>()
                .CustomInstantiator(faker => new RequisitoNaoFuncional(numero,
                                                                       nameof(RequisitoNaoFuncional),
                                                                       faker.PickRandom<TipoRequisitoNaoFuncional>(),
                                                                       UsuarioMock.ObterValido()
                                                                       )
                ).Generate();
        }

        public static RequisitoNaoFuncional ObterValido()
        {
            return new Faker<RequisitoNaoFuncional>()
                .CustomInstantiator(faker => ObterValido(faker.Random.Number(1, 999999)))
                .Generate();
        }

        public static RequisitoNaoFuncional ObterInvalido()
        {
            return new RequisitoNaoFuncional(-1, null, TipoRequisitoNaoFuncional.Entrega, null);
        }
    }
}
