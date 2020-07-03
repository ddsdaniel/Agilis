using Agilis.Domain.Models.Entities.Trabalho;
using Bogus;
using System;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class TemaMock
    {
        public static Tema ObterValido()
            => new Faker<Tema>()
               .CustomInstantiator(p => new Tema(p.Commerce.Product(), Guid.NewGuid())
               ).Generate();
    }
}
