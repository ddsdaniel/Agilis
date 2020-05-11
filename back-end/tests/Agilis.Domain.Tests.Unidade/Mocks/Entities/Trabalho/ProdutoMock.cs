﻿using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas;
using Bogus;
using System;

namespace Agilis.Domain.Tests.Unidade.Mocks.Entities.Trabalho
{
    public static class ProdutoMock
    {
        public static Produto ObterValido()
            => new Faker<Produto>()
               .CustomInstantiator(p => new Produto(Guid.NewGuid(), p.Commerce.Product()))
               .Generate();
    }
}
