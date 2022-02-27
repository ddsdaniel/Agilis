using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Models.ValueObjects;
using Agilis.Test.Mock.Seguranca.Models.Entities;
using Agilis.Test.Mock.Seguranca.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Seguranca.Models.Entities
{
    public class UsuarioTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var usuario = UsuarioMocks.ObterValido();

            //Assert
            Assert.True(usuario.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var usuario = UsuarioMocks.ObterComNome(nome);

            //Assert
            Assert.True(usuario.Invalid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_SobrenomeInvalido_Invalid(string sobrenome)
        {
            //Arrange & Act
            var usuario = UsuarioMocks.ObterComSobrenome(sobrenome);

            //Assert
            Assert.True(usuario.Invalid);
        }

        [Fact]
        public void Construtor_SenhaNull_Invalid()
        {
            //Arrange & Act
            var usuario = UsuarioMocks.ObterComSenha(null);

            //Assert
            Assert.True(usuario.Invalid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_SenhaInvalida_Invalid(string conteudo)
        {
            //Arrange
            var senhaInvalida = new Senha(conteudo);

            //Act
            var usuario = UsuarioMocks.ObterComSenha(senhaInvalida);

            //Assert
            Assert.True(usuario.Invalid);
            Assert.True(senhaInvalida.Invalid);
        }

        [Fact]
        public void Construtor_EmailNull_Invalid()
        {
            //Arrange & Act
            var usuario = UsuarioMocks.ObterComEmail(null);

            //Assert
            Assert.True(usuario.Invalid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_EmailInvalido_Invalid(string endereco)
        {
            //Arrange
            var emailInvalido = new Email(endereco);

            //Act
            var usuario = UsuarioMocks.ObterComEmail(emailInvalido);

            //Assert
            Assert.True(usuario.Invalid);
            Assert.True(emailInvalido.Invalid);
        }

        [Fact]
        public void AlterarSenha_DadosValidos_Valid()
        {
            //Arrange
            var usuario = UsuarioMocks.ObterValido();
            var alterarMinhaSenha = AlterarMinhaSenhaMocks.ObterComSenhaAtual(usuario.Senha);

            //Act
            usuario.AlterarSenha(alterarMinhaSenha);

            //Assert
            Assert.True(usuario.Valid);
            Assert.True(alterarMinhaSenha.Valid);
            Assert.Equal(alterarMinhaSenha.NovaSenha.Conteudo, usuario.Senha.Conteudo);
        }


        [Fact]
        public void AlterarSenha_SenhaErrada_Invalid()
        {
            //Arrange
            var usuario = UsuarioMocks.ObterValido();
            var alterarMinhaSenha = AlterarMinhaSenhaMocks.ObterValido();
            var senhaAnterior = usuario.Senha.Conteudo;

            //Act
            usuario.AlterarSenha(alterarMinhaSenha);

            //Assert
            Assert.True(alterarMinhaSenha.Valid);
            Assert.True(usuario.Invalid);
            Assert.Equal(senhaAnterior, usuario.Senha.Conteudo);
        }

        [Fact]
        public void RedefinirSenha_DadosValidos_Valid()
        {
            //Arrange
            var usuario = UsuarioMocks.ObterValido();
            var redefinirSenha = RedefinicaoSenhaMocks.ObterValido();

            //Act
            usuario.RedefinirSenha(redefinirSenha);

            //Assert
            Assert.True(usuario.Valid);
            Assert.True(redefinirSenha.Valid);
            Assert.Equal(redefinirSenha.NovaSenha.Conteudo, usuario.Senha.Conteudo);
        }

    }

}
