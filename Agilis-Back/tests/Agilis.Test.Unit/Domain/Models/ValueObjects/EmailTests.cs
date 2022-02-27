using Agilis.Test.Mock.Domain.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Domain.Models.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var email = EmailMocks.ObterValido();

            //Assert
            Assert.True(email.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("usuario@@dominio.com")]
        [InlineData("usuario@dominio..com")]
        [InlineData("usuario@dominio")]
        public void Construtor_EnderecoInvalido_Invalid(string endereco)
        {
            //Arrange & Act
            var email = EmailMocks.ObterComEndereco(endereco);

            //Assert
            Assert.True(email.Invalid);
        }

    }

}
