using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Bogus;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class TemaMock
    {
        public static Tema ObterValido()
            => new Faker<Tema>()
               .CustomInstantiator(p => new Tema(p.Commerce.Product(), Guid.NewGuid(), new List<EpicoFK>())
               ).Generate();
    }
}
