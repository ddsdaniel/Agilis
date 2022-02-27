using Agilis.Test.Mock.Domain.Models.Entities;
using Xunit;

namespace Agilis.Test.Unit.Domain.Models.Entities
{
    public class MigrationTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var migration = MigrationMocks.ObterValido();

            //Assert
            Assert.True(migration.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var migration = MigrationMocks.ObterComNome(nome);

            //Assert
            Assert.True(migration.Invalid);
        }

    }

}
