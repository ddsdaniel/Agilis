using Bogus;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Test.Mock.Domain.Models.ValueObjects
{
    public static class EmailMocks
    {
        public static Email ObterValido()
        {
            var email = new Faker<Email>("pt_BR")
             .CustomInstantiator(f => new Email(
                 endereco: f.Internet.Email()
                 ))
             .Generate();

            return email;
        }

        public static Email ObterComEndereco(string endereco)
        {
            var email = new Faker<Email>("pt_BR")
             .CustomInstantiator(f => new Email(
                 endereco
                 ))
             .Generate();

            return email;
        }

    }

}
