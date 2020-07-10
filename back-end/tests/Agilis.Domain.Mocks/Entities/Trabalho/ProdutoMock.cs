using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class ProdutoMock
    {
        public static Produto ObterValido()
            => new Faker<Produto>()
               .CustomInstantiator(p => new Produto(
                   p.Commerce.Product(), 
                   Guid.NewGuid(),
                   new List<AtorFK>(),
                   new StoryMapping(new List<Tema>())
                   )
               ).Generate();
    }
}
