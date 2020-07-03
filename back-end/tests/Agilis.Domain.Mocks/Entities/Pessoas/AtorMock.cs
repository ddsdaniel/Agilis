using Agilis.Domain.Models.Entities.Pessoas;
using Bogus;
using System;

namespace Agilis.Domain.Mocks.Entities.Pessoas
{
    public static class AtorMock
    {
        public static Ator ObterValido()
            => new Faker<Ator>()
               .CustomInstantiator(p => new Ator(p.Commerce.Product(), Guid.NewGuid())
               ).Generate();
    }
}
