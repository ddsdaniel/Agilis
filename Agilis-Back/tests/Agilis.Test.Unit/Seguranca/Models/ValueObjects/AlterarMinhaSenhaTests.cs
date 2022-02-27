using Agilis.Infra.Seguranca.Models.ValueObjects;
using Agilis.Test.Mock.Seguranca.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Seguranca.Models.ValueObjects
{
    public class AlterarMinhaSenhaTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var alterarMinhaSenha = AlterarMinhaSenhaMocks.ObterValido();

            //Assert
            Assert.True(alterarMinhaSenha.Valid);
        }

        [Theory]
        [InlineData("123", "321", "321", true)]
        [InlineData("123", "123", "123", false)]
        [InlineData("123", "321", "4321", false)]
        [InlineData(null, "321", "321", false)]
        [InlineData("123", null, null, false)]
        [InlineData("123", null, "321", false)]
        [InlineData("123", "321", null, false)]
        public void Construtor_ConformeParametros_ConformeParametros(string senhaAtual, string novaSenha, string confirmaSenha, bool esperado)
        {
            //Arrange & Act
            var alterarMinhaSenha = new AlterarMinhaSenha(
                new Senha(senhaAtual),
                new Senha(novaSenha),
                new Senha(confirmaSenha)
                );

            //Assert
            Assert.Equal(esperado, alterarMinhaSenha.Valid);
        }

        [Fact]
        public void Construtor_SenhaAtualNull_Invalid()
        {
            //Arrange
            var novaSenha = SenhaMocks.ObterValido();

            //Act
            var alterarMinhaSenha = new AlterarMinhaSenha(null, novaSenha, novaSenha);

            //Assert
            Assert.True(alterarMinhaSenha.Invalid);
        }

        [Fact]
        public void Construtor_NovaSenhaNull_Invalid()
        {
            //Arrange
            var senhaAtual = SenhaMocks.ObterValido();
            var confirmaSenha = SenhaMocks.ObterValido();

            //Act
            var alterarMinhaSenha = new AlterarMinhaSenha(senhaAtual, null, confirmaSenha);

            //Assert
            Assert.True(alterarMinhaSenha.Invalid);
        }

        [Fact]
        public void Construtor_ConfirmaSenhaNull_Invalid()
        {
            //Arrange
            var senhaAtual = SenhaMocks.ObterValido();
            var novaSenha = SenhaMocks.ObterValido();

            //Act
            var alterarMinhaSenha = new AlterarMinhaSenha(senhaAtual, novaSenha, null);

            //Assert
            Assert.True(alterarMinhaSenha.Invalid);
        }

    }

}
