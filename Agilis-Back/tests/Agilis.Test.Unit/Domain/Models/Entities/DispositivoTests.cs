using Agilis.Test.Mock.Domain.Models.Entities;
using Xunit;

namespace Agilis.Test.Unit.Domain.Models.Entities
{
    public class DispositivoTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var dispositivo = DispositivoMocks.ObterValido();

            //Assert
            Assert.True(dispositivo.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_TokenInvalido_Invalid(string token)
        {
            //Arrange & Act
            var dispositivo = DispositivoMocks.ObterComToken(token);

            //Assert
            Assert.True(dispositivo.Invalid);
        }

    }

}
