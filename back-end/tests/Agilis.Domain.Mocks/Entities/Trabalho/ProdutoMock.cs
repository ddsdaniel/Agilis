using Agilis.Domain.Models.Entities.Trabalho;
using Bogus;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class ProdutoMock
    {
        public static Produto ObterValido()
            => new Faker<Produto>()
               .CustomInstantiator(p => new Produto(p.Commerce.Product()))
               .Generate();
    }
}
