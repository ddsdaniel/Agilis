using Agilis.Domain.Enums;
using Agilis.Domain.Mocks.ValueObjects;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Bogus;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.Entities.Pessoas
{
    public static class TimeMock
    {
        public static Time ObterTimePessoalValido()
            => new Faker<Time>()
               .CustomInstantiator(p => new Time(nome: p.Commerce.Product(),
                                                 escopo: EscopoTime.Pessoal,
                                                 colaboradores: new List<UsuarioFK>(),
                                                 administradores: new List<UsuarioFK> {
                                                     new UsuarioFK(Guid.NewGuid(), "Usuário 1", EmailMock.ObterValido().Endereco)
                                                 }
                   ))
               .Generate();

        public static Time ObterTimeColaborativoValido()
           => new Faker<Time>()
               .CustomInstantiator(p => new Time(nome: p.Commerce.Product(),
                                                 escopo: EscopoTime.Colaborativo,
                                                 colaboradores: new List<UsuarioFK>(),
                                                 administradores: new List<UsuarioFK> { 
                                                     new UsuarioFK(Guid.NewGuid(), "Usuário 1", EmailMock.ObterValido().Endereco) 
                                                 }
                   ))
               .Generate();
    }
}
