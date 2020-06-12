using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using System;
using DDS.Domain.Core.Model.ValueObjects;
using Agilis.Domain.Models.ValueObjects.Trabalho;

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
                                    1,
                                    new IntervaloDatas(null, null),
                                    new ReleaseVO(1, Guid.NewGuid(), "Release 1")
                                    );

            //Assert
            Assert.True(sprint.Invalid);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void Construtor_NumeroInvalido_Invalid(int numero)
        {
            //Arrange & Act
            var sprint = new Sprint("Nome válido",
                                    numero,
                                    new IntervaloDatas(null, null),
                                    new ReleaseVO(1, Guid.NewGuid(), "Release 1")
                                    );

            //Assert
            Assert.True(sprint.Invalid);
        }
    }
}
