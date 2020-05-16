using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Pessoas
{
    public class AtorTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var ator = AtorMock.ObterValido();

            //Assert
            Assert.True(ator.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var ator = new Ator(nome);

            //Assert
            Assert.True(ator.Invalid);
        }
    }
}
