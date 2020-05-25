using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class ProdutoMock
    {
        public static Produto ObterValido()
            => new Faker<Produto>()
               .CustomInstantiator(p => new Produto(p.Commerce.Product(),
                                                    new List<RequisitoNaoFuncional> { RequisitoNaoFuncionalMock.ObterValido() },
                                                    new List<Modulo> { ModuloMock.ObterValido() }
                                                   )
               ).Generate();
    }
}
