using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using System;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Trabalho
{
    public class EpicoTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var epico = EpicoMock.ObterValido();

            //Assert
            Assert.True(epico.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var epico = new Epico(nome, Guid.NewGuid());

            //Assert
            Assert.True(epico.Invalid);
        }
    }
}
