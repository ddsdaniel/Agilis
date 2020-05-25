using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.ValueObjects.Trabalho
{
    public static class ModuloMock
    {
        public static Modulo ObterValido(int numero)
        {
            return new Faker<Modulo>()
                .CustomInstantiator(faker => new Modulo(numero,
                                                        nameof(Modulo),
                                                        new List<RegraDeNegocio>(),
                                                        new List<RequisitoFuncional>()
                                                        )
                ).Generate();
        }

        public static Modulo ObterValido()
        {
            return new Faker<Modulo>()
                .CustomInstantiator(faker => ObterValido(faker.Random.Number(1, 999999)))
                .Generate();
        }

        public static Modulo ObterInvalido()
        {
            return new Modulo(-1, null, null, null);
        }
    }
}
