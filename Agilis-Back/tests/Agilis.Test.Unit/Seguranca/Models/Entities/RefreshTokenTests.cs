using Agilis.Test.Mock.Seguranca.Models.Entities;
using Xunit;

namespace Agilis.Test.Unit.Seguranca.Models.Entities
{
    public class RefreshTokenTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var refreshToken = RefreshTokenMocks.ObterValido();

            //Assert
            Assert.True(refreshToken.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("token-invalido")]
        [InlineData(null)]
        public void Construtor_TokenInvalido_Invalid(string token)
        {
            //Arrange & Act
            var refreshToken = RefreshTokenMocks.ObterComToken(token);

            //Assert
            Assert.True(refreshToken.Invalid);
        }

        [Fact]
        public void Decodificar_DadosValidos_Valid()
        {
            //Arrange
            var refreshToken = RefreshTokenMocks.ObterValido();

            //Act
            var jwtToken = refreshToken.Decodificar();

            //Assert
            Assert.True(refreshToken.Valid);
            Assert.NotNull(jwtToken);
            Assert.True(jwtToken.ValidTo > System.DateTime.Now);
        }

        [Fact]
        public void ObterEmail_DadosValidos_Valid()
        {
            //Arrange
            var refreshToken = RefreshTokenMocks.ObterValido();

            //Act
            var email = refreshToken.ObterEmail();

            //Assert
            Assert.True(refreshToken.Valid);
            Assert.NotNull(email);
            Assert.True(email.Valid);
        }

        [Fact]
        public void TestarSeExpirou_DadosValidos_Valid()
        {
            //Arrange
            var refreshToken = RefreshTokenMocks.ObterValido();

            //Act
            var expirou = refreshToken.TestarSeExpirou();

            //Assert
            Assert.True(refreshToken.Valid);
            Assert.False(expirou);
        }

    }

}
