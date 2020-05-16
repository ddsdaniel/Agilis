using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Bogus;
using System;

namespace Agilis.Domain.Mocks.Entities.Pessoas
{
    public static class TimeMock
    {
        public static Time ObterTimePessoalValido()
            => new Faker<Time>()
               .CustomInstantiator(p => new Time(Guid.NewGuid(), 
                                                 p.Commerce.Product(), 
                                                 p.Random.Bool(), 
                                                 EscopoTime.Pessoal))
               .Generate();

        public static Time ObterTimeColaborativoValido()
            => new Faker<Time>()
               .CustomInstantiator(p => new Time(Guid.NewGuid(), 
                                                 p.Commerce.Product(), 
                                                 p.Random.Bool(), 
                                                 EscopoTime.Colaborativo))
               .Generate();
    }
}
