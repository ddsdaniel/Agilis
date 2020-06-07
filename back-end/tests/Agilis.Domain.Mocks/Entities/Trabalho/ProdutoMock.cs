﻿using Agilis.Domain.Mocks.ValueObjects.Especificacao;
using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Bogus;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class ProdutoMock
    {
        public static Produto ObterValido()
            => new Faker<Produto>()
               .CustomInstantiator(p => new Produto(p.Commerce.Product(),
                                                    new TimeVO(Guid.NewGuid(), "Time 1"),
                                                    new List<RequisitoNaoFuncional> { RequisitoNaoFuncionalMock.ObterValido() },
                                                    LinguagemUbiquaMock.ObterValida()
                                                   )
               ).Generate();
    }
}
