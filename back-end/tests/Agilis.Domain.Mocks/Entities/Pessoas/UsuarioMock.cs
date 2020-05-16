using Agilis.Domain.Enums;
using Agilis.Domain.Mocks.ValueObjects;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Mocks.ValueObjects.Seguranca.Senhas;
using Bogus;

namespace Agilis.Domain.Mocks.Entities.Pessoas
{
    public static class UsuarioMock
    {
        public static Usuario ObterValido()
            => new Faker<Usuario>()
               .CustomInstantiator(p => new Usuario(EmailMock.ObterValido(),
                                                    p.Person.FirstName,
                                                    p.Person.LastName,
                                                    SenhaMock.ObterValida(),
                                                    p.PickRandom<RegraUsuario>()
                                                    ))
               .Generate();

        public static Usuario ObterInvalido()
            => new Usuario(null,
                           null,
                           null,
                           null,
                           RegraUsuario.Usuario
                           );

        public static Usuario ObterAdminValido()
          => new Faker<Usuario>()
             .CustomInstantiator(p => new Usuario(EmailMock.ObterValido(),
                                                  p.Person.FirstName,
                                                  p.Person.LastName,
                                                  SenhaMock.ObterValida(),
                                                  RegraUsuario.Admin
                                                  ))
             .Generate();
    }
}
