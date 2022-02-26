using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using DDS.Domain.Core.Models.ValueObjects;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Trabalho
{
    public class SprintTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var sprint = SprintMock.ObterValido();

            //Assert
            Assert.True(sprint.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var sprint = new Sprint(nome,
                                    new IntervaloDatas(null, null)
                                    );

            //Assert
            Assert.True(sprint.Invalid);
        }
    }
}
