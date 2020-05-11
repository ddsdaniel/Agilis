using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas;
using System;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Pessoas
{
    public class TimeTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var time = TimeMock.ObterValido();

            //Assert
            Assert.True(time.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var time = new Time(Guid.NewGuid(), nome);

            //Assert
            Assert.True(time.Invalid);
        }
        
        [Fact]
        public void Construtor_UsuarioIdEmpty_Invalid()
        {
            //Arrange & Act
            var time = new Time(Guid.Empty, "Time 1");

            //Assert
            Assert.True(time.Invalid);
        }

    }
}
