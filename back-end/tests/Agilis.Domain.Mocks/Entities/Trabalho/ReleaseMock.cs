using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using Bogus;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class ReleaseMock
    {
        public static Release ObterValido()
            => new Faker<Release>()
               .CustomInstantiator(faker => new Release(faker.Random.Number(0, 1000),
                                                        faker.Commerce.Product(),
                                                        TimeMock.ObterTimeVOValido()
                                                        )
               ).Generate();
    }
}
