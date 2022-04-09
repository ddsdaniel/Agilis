using Bogus;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Models.Entities;
using Agilis.Infra.Seguranca.Models.ValueObjects;
using Agilis.Test.Mock.Domain.Models.ValueObjects;
using Agilis.Test.Mock.Seguranca.Models.ValueObjects;

namespace Agilis.Test.Mock.Seguranca.Models.Entities
{
    public static class UsuarioMocks
    {
        public static Usuario ObterValido()
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome: f.Person.FirstName,
                 sobrenome: f.Person.LastName,
                 senha: SenhaMocks.ObterValido(),
                 email: EmailMocks.ObterValido(),
                 ativo: f.Random.Bool(),
                 regra: f.Random.Enum<RegraUsuario>()
                 ))
             .Generate();

            return usuario;
        }

        public static Usuario ObterComNome(string nome)
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome,
                 sobrenome: f.Person.LastName,
                 senha: SenhaMocks.ObterValido(),
                 email: EmailMocks.ObterValido(),
                 ativo: f.Random.Bool(),
                 regra: f.Random.Enum<RegraUsuario>()
                 ))
             .Generate();

            return usuario;
        }

        public static Usuario ObterComSobrenome(string sobrenome)
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome: f.Person.FirstName,
                 sobrenome,
                 senha: SenhaMocks.ObterValido(),
                 email: EmailMocks.ObterValido(),
                 ativo: f.Random.Bool(),
                 regra: f.Random.Enum<RegraUsuario>()
                 ))
             .Generate();

            return usuario;
        }

        public static Usuario ObterComSenha(Senha senha)
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome: f.Person.FirstName,
                 sobrenome: f.Person.LastName,
                 senha,
                 email: EmailMocks.ObterValido(),
                 ativo: f.Random.Bool(),
                 regra: f.Random.Enum<RegraUsuario>()
                 ))
             .Generate();

            return usuario;
        }

        public static Usuario ObterComEmail(Email email)
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome: f.Person.FirstName,
                 sobrenome: f.Person.LastName,
                 senha: SenhaMocks.ObterValido(),
                 email,
                 ativo: f.Random.Bool(),
                 regra: f.Random.Enum<RegraUsuario>()
                 ))
             .Generate();

            return usuario;
        }

        public static Usuario ObterComAtivo(bool ativo)
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome: f.Person.FirstName,
                 sobrenome: f.Person.LastName,
                 senha: SenhaMocks.ObterValido(),
                 email: EmailMocks.ObterValido(),
                 ativo,
                 regra: f.Random.Enum<RegraUsuario>()
                 ))
             .Generate();

            return usuario;
        }

        public static Usuario ObterComRegra(RegraUsuario regra)
        {
            var usuario = new Faker<Usuario>("pt_BR")
             .CustomInstantiator(f => new Usuario(
                 nome: f.Person.FirstName,
                 sobrenome: f.Person.LastName,
                 senha: SenhaMocks.ObterValido(),
                 email: EmailMocks.ObterValido(),
                 ativo: f.Random.Bool(),
                 regra
                 ))
             .Generate();

            return usuario;
        }

    }

}
