using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas;
using Bogus;
using System;

namespace Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas
{
    public static class TimeMock
    {
        public static Time ObterValido()
            => new Faker<Time>()
               .CustomInstantiator(p => new Time(Guid.NewGuid(), p.Commerce.Product()))
               .Generate();
    }
}
