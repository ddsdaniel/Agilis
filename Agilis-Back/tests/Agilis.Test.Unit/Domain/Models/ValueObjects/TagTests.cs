using Agilis.Test.Mock.Domain.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Domain.Models.ValueObjects
{
    public class TagTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var tag = TagMocks.ObterValido();

            //Assert
            Assert.True(tag.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var tag = TagMocks.ObterComNome(nome);

            //Assert
            Assert.True(tag.Invalid);
        }

    }

}
