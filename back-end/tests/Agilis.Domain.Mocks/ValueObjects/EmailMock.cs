using Bogus;
using DDS.Domain.Core.Model.ValueObjects;

namespace Agilis.Domain.Mocks.ValueObjects
{
    public static class EmailMock
    {
        public static Email ObterValido()
            => new Faker<Email>()
               .CustomInstantiator(e => new Email(e.Internet.Email()))
               .Generate();
    }
}
