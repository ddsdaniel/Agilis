using Agilis.Core.Domain.Services;
using Xunit;

namespace Agilis.Test.Unit.Domain.Services
{
    public class AdvancedEncryptionStandardServiceTests
    {
        private const string CHAVE_SECRETA = "10B74384-C4FA-4FCE-A121-0AF237BB7F75";

        [Theory]
        [InlineData("Senha", "PP0DysMJcW+wSEM7MZJsiA==")]
        [InlineData("AdvancedEncryptionStandardService", "cCJ5CJRD1iIadqUsdJ6INpoOuDP/G1toTKBxofggm58vw+bTzMH6numpkoVZzqEB")]
        public void Cifrar_StringDecifrada_StringCifrada(string input, string esperado)
        {
            //Arrange
            var advancedEncryptionStandardService = new AdvancedEncryptionStandardService();

            //Act
            var atual = advancedEncryptionStandardService.Cifrar(input, CHAVE_SECRETA);

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("PP0DysMJcW+wSEM7MZJsiA==", "Senha")]
        [InlineData("cCJ5CJRD1iIadqUsdJ6INpoOuDP/G1toTKBxofggm58vw+bTzMH6numpkoVZzqEB", "AdvancedEncryptionStandardService")]
        public void Decifrar_StringCifrada_StringDecifrada(string input, string esperado)
        {
            //Arrange
            var advancedEncryptionStandardService = new AdvancedEncryptionStandardService();

            //Act
            var atual = advancedEncryptionStandardService.Decifrar(input, CHAVE_SECRETA);

            //Assert
            Assert.Equal(esperado, atual);
        }

    }

}
