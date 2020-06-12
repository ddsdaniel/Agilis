using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using System;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Trabalho
{
    public class ReleaseTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var release = ReleaseMock.ObterValido();

            //Assert
            Assert.True(release.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var release = new Release(1, 
                                      nome,
                                      new TimeVO(Guid.NewGuid(), "Time 1")
                                      );

            //Assert
            Assert.True(release.Invalid);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void Construtor_OrdemNegativa_Invalid(int ordem)
        {
            //Arrange & Act
            var release = new Release(ordem,
                                      "Nome válido",
                                      new TimeVO(Guid.NewGuid(), "Time 1")
                                      );

            //Assert
            Assert.True(release.Invalid);
        }
    }
}
