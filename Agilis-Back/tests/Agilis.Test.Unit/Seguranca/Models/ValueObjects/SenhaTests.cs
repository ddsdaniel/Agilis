using Agilis.Test.Mock.Seguranca.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Seguranca.Models.ValueObjects
{
    public class SenhaTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var senha = SenhaMocks.ObterValido();

            //Assert
            Assert.True(senha.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_ConteudoInvalido_Invalid(string conteudo)
        {
            //Arrange & Act
            var senha = SenhaMocks.ObterComConteudo(conteudo);

            //Assert
            Assert.True(senha.Invalid);
        }

    }

}
