using Agilis.Domain.Models.Entities.Pessoas;
using Bogus;

namespace Agilis.Domain.Mocks.Entities.Pessoas
{
    public static class AtorMock
    {
        public static Ator ObterValido()
            => new Faker<Ator>()
               .CustomInstantiator(p => new Ator(p.Person.FirstName))
               .Generate();

        public static Ator ObterInvalido()
            => new Ator(null);
    }
}
