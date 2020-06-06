using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
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
                                                 colaboradores: new List<UsuarioVO>(),
                                                 administradores: new List<UsuarioVO> { new UsuarioVO(Guid.NewGuid(), "Usuário 1") },
                                                 produtos: new List<ProdutoVO>()
                   ))
               .Generate();

        public static Time ObterTimeColaborativoValido()
           => new Faker<Time>()
               .CustomInstantiator(p => new Time(nome: p.Commerce.Product(),
                                                 escopo: EscopoTime.Colaborativo,
                                                 colaboradores: new List<UsuarioVO>(),
                                                 administradores: new List<UsuarioVO> { new UsuarioVO(Guid.NewGuid(), "Usuário 1") },
                                                 produtos: new List<ProdutoVO>()
                   ))
               .Generate();
    }
}
