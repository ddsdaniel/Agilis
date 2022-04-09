using Bogus;
using Agilis.Core.Domain.Models.Entities;

namespace Agilis.Test.Mock.Domain.Models.Entities
{
    public static class MigrationMocks
    {
        public static Migration ObterValido()
        {
            var migration = new Faker<Migration>("pt_BR")
             .CustomInstantiator(f => new Migration(
                 nome: f.Random.String()
                 ))
             .Generate();

            return migration;
        }

        public static Migration ObterComNome(string nome)
        {
            var migration = new Faker<Migration>("pt_BR")
             .CustomInstantiator(f => new Migration(
                 nome
                 ))
             .Generate();

            return migration;
        }

    }

}
