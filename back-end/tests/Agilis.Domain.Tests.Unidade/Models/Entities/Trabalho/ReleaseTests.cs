using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using System.Collections.Generic;
using Agilis.Domain.Models.ForeignKeys;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;

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
            var release = new Release(nome: nome,
                                      sprints: new List<SprintFK>(),
                                      productBacklog: new ProductBacklog()
                                      );

            //Assert
            Assert.True(release.Invalid);
        }
    }
}
