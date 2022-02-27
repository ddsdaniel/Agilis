using Agilis.Test.Mock.Domain.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Domain.Models.ValueObjects
{
    public class HtmlColorTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var htmlColor = HtmlColorMocks.ObterValido();

            //Assert
            Assert.True(htmlColor.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("#JJJJJJ")]
        [InlineData("#1")]
        [InlineData("#12")]
        [InlineData("#1234")]
        [InlineData("#12345")]
        [InlineData("#1234567")]
        [InlineData("#12345678")]
        [InlineData("#A")]
        [InlineData("#AB")]
        [InlineData("#ABCD")]
        [InlineData("#ABCDE")]
        [InlineData("#ABCDEFG")]
        [InlineData("#ABCDEFGH")]

        public void Construtor_CodigoInvalido_Invalid(string codigo)
        {
            //Arrange & Act
            var htmlColor = HtmlColorMocks.ObterComCodigo(codigo);

            //Assert
            Assert.True(htmlColor.Invalid);
        }

    }

}
