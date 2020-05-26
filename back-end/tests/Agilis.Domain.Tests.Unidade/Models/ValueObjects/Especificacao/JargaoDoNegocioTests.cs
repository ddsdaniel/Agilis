using Agilis.Domain.Mocks.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.ValueObjects.Especificacao
{
    public class JargaoDoNegocioTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var jargao = JargaoDoNegocioMock.ObterValido();

            //Assert
            Assert.True(jargao.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_JargaoInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var jargao = new JargaoDoNegocio(nome, "Significado");

            //Assert
            Assert.True(jargao.Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_SignificadoInvalido_Invalid(string significado)
        {
            //Arrange & Act
            var jargao = new JargaoDoNegocio("Jargão", significado);

            //Assert
            Assert.True(jargao.Invalid);
        }
    }
}
