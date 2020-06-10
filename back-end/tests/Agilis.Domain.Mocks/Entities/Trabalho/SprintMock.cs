using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;
using DDS.Domain.Core.Model.ValueObjects;
using System;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class SprintMock
    {
        public static Sprint ObterValido()
            => new Faker<Sprint>()
               .CustomInstantiator(p =>
                   {
                       var numero = p.Random.Number(0, 1000);

                       return new Sprint($"Sprint {numero}",
                                        numero,
                                        new IntervaloDatas(p.Date.Past(), p.Date.Future()),
                                        new ProdutoVO(Guid.NewGuid(), p.Commerce.ProductName())
                                        );
                   }
               ).Generate();
    }
}
