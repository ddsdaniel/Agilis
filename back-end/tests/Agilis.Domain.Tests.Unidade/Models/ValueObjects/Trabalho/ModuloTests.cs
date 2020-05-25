using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System.Collections.Generic;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.ValueObjects.Trabalho
{
    public class ModuloTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var modulo = ModuloMock.ObterValido();

            //Assert
            Assert.True(modulo.Valid);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Construtor_NumeroInvalido_Invalid(int numero, bool resultadoEsperado)
        {
            //Arrange & Act
            var modulo = new Modulo(numero,
                                    nameof(Modulo),
                                    new List<RegraDeNegocio>(),
                                    new List<RequisitoFuncional>()
                                    );

            //Assert
            Assert.Equal(resultadoEsperado, modulo.Valid);
        }

        [Theory]
        [InlineData("Descrição válida", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Construtor_NomeInvalido_Invalid(string nome, bool resultadoEsperado)
        {
            //Arrange & Act
            var modulo = new Modulo(1,
                                    nome,
                                    new List<RegraDeNegocio>(),
                                    new List<RequisitoFuncional>()
                                    );

            //Assert
            Assert.Equal(resultadoEsperado, modulo.Valid);
        }
    }
}
