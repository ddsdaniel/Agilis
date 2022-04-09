using Bogus;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Test.Mock.Domain.Models.ValueObjects
{
    public static class TagMocks
    {
        public static Tag ObterValido()
        {
            var tag = new Faker<Tag>("pt_BR")
             .CustomInstantiator(f => new Tag(
                 nome: f.Random.String()
                 ))
             .Generate();

            return tag;
        }

        public static Tag ObterComNome(string nome)
        {
            var tag = new Faker<Tag>("pt_BR")
             .CustomInstantiator(f => new Tag(
                 nome
                 ))
             .Generate();

            return tag;
        }

    }

}
