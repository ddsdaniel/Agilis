using Bogus;
using Agilis.Core.Domain.Models.Entities;

namespace Agilis.Test.Mock.Domain.Models.Entities
{
    public static class DispositivoMocks
    {
        public static Dispositivo ObterValido()
        {
            var dispositivo = new Faker<Dispositivo>("pt_BR")
             .CustomInstantiator(f => new Dispositivo(
                 token: f.Random.String()
                 ))
             .Generate();

            return dispositivo;
        }

        public static Dispositivo ObterComToken(string token)
        {
            var dispositivo = new Faker<Dispositivo>("pt_BR")
             .CustomInstantiator(f => new Dispositivo(
                 token
                 ))
             .Generate();

            return dispositivo;
        }

    }

}
