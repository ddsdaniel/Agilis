using Agilis.Domain.Models.Entities.Trabalho;
using Bogus;
using System;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class EpicoMock
    {
        public static Epico ObterValido()
            => new Faker<Epico>()
               .CustomInstantiator(p => new Epico(p.Commerce.Product(), Guid.NewGuid())
               ).Generate();
    }
}
